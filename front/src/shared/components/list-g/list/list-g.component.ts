import { Component, EventEmitter, Input, OnChanges, OnDestroy, OnInit, Output, SimpleChanges, ViewChild } from '@angular/core';


import { Observable, Subscription } from 'rxjs';
import { map } from 'rxjs/operators';
import { importsModules } from './imports.module';
import { FieldsInterface } from './interfaces/fields-interface';
import { OnClickInterface } from './interfaces/on-click-interface';
import { MatPaginator, PageEvent } from '@angular/material/paginator';


@Component({
  selector: 'list-g',
  standalone: true,
  templateUrl: './list-g.component.html',
  imports: [
    importsModules
  ],
  styleUrls: ['./list-g.component.scss']
})
export class ListGComponent implements OnChanges, OnInit, OnDestroy {

  @ViewChild('paginatorAbove') paginatorAbove!: MatPaginator
  @ViewChild('paginatorBelow') paginatorBelow!: MatPaginator

  @Input('entities') entities$!: Observable<any[] | undefined>;
  paginatedEntities$!: Observable<any[] | undefined>;

  @Input() headersLabel: FieldsInterface[] = [];
  @Input() headersFields: FieldsInterface[] = [];
  @Output() outOnClickIcons = new EventEmitter<OnClickInterface>();
  @Output() outOnClickButton = new EventEmitter<string>();
  @Output() outOnClickHeaderField = new EventEmitter<string>();
  @Output() outonPageChange = new EventEmitter<PageEvent>();

  spinerNoRegisterClean = true;
  length!: number;
  pageSize: number = 20;
  pageIndex = 0
  pageEvent!: PageEvent;

  destroy!: Subscription;

  ngOnChanges(changes: SimpleChanges): void {
    this.paginatorLength();

    this.paginatedEntities$ = this.entities$.pipe(
      map(entities => {
        if (!entities) return [];

        this.length = entities.length;

        // Aplica paginação
        const startIndex = this.pageIndex * this.pageSize;
        return entities.slice(startIndex, startIndex + this.pageSize);
      })
    );
  }

  ngOnInit(): void {
    this.paginatorLength();
  }

  ngOnDestroy(): void {
    this.destroy.unsubscribe();
  }

  paginatorLength() {
    this.destroy = this?.entities$?.pipe(map(x => {
      this.length = x?.length ?? 0
      return x?.length;
    })).subscribe();
  }


  // mobile: boolean = true;
  // screenResize($event: any) {
  //   if ($event.target.innerWidth >= 640) {
  //     this.mobile = false
  //   }
  //   else {
  //     this.mobile = true
  //   }
  // }


  // onPageChange($event: PageEvent) {
  //   this.outonPageChange.emit($event);
  // }



  onPageChange(e: PageEvent) {

    this.pageEvent = e;
    this.length = e.length;
    this.pageSize = e.pageSize;
    this.pageIndex = e.pageIndex;

    this.paginatorAbove.pageIndex = e.pageIndex;
    this.paginatorBelow.pageIndex = e.pageIndex;

    this.paginatedEntities$ = this.entities$.pipe(
      map(entities => {
        if (!entities) return [];

        const startIndex = this.pageIndex * this.pageSize;
        return entities.slice(startIndex, startIndex + this.pageSize);
      })
    );


    // console.log(endIndex)

    //     if ($event.previousPageIndex ?? 0 < $event.pageIndex)
    //       this.entities$ = this.entities$.pipe(map(entities => entities?.slice(startIndex, endIndex)));

    //     else if ($event.previousPageIndex ?? 0 > $event.pageIndex)
    //       this.entities$ = this.entities$.pipe(map(entities => entities?.slice(startIndex, endIndex)));

    //this.paginatorLength();





  }

  onClickHeaderField(field: string) {
    this.outOnClickHeaderField.emit(field);
  }

  onClickButton(field: string) {
    this.outOnClickButton.emit(field);
  }

  onClickIcon(action: string, entityId: number) {

    const onClick: OnClickInterface = {
      action: action,
      entityId: entityId
    }

    // action)
    // entityId)

    this.outOnClickIcons.emit(onClick);

  }
  // onClickIcon(action: string, entityId: number) {

  //   const onClick: OnClickInterface = {
  //     action: action,
  //     entityId: entityId
  //   }

  //   this.outOnClickIcons.emit(onClick);

  // }

  spinner = false
  spinnerEvent($event: boolean) {
    this.spinner = !$event
  }

}

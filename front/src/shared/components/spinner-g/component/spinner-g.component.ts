import { NgIf } from '@angular/common';
import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { MatProgressSpinnerModule as MatProgressSpinnerModule } from '@angular/material/progress-spinner';


import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  selector: 'spinner-g',
  standalone: true,
  templateUrl: './spinner-g.component.html',
  styleUrls: ['./spinner-g.component.scss'],
  imports: [
    MatProgressSpinnerModule,
    NgIf
  ],
})
export class SpinnerGComponent implements OnChanges {

  @Input() entities$ = new Observable<any[] | undefined>();
  @Input() optionalTitle = '';
  @Input() set _empty(value: boolean) {
    this.empty = false;
  }
  @Output() spinnerStatusOut = new EventEmitter<boolean>();

  spinner = true;
  empty = false;

  ngOnChanges(changes: SimpleChanges): void {

    const length = this?.entities$?.pipe(
      map(x => {
        const timeout = setTimeout(() => {
          if (this.spinner)
            this.empty = true;
          this.spinner = false;
        }, 10000)

        if (x?.length ?? 0 > 0) {
          this.spinner = false
          this.spinnerStatusOut.emit(this.spinner)
          clearTimeout(timeout)
        }

        if (x?.length == 0)
          this.spinner = true;
      }),
    ).subscribe();

  }
}

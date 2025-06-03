import { CommonModule } from '@angular/common';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';


import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { Router } from '@angular/router';
import { DatabaseSideNavServices } from '../../../services/database-side-nav.service';


@Component({
  selector: 'side-menu-large',
  templateUrl: './side-menu-large.component.html',
  styleUrls: ['./side-menu-large.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    MatDividerModule,
    MatIconModule,
    MatFormFieldModule,
  ]
})
export class SideMenuLargeComponent implements OnInit {

  // @Input() collapsed:boolean = false;

  arrowMenuCustomer: boolean = false;
  @Output() closeMenu = new EventEmitter<void>();

  constructor(
    private _dataTree: DatabaseSideNavServices,
    private _router: Router
  ) { }

  get dataTree() {
    return this._dataTree.dataTree
  }

  navigateByUrl(route: string) {
    this.closeMenu.emit();
    this._router.navigateByUrl(route)
  }

  levelOneSubMenuArrowOpenClosedOnAction(rootName?: string, subName?: string) {
    this.dataTree.forEach(x => {

      if (x.children) {
        x.children.forEach(y => {

          if (x.name === rootName && y.name === subName) {
            y.opened = !y.opened
          }

        })
      }
    })

   // this.arrowMenuCustomer = !this.arrowMenuCustomer
  }

  rootMenuArrowOpenClosedOnAction(opened: string) {
    this.dataTree.forEach(x => {

      if (x.name === opened) {
        x.opened = !x.opened
      }
    })

   // this.arrowMenuCustomer = !this.arrowMenuCustomer
  }

  ngOnInit(): void {

  }



}

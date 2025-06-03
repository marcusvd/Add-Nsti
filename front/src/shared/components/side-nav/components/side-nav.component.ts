
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { RouterModule } from '@angular/router';


import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenav, MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
// import { BaseForm } from '../../inheritance/forms/base-form';
import { SideMenuLargeComponent } from './common-components/side-menu-large/side-menu-large.component';
import { SideMenuTopLargeComponent } from './common-components/side-menu-large/top-large/top-large.component';
import { SideMenuSlimComponent } from './common-components/side-menu-slim/side-menu-slim.component';
import { SideMenuTopSlimComponent } from './common-components/side-menu-slim/top-slim/top-slim.component';

@Component({
  selector: 'sideNav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.scss'],
  standalone: true,
  imports: [
    MatSidenavModule,
    RouterModule,
    CommonModule,
    MatIconModule,
    MatToolbarModule,
    SideMenuSlimComponent,
    SideMenuLargeComponent,
    SideMenuTopLargeComponent,
    SideMenuTopSlimComponent
  ],
})
export class SideNavComponent implements OnInit, AfterViewInit {

  @ViewChild('sidenav') sidenav!: MatSidenav;

  menuLarge: boolean = true;
  menuSlim: boolean = false;
  menuSlimManually: boolean = false;
  tootlBar: boolean = false;
  menuSlimArrowRightHideShow: boolean = false;
  matSidenavContainerPaddingL: boolean = false;

  constructor(
    // private _dataTree: DatabaseSideNavServices,

  ) { }

  event = { target: window } as unknown as Event;
  screenWidth: number = window.innerWidth;
  screen(event?: Event) {
    const target = event?.target as Window;
    this.screenWidth = target.innerWidth;
    return this.screenWidth
  }


  ngAfterViewInit() {
    const sidenavElement = document.querySelector('.mat-sidenav');

    if (sidenavElement) {
      const observer = new ResizeObserver(entries => {
        for (let entry of entries) {
          const width = entry.contentRect.width;
          //'Sidenav width:', width);

          this.matSidenavContainerPaddingL = width > 238;
        }
      });

      observer.observe(sidenavElement);
    }


  }





  //METHODS
  responsive(event?: Event) {

    if (this.screen(event) <= 639) {
      this.menuSlim = true;
      this.menuLarge = false;
    }
    else {
      this.menuSlim = false;
      this.menuLarge = true;
      this.menuSlimManually = false;
    }

  }

  toggleMenu() {
    this.menuSlim = !this.menuSlim;
    this.menuLarge = !this.menuLarge;
  }



  // toggleMenuLarge() {
  //   this.menuLarge = !this.menuLarge;
  //   this.menuSlim = !this.menuSlim
  //   this.menuSlimManually = false;
  //   // this.sidenavContainer.updateContentMargins();
  // }

  // toggleMenuSlim() {
  //   this.menuSlim = !this.menuSlim
  //   this.menuLarge = !this.menuLarge;
  //   this.menuSlimManually = !this.menuSlimManually
  //   this.sidenav.toggle().then(() => {
  //   // Trigger manual de redimensionamento
  //   window.dispatchEvent(new Event('resize'));
  // });
  //   // this.sidenavContainer.updateContentMargins();
  // }


  toggleMobile() {
    const screenWidth: number = window.innerWidth;
    if (screenWidth <= 639) {
      this.menuSlim = !this.menuSlim;
      this.menuSlimManually = !this.menuSlimManually
    }

    console.log(screenWidth)
  }

  toggleMenuSlimToolBar() {

    this.menuSlim = !this.menuSlim;
    this.menuSlimManually = !this.menuSlimManually
    //   this.sidenav.toggle().then(() => {
    //   // Trigger manual de redimensionamento
    //   window.dispatchEvent(new Event('resize'));
    // });
    // this.sidenavContainer.updateContentMargins();
  }
  // toggleMenuSlimToolBar() {
  //   this.menuSlimManually = !this.menuSlimManually
  //   //   this.sidenav.toggle().then(() => {
  //   //   // Trigger manual de redimensionamento
  //   //   window.dispatchEvent(new Event('resize'));
  //   // });
  //   // this.sidenavContainer.updateContentMargins();
  // }



  ngOnInit(): void {
    //
    this.responsive(this.event);
  }

}

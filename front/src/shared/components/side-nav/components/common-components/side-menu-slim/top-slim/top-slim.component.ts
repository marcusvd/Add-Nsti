import { CommonModule, TitleCasePipe, UpperCasePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { RouterLink } from '@angular/router';



@Component({
  selector: 'top-slim',
  templateUrl: './top-slim.component.html',
  styleUrls: ['./top-slim.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    MatIconModule,
    MatMenuModule,
    MatDividerModule,
    MatFormFieldModule,
    RouterLink,
    UpperCasePipe,
    TitleCasePipe
  ]
})
export class SideMenuTopSlimComponent implements OnInit {

  constructor() { }

  // userName: string;
  // imgProfile: string;

  // logOut() {
  //   this._auth.logOut();
  // }


  ngOnInit(): void {

    // this.userName = this._auth.currentUser.userName;
    // this.imgProfile = this._auth.currentUser.imgProfile;
  }
}

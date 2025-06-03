import { CommonModule, TitleCasePipe, UpperCasePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { Route, Router, RouterLink } from '@angular/router';
import { AuthService } from 'components/authentication/services/auth.service';



@Component({
  selector: 'top-large',
  templateUrl: './top-large.component.html',
  styleUrls: ['./top-large.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    MatIconModule,
    MatMenuModule,
    MatDividerModule,
    MatFormFieldModule,
    UpperCasePipe
  ]
})
export class SideMenuTopLargeComponent implements OnInit {

  constructor(
    private _auth: AuthService,
    private _router: Router
  ) { }


  // userName: string;
  firstLetter: string = '';
  userName: string = '';
  // imgProfile: string;

  logOut() {
    this._auth.logOut();
    this._router.navigateByUrl('login');
  }




  ngOnInit(): void {
    this.firstLetter = localStorage.getItem('userName')?.substring(0,1) ?? '';
    this.userName = localStorage.getItem('userName')?.split('@')[0] ?? '';
    //  this.firstLetter = this._auth.currentUser.userName[0];
    // this.userName = this._auth.currentUser.userName;
    // this.imgProfile = this._auth.currentUser.imgProfile;
  }
}

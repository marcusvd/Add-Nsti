import { CommonModule, UpperCasePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { Router } from '@angular/router';
import { UserTokenDto } from 'components/authentication/dtos/user-token-dto';
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

  firstLetter: string = '';
  userName: string = '';

  logOut() {
    this._auth.logOut();
    this._router.navigateByUrl('login');
  }

  ngOnInit(): void {

    const isAuthenticated: UserTokenDto = JSON.parse(localStorage.getItem("myUser") ?? '{}');

    this.firstLetter = isAuthenticated.userName.substring(0, 1) ?? '';

    this.userName = isAuthenticated.userName?.split('@')[0] ?? '';
  }
}

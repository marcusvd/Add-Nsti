import { CommonModule, UpperCasePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { Router, RouterModule } from '@angular/router';
import { UserTokenDto } from 'components/authentication/dtos/user-token-dto';
import { LoginService } from 'components/authentication/services/login.service';



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
    UpperCasePipe,
    RouterModule
]
})
export class SideMenuTopLargeComponent implements OnInit {

  constructor(
    private _auth: LoginService,
    private _router: Router
  ) { }

  firstLetter!: string;
  userName!: string;
  businessId!: number;
  route!:string;
  roles!:string[];


  logOut() {
    this._auth.logOut();
    this._router.navigateByUrl('login');
  }

  ngOnInit(): void {

    const isAuthenticated: UserTokenDto = JSON.parse(localStorage.getItem("myUser") ?? '{}');

    this.roles = isAuthenticated.Roles;

    this.route = `/users/adm-list/${isAuthenticated.businessId}`

    this.firstLetter = isAuthenticated.userName.substring(0, 1) ?? '';

    this.userName = isAuthenticated.userName?.split('@')[0] ?? '';
  }
}

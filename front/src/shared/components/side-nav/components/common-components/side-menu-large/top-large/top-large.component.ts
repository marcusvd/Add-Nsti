import { CommonModule, UpperCasePipe } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { Router, RouterModule } from '@angular/router';
import { UserTokenDto } from 'components/authentication/dtos/user-token-dto';
import { LoginService } from 'components/authentication/services/login.service';
import { ApiResponse } from 'components/authentication/two-factor-enable/dtos/authenticator-setup-response';
import { WarningsService } from 'components/warnings/services/warnings.service';



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

  private _warningsService = inject(WarningsService);
  private _auth = inject(LoginService);
  private _router = inject(Router);
  public isAuthenticated: UserTokenDto = JSON.parse(localStorage.getItem("userToken") ?? '{}');

  userAccountPSDefault: string = `/users/user-account-profile-settings/${this.isAuthenticated.id}`;
  test: string = `/users/select-company-to-start/${this.isAuthenticated.id}`;
  
  firstLetter!: string;
  userName!: string;
  businessId!: number;
  route!: string;
  sysadm = false;
  // roles!:string[];



  logOut() {
    this._auth.logOut().subscribe(
      (x: ApiResponse<string>) => {
        if (x.success) {
          this._warningsService.openSnackBar('Até mais...', 'warnings-success');
          localStorage.clear();
          this._router.navigateByUrl('login');
        }
      }
    );
  }



  ngOnInit(): void {

    this.sysadm = this.isAuthenticated.roles.includes("SYSADMIN");

    this.route = `/users/adm-list/${this.isAuthenticated.businessId}`

    this.firstLetter = this.isAuthenticated.userName.substring(0, 1) ?? '';

    this.userName = this.isAuthenticated.userName?.split('@')[0] ?? '';
  }
}

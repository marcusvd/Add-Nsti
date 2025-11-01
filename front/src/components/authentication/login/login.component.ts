
import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';


import { DefaultCompImports } from 'components/imports/default-comp-imports';
import { WarningsService } from 'components/warnings/services/warnings.service';
import { CaptchaComponent } from 'shared/components/captcha/captcha.component';
import { LoginDto } from '../dtos/login-dto';
import { UserTokenDto } from '../dtos/user-token-dto';
import { AuthLoginImports } from '../imports/auth.imports';
import { ApiResponse } from '../two-factor-enable/dtos/authenticator-setup-response';
import { LoginHelper } from './login-helper';


@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  standalone: true,
  imports: [
    DefaultCompImports,
    AuthLoginImports,
  ],
  providers: [
    WarningsService
  ]
})
export class LoginComponent extends LoginHelper implements OnInit {

  @ViewChild('token') reCaptcha!: CaptchaComponent;

  private _fb = inject(FormBuilder);

  override formMain!: FormGroup;


  ngOnInit(): void {
    this.formLoad();
  }

  inputEmail(email: string) {
    if (email.length == 0)
      this.loginErrorMessage = '';
  }

  inputPwd(pwd: string) {
    if (pwd.length == 0)
      this.loginErrorMessage = '';
  }


  login() {
    if (this.alertSave(this.formMain)) {
      if (this.formMain.valid) {

        const login: LoginDto = { ...this.formMain.value }

        this._loginService?.login$(login)?.subscribe({
          next: (request: ApiResponse<UserTokenDto>) => {
            // console.log(login)
            // console.log(request)
            this.loginCalls(request);

          }, error: (err: any) => {
            console.log(err);
            this.loginsErrorHandler(err, login.email);

          }
        })
      }
    }
  }


  pwdType: string = 'password';
  pwdIcon: string = 'visibility_off';


  pwdHideShow() {
    if (this.pwdType === 'password') {
      this.pwdType = 'text';
      this.pwdIcon = 'visibility';
    } else {
      this.pwdType = 'password';
      this.pwdIcon = 'visibility_off';
    }
  }

  formLoad() {
    return this.formMain = this._fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    })
  }
}

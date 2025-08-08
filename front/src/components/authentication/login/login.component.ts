
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';


import { BaseForm } from '../../../shared/inheritance/forms/base-form';
import { DefaultCompImports } from 'components/imports/default-comp-imports';
import { MyUser } from '../dtos/my-user';
import { AuthLoginImports } from '../imports/auth.imports';
import { AuthService } from '../services/auth.service';
import { LoginDto } from '../dtos/login-dto';
import { LoginService } from '../services/login.service';
import { WarningsService } from 'components/warnings/services/warnings.service';
import { UserTokenDto } from '../dtos/user-token-dto';


@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  standalone: true,
  imports: [
    DefaultCompImports,
    AuthLoginImports
  ]
})
export class LoginComponent extends BaseForm implements OnInit {

  inputEmail(arg0: string) {
    if (arg0.length == 0)
      this.loginErrorMessage = '';
  }

  test() {
    // console.log(this.formMain.get('userName')?.hasError('required'));
    // console.log(this.formMain.get('userName')?.touched);
  }

  constructor(
    public _loginService: LoginService,
    private _router: Router,
    private _fb: FormBuilder,
    private _warningsService: WarningsService,

  ) { super() }

  override formMain!: FormGroup;

  loginErrorMessage: string = '';

  login() {

    this._loginService.login$(this.formMain.value).subscribe({
      next: (user: any) => {


        if (user.authenticated) {

          this.loginErrorMessage = '';
          if (user.action == "TwoFactor") {

            this._router.navigateByUrl('two-factor');

          }

          localStorage.setItem("myUser", JSON.stringify(user));

          this._warningsService.openSnackBar('SEJA BEM-VINDO!', 'warnings-success');


          this._router.navigateByUrl('/');


        }
        else {
        }

      }, error: (err: any) => {
        const erroCode: string = err.error.Message.split('|');
        switch (erroCode[0]) {
          case '1.0': {
            // this.resendEmailConfim(user);
            this.loginErrorMessage = erroCode[1]
            break;
          }
          case '1.4': {
            this._warningsService.openSnackBar(erroCode[1], 'warnings-error');
            this.loginErrorMessage = erroCode[1]
            break;
          }
          case '1.11': {
            this._warningsService.openSnackBar(erroCode[1], 'warnings-error');
            this._warningsService.openAuthWarnings({
               btnLeft: 'Fechar', btnRight: '', title: 'ERRO DE AUTENTICAÇÃO:',
              body: erroCode[1]
            })
            break;
          }
          case '1.6': {
            this._warningsService.openSnackBar(erroCode[1], 'warnings-error');
            this.loginErrorMessage = erroCode[1]
            break;
          }
        }

      }

    })
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

  ngOnInit(): void {
    // this.loginErrorMessage = null;
    // this.loginErrorMessage = '';
    this.formLoad();


  }

}

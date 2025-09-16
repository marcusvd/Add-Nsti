
import { Component, OnInit, ViewChild, viewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';


import { BaseForm } from '../../../shared/inheritance/forms/base-form';
import { DefaultCompImports } from 'components/imports/default-comp-imports';
import { AuthLoginImports } from '../imports/auth.imports';
import { LoginService } from '../services/login.service';
import { WarningsService } from 'components/warnings/services/warnings.service';
import { UserTokenDto } from '../dtos/user-token-dto';
import { MatButtonModule } from '@angular/material/button';
import { CaptchaComponent } from 'shared/components/captcha/captcha.component';


@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  standalone: true,
  imports: [
    DefaultCompImports,
    AuthLoginImports,
  ]
})
export class LoginComponent extends BaseForm implements OnInit {

  @ViewChild('token') reCaptcha!: CaptchaComponent;

  inputEmail(email: string) {
    if (email.length == 0)
      this.loginErrorMessage = '';
  }

  inputPwd(pwd: string) {
    if (pwd.length == 0)
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

  // register(tokenCaptcha: string | undefined) {

  //     const user: any = this.formMain.value;

  //     if (this.alertSave(this.formMain)) {
  //       if (this.formMain.valid && tokenCaptcha)


  login(tokenCaptcha?: string) {

    // if (this.alertSave(this.formMain)) {

    if (this.alertSave(this.formMain)) {
      if (this.formMain.valid && tokenCaptcha)

        this._loginService?.login$(this?.formMain?.value)?.subscribe({
          next: (user: any) => {

            if (user?.authenticated) {

              this.loginErrorMessage = '';
              if (user.action == "TwoFactor")
                this._router.navigateByUrl('two-factor');

              console.log(user as UserTokenDto)

              localStorage.setItem("myUser", JSON.stringify(user));

              this._warningsService.openSnackBar('SEJA BEM-VINDO!', 'warnings-success');


              this._router.navigateByUrl('/');


            }
            else {
            }

          }, error: (err: any) => {


            localStorage.removeItem("myUser");

            const erroMessage: string = err?.error?.Message;
            const erroCode = erroMessage ? erroMessage.split('|') : ['0', 'Falha na comunicação com o servidor.']
            // this.reCaptcha.resetCaptcha();
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
              case '1.15': {
                this._warningsService.openSnackBar(erroCode[1], 'warnings-error');
                this.loginErrorMessage = erroCode[1]
                break;
              }
              case '1.16': {
                this._warningsService.openSnackBar(erroCode[1], 'warnings-error');
                this.loginErrorMessage = erroCode[1]
                break;
              }
              default: {
                this._warningsService.openSnackBar(erroCode[1], 'warnings-error');
                this.loginErrorMessage = erroCode[1];
                break;
              }
            }

          }
        })
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

  ngOnInit(): void {
    this.formLoad();
  }

}

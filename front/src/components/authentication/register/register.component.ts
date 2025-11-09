
import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';


import { environment } from 'environments/environment';
import { RegisterService } from '../services/register.service';

import { WarningsService } from 'components/warnings/services/warnings.service';
import { AddressService } from 'shared/components/address/services/address.service';
import { CpfCnpjComponent } from 'shared/components/administrative/cpf-cnpj/cpf-cnpj.component';
import { CaptchaComponent } from 'shared/components/captcha/captcha.component';
import { IsMobileNumberPipe } from 'shared/pipes/is-mobile-number.pipe';
import { Register } from '../dtos/register';
import { RegisterHelper } from './helper/register-helper';
import { ImportsRegister } from './imports/imports-register';


@Component({
  selector: 'register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  standalone: true,
  imports: [
    ImportsRegister,
    CpfCnpjComponent
  ],
  providers: [
    RegisterService,
    AddressService,
    IsMobileNumberPipe
  ]
})
export class RegisterComponent extends RegisterHelper implements OnInit {

  @ViewChild('token') reCaptcha!: CaptchaComponent;

  _registerService = inject(RegisterService);
  _warningsService = inject(WarningsService);
  _actRoute = inject(ActivatedRoute);

  loginErrorMessage: string = '';

  backend = `${environment._BACK_END_ROOT_URL}/_Register/RegisterAsync`;

  register(tokenCaptcha: string | undefined) {

    const user: Register = this.formMain.value;

    if (this.alertSave(this.formMain)) {
      if (this.formMain.valid && tokenCaptcha) {
        this._registerService.AddUser(user, this.formMain, this.backend)
          .subscribe({
            next: (user) => {
              this._warningsService.openSnackBar('CADASTRADO!' + '   ' + user.email.toUpperCase() + '.', 'warnings-success');

              setTimeout(() => {
                this.callRouter('login');
              }, 3000);

            }, error: (err: any) => {
              this.reCaptcha.resetCaptcha();
              const erroCode: string = err?.error?.Message?.split('|');
              console.log(err)

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

  inputEmail(arg0: string) {
    if (arg0.length == 0)
      this.loginErrorMessage = '';
  }

  back() {
    window.history.back();
  }

  ngOnInit(): void {
    this.formLoadCnpj();
    const email = this._actRoute.snapshot.params['email'];
    this.formMain.get('email')?.setValue(email);
  }

}

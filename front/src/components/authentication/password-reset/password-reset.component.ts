
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';


import { BaseForm } from 'shared/inheritance/forms/base-form';
import { RegisterService } from '../services/register.service';
import { ResetPassword } from '../dtos/reset-password';
import { AccountService } from '../services/account.service';
import { PasswordConfirmationValidator } from '../validators/password-confirmation-validator';
import { PasswordValidator } from '../validators/password-validator';
import { ImportsPasswordReset } from './imports/imports-password-reset';


@Component({
  selector: 'password-reset',
  templateUrl: './password-reset.component.html',
  styleUrls: ['./password-reset.component.scss'],
  standalone: true,
  imports: [
    ImportsPasswordReset
  ],
  providers: [
    RegisterService
  ]
})
export class PasswordResetComponent extends BaseForm implements OnInit {

  override formMain!: FormGroup;
  loginErrorMessage: string = '';

  constructor(
    private _activatedRoute: ActivatedRoute,
    private _accountService: AccountService,
    private _fb: FormBuilder
  ) { super() }


  passwordChange(tokenCaptcha: string | undefined) {

    const resetPassword: ResetPassword = this.formMain.value;

    if (this.formMain.valid && tokenCaptcha) {
      this._accountService.reset(resetPassword);
    }

  }

  formLoad(param: any) {
    this.formMain = this._fb.group({
      token: [param['token'], [Validators.required]],
      email: [param['email'], [Validators.required]],
      userName: [param['userName'], [Validators.required]],
      password: ['', [Validators.required, Validators.minLength(3)]],
      confirmPassword: ['', [Validators.required]],
    }, { validators: [PasswordConfirmationValidator(), PasswordValidator()] })

    return this.formMain;
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

  ngOnInit(): void {
    this._activatedRoute.queryParams.subscribe(param => {
      this.formLoad(param);
    }
    );
  }

}

import { Component, input, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';


import { BaseForm } from 'shared/inheritance/forms/base-form';
import { RegisterService } from '../services/register.service';
import { ResetPassword } from '../dtos/reset-password';
import { AccountService } from '../services/account.service';
import { PasswordConfirmationValidator } from '../validators/password-confirmation-validator';
import { PasswordValidator } from '../validators/password-validator';
import { ImportsPasswordResetAdm } from './imports/imports-password-reset-adm';
import { PasswordFieldComponent } from '../common-components/password/password.component';
import { ResponseIdentiyApiDto } from '../dtos/response-identiy-api-dto';
import { CaptchaComponent } from 'shared/components/captcha/captcha.component';


@Component({
  selector: 'password-reset-adm',
  templateUrl: './password-reset-adm.component.html',
  styleUrls: ['./password-reset-adm.component.scss'],
  standalone: true,
  imports: [
    ImportsPasswordResetAdm,
    PasswordFieldComponent
  ],
  providers: [
    RegisterService
  ]
})
export class PasswordResetAdmComponent extends BaseForm implements OnInit {

  @ViewChild('token') reCaptcha!: CaptchaComponent;

  @Input() override formMain!: FormGroup;
  @Input() email: string | undefined = '';

  loginErrorMessage: string = '';

  constructor(
    private _accountService: AccountService,
    private _fb: FormBuilder
  ) { super() }


  passwordChange() {
    const resetPassword: ResetPassword = this.formMain.value;
    if (this.alertSave(this.formMain)) {
      this._accountService.staticPasswordDefined$(resetPassword).subscribe({
        next: (x => {
          if (x.succeeded) {
            this.formMain.get('password')?.reset();
            this.formMain.get('confirmPassword')?.reset();
            this.openSnackBar('Senha Foi alterada!', 'warnings-success');
          }
        }),
        error: (error => {
          {
            this.openSnackBar(error, 'warnings-error');
          }
        })

      })
    }
    // if (this.alertSave(this.formMain)) {
    //   this._accountService.staticPasswordDefined$(resetPassword).subscribe((x: ResponseIdentiyApiDto) => {
    //     if (x.succeeded) {
    //       this.formMain.get('password')?.reset();
    //       this.formMain.get('confirmPassword')?.reset();

    //       this.openSnackBar('Senha Foi alterada!', 'warnings-success');
    //     }
    //   },
    //   error =>{
    //     this.openSnackBar(error, 'warnings-error');
    //   }
    //   )
    // }
  }

  outPwdField($event: any) {
    const event = $event as InputEvent
  }

  formLoad() {

    this.formMain = this._fb.group({
      email: [this.email, [Validators.required]],
      userName: [this.email, []],
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
    this.formLoad();
    // this._activatedRoute.queryParams.subscribe(param => {
    //   console.log(param)
    // }
    // );
  }

}


import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';


import { BaseForm } from 'shared/inheritance/forms/base-form';
import { PasswordChangeDto } from '../dtos/password-change-dto';
import { AccountService } from '../services/account.service';
import { RegisterService } from '../services/register.service';
import { PasswordConfirmationValidator } from '../validators/password-confirmation-validator';
import { PasswordValidator } from '../validators/password-validator';
import { ImportsPasswordReset } from './imports/imports-password-change';
import { PasswordFieldComponent } from '../common-components/password/password.component';


@Component({
  selector: 'password-change',
  templateUrl: './password-change.component.html',
  styleUrls: ['./password-change.component.scss'],
  standalone: true,
  imports: [
    ImportsPasswordReset,
    PasswordFieldComponent
  ],
  providers: [
    RegisterService
  ]
})
export class PasswordChangeComponent extends BaseForm implements OnChanges {

  override formMain!: FormGroup;

  @Input() userIdRoute!: number;
  @Input() userName!: string;


  pwdType: string = 'password';
  pwdIcon: string = 'visibility_off';

  constructor(
    private _activatedRoute: ActivatedRoute,
    private _accountService: AccountService,
    private _fb: FormBuilder
  ) { super() }

  ngOnChanges(changes: SimpleChanges): void {
    this.formLoad(this.userIdRoute, this.userName);
  }


  passwordChange(tokenCaptcha: string | undefined) {
    const resetPassword: PasswordChangeDto = this.formMain.value;

    this._accountService.passwordChange(resetPassword);

    // if (this.formMain.valid && tokenCaptcha) {
    //   this._accountService.passwordChange(resetPassword);
    // }

  }

  formLoad(userId: number, userName:string) {
    this.formMain = this._fb.group({
      userId: [userId, [Validators.required]],
      userName: [userName, [Validators.required]],
      currentPwd: ['', [Validators.required, Validators.minLength(3)]],
      password: ['', [Validators.required, Validators.minLength(3)]],
      confirmPassword: ['', [Validators.required]],
    }, { validators: [PasswordConfirmationValidator(), PasswordValidator()] })
    return this.formMain;
  }

  // pwdTypeCurrent: string = 'password';
  // pwdIconCurrent: string = 'visibility_off';

  // currentPwdHideShow() {
  //   if (this.pwdTypeCurrent === 'password') {
  //     this.pwdTypeCurrent = 'text';
  //     this.pwdIconCurrent = 'visibility';
  //   } else {
  //     this.pwdTypeCurrent = 'password';
  //     this.pwdIconCurrent = 'visibility_off';
  //   }
  // }


  pwdHideShow() {
    if (this.pwdType === 'password') {
      this.pwdType = 'text';
      this.pwdIcon = 'visibility';
    } else {
      this.pwdType = 'password';
      this.pwdIcon = 'visibility_off';
    }
  }

  // ngOnInit(): void {
  //   this._activatedRoute.queryParams.subscribe(param => {
  //     this.formLoad(1);
  //   }
  //   );
  // }

}

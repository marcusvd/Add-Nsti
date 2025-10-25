
import { Component, OnInit } from '@angular/core';

import { FormControl, FormGroup, Validators } from '@angular/forms';

import { AccountService } from '../services/account.service';
import { ImportsForgot } from './imports/imports-forgot';
import { BaseForm } from 'shared/extends/forms/base-form';
import { ForgotPassword } from '../dtos/forgot-password';

@Component({
  selector: 'forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css'],
  standalone: true,
  imports: [
ImportsForgot
// DefaultCompImports,


// MatInputModule,
// CaptchaComponent,
// MatCardModule,
// ReactiveFormsModule,

// MatDividerModule,
// MatFormFieldModule,
//     RouterModule,

//     NgIf,
//     TitleDescriptionAuthComponent

  ],
  // providers: [CaptchaComponent]
})
export class ForgotPasswordComponent extends BaseForm implements OnInit {

  constructor(
    private _accountService: AccountService
  ) {
    super()
  }


  recovery(tokenCaptcha: string | undefined) {
    if (this.formMain.controls['email'].valid && tokenCaptcha) {
      const forgotMyPassword: ForgotPassword = this.formMain.value;
      this._accountService.forgotMyPassword(forgotMyPassword);
    }
  }

  formLoad() {
    return this.formMain = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
    });
  }
  back() {
    window.history.back();
  }
  ngOnInit(): void {
    this.formLoad();
  }

}

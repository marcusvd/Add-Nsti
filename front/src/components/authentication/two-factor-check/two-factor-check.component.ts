import { Component, inject, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';

import { FormBuilder, Validators } from '@angular/forms';
import { DefaultCompImports } from 'components/imports/default-comp-imports';
import { BaseForm } from 'shared/inheritance/forms/base-form';
import { VerifyTwoFactorRequest } from '../dtos/t2-factor';
import { UserTokenDto } from '../dtos/user-token-dto';
import { AccountService } from '../services/account.service';
import { ApiResponse } from '../two-factor-enable/dtos/authenticator-setup-response';
import { TwoFactorCheckFieldComponent } from './common-components/two-factor-check-field/two-factor-check-field.component';

@Component({
  selector: 'two-factor-check',
  templateUrl: './two-factor-check.component.html',
  styles: [``],
  standalone: true,
  imports: [
    DefaultCompImports,
    TwoFactorCheckFieldComponent,
    RouterModule
  ]
})
export class TwoFactorCheckComponent extends BaseForm implements OnInit {

  private _accountService = inject(AccountService);
  private _fb = inject(FormBuilder);

  twoFactorCheck: VerifyTwoFactorRequest = new VerifyTwoFactorRequest();

  ngOnInit(): void {

    const userToken: UserTokenDto = JSON.parse(localStorage.getItem('userToken') ?? '');

    this.formMain = this._fb.group({
      email: [userToken.email, [Validators.required]],
      provider: ['Email', [Validators.required]],
      code: ['', [Validators.required]],
      rememberMe: [true, []],
    })
  }

  authenticate() {
    this.twoFactorCheck = { ...this.formMain.value };

    this._accountService.twofactorverifyAsync$(this.twoFactorCheck).subscribe(
      {
        next: ((x: ApiResponse<UserTokenDto>) => {
          if (x.success) {
            this.openSnackBar('Autenticação de dois fatores verificada com sucesso.', 'warnings-success');
          }
          localStorage.setItem("userToken", JSON.stringify(x.data));
          console.log(localStorage.getItem("userToken"));
          this.callRouter('/');
        }),
        error: (error => {
          {
            console.log(error)
            this.openSnackBar('Falha na verificação de dois fatores.', 'warnings-error');
          }
        })
      }
    )
  }
}

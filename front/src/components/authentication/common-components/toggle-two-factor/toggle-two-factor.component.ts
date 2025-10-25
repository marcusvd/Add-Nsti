import { Component, inject, Input, OnInit } from '@angular/core';
import { ImportsToogleTwoFactor } from './imports/imports-toogle-two-factor';
import { BaseForm } from 'shared/extends/forms/base-form';
import { AccountService } from 'components/authentication/services/account.service';
import { FormBuilder } from '@angular/forms';
import { MatCheckboxChange } from '@angular/material/checkbox';

import { TwoFactorStatusViewModel, TwoFactorToggleDto } from 'components/authentication/dtos/t2-factor';

@Component({
  selector: 'toggle-two-factor',
  standalone: true,
  imports: [
    ImportsToogleTwoFactor
  ],
  templateUrl: './toggle-two-factor.component.html',
  styleUrl: './toggle-two-factor.component.scss'
})
export class ToggleTwoFactorComponent extends BaseForm implements OnInit {

  @Input() usrId!: number;

  _accountService = inject(AccountService);
  _fb = inject(FormBuilder);

  twoFactorLabel!: string;
  twoFactorLabelCssClass!: string;
  twoFactorIcon!: string;

  ngOnInit(): void {
    this._accountService.IsEnabledTwoFactorAsync$(this.usrId)
      .subscribe({
        next: ((x:TwoFactorStatusViewModel) => {
          console.log(x)
          this.formLoad(x.isEnabled);
          this.twoFactorOnChage(x.isEnabled);
          // this.passwordWillExpires.willExpires = x;
          // this.passwordWillExpires.userId = this.userAccountId
          // this.formLoad(this.passwordWillExpires);
          // this.willExpiresOnChage(x);
        })
      })
    }


  formLoad(x: boolean) {
    this.formMain = this._fb.group({
      userId: [this.usrId, []],
      enable: [x, []],
    })
  }

  twoFactorOnChage(x: boolean) {
    if (x) {
      this.twoFactorLabel = 'Habilitada';
      this.twoFactorIcon = 'check';
      this.twoFactorLabelCssClass = 'text-green-700';
      this.formMain.get('twoFactor')?.setValue(false);
    }
    else {
      this.twoFactorLabel = 'Desativada';
      this.twoFactorIcon = 'close';
      this.twoFactorLabelCssClass = 'text-red-700';
      this.formMain.get('twoFactor')?.setValue(true);
    }

  }

  toggle(x: MatCheckboxChange) {

    const update: TwoFactorToggleDto = this.formMain.value;

    const enabled: string = "Autenticação de dois fatores ativada.";
    const disabled: string = "Autenticação de dois fatores Desativadas.";

    this._accountService.TwoFactorToggle$(update).subscribe(
      {
        next: (x => {
          if (x.succeeded && update.enable)
            this.openSnackBar(enabled, 'warnings-success');
          else
            this.openSnackBar(disabled, 'warnings-alert');

        }),
        error: (error => {
          {
            this.openSnackBar(error, 'warnings-error');
          }
        })
      }
    )

  }




}

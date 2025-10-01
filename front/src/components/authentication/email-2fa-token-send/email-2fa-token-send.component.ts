import { Component, inject, Input, OnInit } from '@angular/core';
import { BaseForm } from 'shared/inheritance/forms/base-form';
import { AccountService } from '../services/account.service';
import { FormBuilder, Validators } from '@angular/forms';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { ResponseIdentiyApiDto } from '../dtos/response-identiy-api-dto';
import { PasswordWillExpiresDto } from '../dtos/password-will-expires-dto';
import { MatDividerModule } from '@angular/material/divider';
import { ImportsEmail2faTokenSend } from './imports/email-2fa-token-send';
import { OnOff2FaCodeViaEmail } from '../dtos/t2-factor';

@Component({
  selector: 'email-2fa-token-send',
  standalone: true,
  imports: [
    ImportsEmail2faTokenSend,
    MatDividerModule
  ],
  templateUrl: './email-2fa-token-send.component.html',
  styleUrl: './email-2fa-token-send.component.scss'
})

export class Email2faTokenSendComponent extends BaseForm implements OnInit {

  @Input({ required: true }) email: string = '';
  @Input({ required: true }) onOff: Date = new Date();

  // @Input() userAccountId!: number;
  // passwordWillExpires = new PasswordWillExpiresDto();

  _accountService = inject(AccountService);
  _fb = inject(FormBuilder);

  code2FaSendEmailLabel!: string;
  code2FaSendEmailLabelCssClass!: string;
  code2FaSendEmailIcon!: string;

  ngOnInit(): void {
    this.checkEnabledEmail(this.onOff, this.email);
    // this._accountService.isPasswordExpires$(this.userAccountId)
    //   .subscribe({
    //     next: (x => {
    //       this.passwordWillExpires.willExpires = x;
    //       this.passwordWillExpires.userId = this.userAccountId
    //       this.formLoad(this.passwordWillExpires);
    //       this.willExpiresOnChage(x);
    //     })
    //   })
  }



  checkEnabledEmail(code2Fa: Date, email: string) {

    const db2Fa = new Date(code2Fa ?? new Date());

    this.formLoad(db2Fa.getFullYear() == this.minDate.getFullYear(), email);

    this.code2FaSendEmailOnChage(db2Fa.getFullYear() == this.minDate.getFullYear());

  }

  formLoad(onOff: boolean, email: string) {
    this.formMain = this._fb.group({
      email: [email, [Validators.required]],
      onOff: [onOff, [Validators.required]],
    })
  }

  code2FaSendEmailOnChage(x: boolean) {
    if (x) {
      this.code2FaSendEmailLabel = 'Será enviado.';
      this.code2FaSendEmailIcon = 'check';
      this.code2FaSendEmailLabelCssClass = 'text-green-700';
      this.formMain.get('onOff')?.setValue(true);
    }
    else {
      this.code2FaSendEmailLabel = 'Não será enviado.';
      this.code2FaSendEmailIcon = 'close';
      this.code2FaSendEmailLabelCssClass = 'text-red-700';
      this.formMain.get('onOff')?.setValue(false);
    }

  }

  actionOnOffEmail(checked: MatCheckboxChange) {

    const update: OnOff2FaCodeViaEmail = this.formMain.value;
    this._accountService.OnOff2FaCodeViaEmailAsync$(update).subscribe(
      {
        next: (x => {
          if (x.succeeded && this.formMain.get('onOff')?.value)
            this.openSnackBar('Será enviado pelo e-mail.', 'warnings-success');
          else
            this.openSnackBar('Não será enviado pelo e-mail.', 'warnings-alert');

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

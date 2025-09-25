import { Component, inject, Input, OnInit } from '@angular/core';
import { BaseForm } from 'shared/inheritance/forms/base-form';
import { AccountService } from '../services/account.service';
import { FormBuilder } from '@angular/forms';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { ResponseIdentiyApiDto } from '../dtos/response-identiy-api-dto';
import { PasswordWillExpiresDto } from '../dtos/password-will-expires-dto';
import { MatDividerModule } from '@angular/material/divider';
import { ImportsEmail2faTokenSend } from './imports/email-2fa-token-send';

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

  @Input() code2FaSendEmail!: Date | undefined;
  // @Input() userAccountId!: number;
  // passwordWillExpires = new PasswordWillExpiresDto();

  _accountService = inject(AccountService);
  _fb = inject(FormBuilder);

  code2FaSendEmailLabel!: string;
  code2FaSendEmailLabelCssClass!: string;
  code2FaSendEmailIcon!: string;

  ngOnInit(): void {
    this.checkEnabledEmail(this.code2FaSendEmail);
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



  checkEnabledEmail(code2Fa?: Date) {

    const db2Fa = new Date(code2Fa ?? new Date());

      this.formLoad(db2Fa.getFullYear() == this.minDate.getFullYear());

  }


  formLoad(x?: boolean) {
    this.formMain = this._fb.group({
      userId: ['x?.userId', []],
      code2FaSendEmail: [x, []],
    })
  }

  code2FaSendEmailOnChage(x: boolean) {
    if (x) {
      this.code2FaSendEmailLabel = 'A senha expira no próximo login. (SENHA: 123456)';
      this.code2FaSendEmailIcon = 'close';
      this.code2FaSendEmailLabelCssClass = 'text-red-700';
      this.formMain.get('code2FaSendEmail')?.setValue(true);
    }
    else {
      this.code2FaSendEmailLabel = 'Senha está definida.';
      this.code2FaSendEmailIcon = 'check';
      this.code2FaSendEmailLabelCssClass = 'text-green-700';
      this.formMain.get('code2FaSendEmail')?.setValue(false);
    }

  }

  actionExpire(x: MatCheckboxChange) {

    const update: PasswordWillExpiresDto = this.formMain.value;

    this._accountService.markPasswordExpire$(update).subscribe(
      {
        next: (x => {
          if (x.succeeded)
            this.openSnackBar('A senha expira no primeiro login.', 'warnings-alert');
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

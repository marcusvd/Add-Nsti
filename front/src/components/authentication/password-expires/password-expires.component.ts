import { Component, inject, Input, OnInit } from '@angular/core';
import { BaseForm } from 'shared/inheritance/forms/base-form';
import { AccountService } from '../services/account.service';
import { FormBuilder } from '@angular/forms';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { ResponseIdentiyApiDto } from '../dtos/response-identiy-api-dto';
import { ImportsPasswordExpires } from './imports/imports-password-expires';
import { PasswordWillExpiresDto } from '../dtos/password-will-expires-dto';
import { MatDividerModule } from '@angular/material/divider';

@Component({
  selector: 'password-expires',
  standalone: true,
  imports: [
    ImportsPasswordExpires,
    MatDividerModule
  ],
  templateUrl: './password-expires.component.html',
  styleUrl: './password-expires.component.scss'
})

export class PasswordExpiresComponent extends BaseForm implements OnInit {

  @Input() willExpires!: Date;
  @Input() userAccountId!: number;
  passwordWillExpires = new PasswordWillExpiresDto();

  _accountService = inject(AccountService);
  _fb = inject(FormBuilder);

  willExpiresLabel!: string;
  willExpiresLabelCssClass!: string;
  willExpiresIcon!: string;

  ngOnInit(): void {
    this._accountService.isPasswordExpires$(this.userAccountId)
      .subscribe({
        next: (x => {
          this.passwordWillExpires.willExpires = x;
          this.passwordWillExpires.userId = this.userAccountId
          this.formLoad(this.passwordWillExpires);
          this.willExpiresOnChage(x);
        })
      })
  }






  formLoad(x: PasswordWillExpiresDto) {
    this.formMain = this._fb.group({
      userId: [x?.userId, []],
      willExpires: [x?.willExpires, []],
    })
  }

  willExpiresOnChage(x: boolean) {
    if (x) {
      this.willExpiresLabel = 'A senha expira no próximo login. (SENHA: 123456)';
      this.willExpiresIcon = 'close';
      this.willExpiresLabelCssClass = 'text-red-700';
      this.formMain.get('willExpires')?.setValue(true);
    }
    else {
      this.willExpiresLabel = 'Senha está definida.';
      this.willExpiresIcon = 'check';
      this.willExpiresLabelCssClass = 'text-green-700';
      this.formMain.get('willExpires')?.setValue(false);
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

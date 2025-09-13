import { AfterViewInit, Component, inject, Input, OnInit } from '@angular/core';
import { ImportsAccountStatus } from './imports/imports-account-status';
import { BaseForm } from 'shared/inheritance/forms/base-form';
import { AccountService } from '../services/account.service';
import { AccountStatusDto } from '../dtos/account-status-dto';
import { FormBuilder } from '@angular/forms';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { EmailConfirmManualDto } from '../dtos/email-confirm-manual-dto';
import { ResponseIdentiyApiDto } from '../dtos/response-identiy-api-dto';
import { AccountLockedOutManualDto } from '../dtos/account-locked-out-manual-dto';

@Component({
  selector: 'account-status',
  standalone: true,
  imports: [ImportsAccountStatus],
  templateUrl: './account-status.component.html',
  styleUrl: './account-status.component.scss'
})

export class AccountStatusComponent extends BaseForm implements OnInit, AfterViewInit {
  ngAfterViewInit(): void {
    this.neverLoggedIn();
  }


  ngOnInit(): void {
    this.startInitial();
  }

  @Input() userName!: string;
  @Input() lastLogin!: Date | string;
  _accountService = inject(AccountService);
  _fb = inject(FormBuilder);
  emailConfirmManual = new EmailConfirmManualDto();
  accountLockedOutManual = new AccountLockedOutManualDto();

  lockedOutLabel!: string;
  lockedOutLabelCssClass!: string;
  lockedOutIcon!: string;

  neverLoggedInLabel!: string;
  neverLoggedInLabelCssClass!: string;
  neverLoggedInIcon!: string;
  dateString: boolean = false;

  emailConfirmedLabel!: string;
  emailConfirmedLabelCssClass!: string;
  emailConfirmedIcon!: string;

  formLoad(x: AccountStatusDto) {
    this.formMain = this._fb.group({
      isAccountLockedOut: [x.isAccountLockedOut, []],
      isEmailConfirmed: [x.isEmailConfirmed, []],
    })
  }

  startInitial() {
    this._accountService.getAccountStatus$(this.userName)
      .subscribe(x => {
        this.formLoad(x);
        this.lockedOutOnChage(x.isAccountLockedOut);
        this.emailConfirmedOnChage(x.isEmailConfirmed);
      })
  }

  lockedOutOnChage(x: boolean) {

    if (x) {
      this.lockedOutLabel = 'Bloqueado';
      this.lockedOutIcon = 'lock';
      this.lockedOutLabelCssClass = 'text-red-700';
      this.formMain.get('isAccountLockedOut')?.setValue(true)
    }
    else {
      this.lockedOutLabel = 'Desbloqueado';
      this.lockedOutIcon = 'lock_open';
      this.lockedOutLabelCssClass = 'text-green-700';
      this.formMain.get('isAccountLockedOut')?.setValue(false)
    }
  }

  neverLoggedIn() {

    const lLogin = new Date(this.lastLogin);

    if (lLogin.getUTCFullYear() == this.minDate.getUTCFullYear()) {
      this.lastLogin = 'Nunca';
      this.dateString = true;
      this.neverLoggedInIcon = 'cancel';
      this.neverLoggedInLabelCssClass = 'text-red-700';
    }
    else {
      this.neverLoggedInLabel = 'Desbloqueado';
      this.neverLoggedInIcon = 'access_alarm';
      this.neverLoggedInLabelCssClass = 'text-green-700';
      this.dateString = false;
    }


  }


  emailConfirmedOnChage(x: boolean) {
    if (x) {
      this.emailConfirmedLabel = 'Confirmado';
      this.emailConfirmedIcon = 'check';
      this.emailConfirmedLabelCssClass = 'text-green-700';
      this.formMain.get('isEmailConfirmed')?.setValue(true);
    }
    else {
      this.emailConfirmedLabel = 'Não confirmado';
      this.emailConfirmedIcon = 'close';
      this.emailConfirmedLabelCssClass = 'text-red-700';
      this.formMain.get('isEmailConfirmed')?.setValue(false);
    }

  }

  actionEmailConfirm(change: MatCheckboxChange) {

    this.emailConfirmManual.email = this.userName;
    this.emailConfirmManual.emailConfirmed = change.checked;

    this._accountService.updateAccountStatusEmailConfirm$(this.emailConfirmManual).subscribe(
      {

        next: (x => {
          if (x.succeeded) {
            this.startInitial();
            if (change.checked)
              this.openSnackBar('CONFIRMADO!', 'warnings-success');
            else
              this.openSnackBar('NÃO CONFIRMADO!', 'warnings-alert');

          }
        }),
        error: (error => {
          {
            this.openSnackBar(error, 'warnings-error');
          }
        })
      }
    )
  }


  actionLockedOut(change: MatCheckboxChange) {

    this.accountLockedOutManual.email = this.userName;
    this.accountLockedOutManual.accountLockedOut = change.checked;

    this._accountService.updateAccountLockedOutManual$(this.accountLockedOutManual).subscribe(
      {
        next: (x => {
          if (x.succeeded) {
            this.startInitial();
            if (change.checked)
              this.openSnackBar('BLOQUEADO!', 'warnings-alert');
            else
              this.openSnackBar('DESBLOQUEADO!', 'warnings-success');

          }
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

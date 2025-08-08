import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, RouterModule } from '@angular/router';

import { AccountService } from '../services/account.service';
import { CommonModule } from '@angular/common';
import { ConfirmEmail } from '../dtos/confirm-email';
import { DefaultCompImports } from 'components/imports/default-comp-imports';





@Component({
  selector: 'confirm-email',
  templateUrl: './confirm-email.component.html',
  styles: [``],
  standalone: true,
  imports: [
    DefaultCompImports,
    RouterModule
  ]
})
export class ConfirmEmailComponent implements OnInit {

  // formMain: FormGroup;
  result!: ConfirmEmail;
  constructor(
    private _accountService: AccountService,
    private _activatedRoute: ActivatedRoute
  ) { }


  ngOnInit(): void {

    this._activatedRoute.queryParams.subscribe(param => {

      let confirmEmail: ConfirmEmail = new ConfirmEmail();
      confirmEmail.token = param['token']
      confirmEmail.email = param['email']

      this._accountService.confirmEmail(confirmEmail);

      this.result = confirmEmail
    })
  }
}

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';

import { DefaultCompImports } from 'components/imports/default-comp-imports';
import { ConfirmEmail } from '../dtos/confirm-email';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'first-register-email-confirm-check-token',
  templateUrl: './first-register-email-confirm-check-token.component.html',
  styles: [``],
  standalone: true,
  imports: [
    DefaultCompImports,
    RouterModule
  ]
})
export class FirstRegisterEmailConfirmCheckTokenComponent implements OnInit {

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

      console.log(param);

      this._accountService.firstEmailConfirmationCheckTokenAsync(confirmEmail);

      this.result = confirmEmail
    })
  }
}

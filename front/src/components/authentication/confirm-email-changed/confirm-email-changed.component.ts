import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, RouterModule } from '@angular/router';

import { AccountService } from '../services/account.service';
import { CommonModule } from '@angular/common';
import { ConfirmEmail } from '../dtos/confirm-email';
import { DefaultCompImports } from 'components/imports/default-comp-imports';
import { ConfirmEmailChangeDto } from '../dtos/confirm-email-change-dto';





@Component({
  selector: 'confirm-email-change',
  template: `
  <div *ngIf="this._accountService.result" class="w-full p-50 m-50 grid grid-rows-1 justify-center items-center">
    <div class="p-40"></div>
        <div class="pt-50 mt-50">
            <h1 class="text-color-main">Mudança de e-mail confirmada!</h1>
            <h3>{{'Novo: '+result.email}}</h3>
        </div>
  </div>
  <div *ngIf="!this._accountService.result" class="w-full p-50 m-50 grid grid-rows-1 justify-center items-center">
    <div class="p-40"></div>
        <div class="pt-50 mt-50">
            <h1 class="text-red-600">Mudança de e-mail não confirmada, falha geral!</h1>
            <h3>{{'Tente novamente ou entre em contato com o suporte!'}}</h3>
        </div>
  </div>
  `,
  styles: [``],
  standalone: true,
  imports: [
    DefaultCompImports,
    RouterModule
  ]
})
export class ConfirmEmailChangedComponent implements OnInit {

  result!: ConfirmEmail;
  constructor(
    public _accountService: AccountService,
    private _activatedRoute: ActivatedRoute
  ) { }


  ngOnInit(): void {

    this._activatedRoute.queryParams.subscribe(param => {

      let confirmEmail: ConfirmEmailChangeDto = new ConfirmEmailChangeDto();
      confirmEmail.id = param['id']
      confirmEmail.token = param['token']
      confirmEmail.email = param['email']

      this._accountService.confirmEmailChange(confirmEmail);
      console.log(confirmEmail);
      this.result = confirmEmail
    })
  }
}

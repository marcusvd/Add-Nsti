import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';

import { DefaultCompImports } from 'components/imports/default-comp-imports';
import { TwoFactorCheckDto } from '../dtos/two-factor-check-dto';
import { AccountService } from '../services/account.service';
import { BaseForm } from 'shared/inheritance/forms/base-form';





@Component({
  selector: 'two-factor-check',
  templateUrl: './two-factor-check.component.html',
  styles: [``],
  standalone: true,
  imports: [
    DefaultCompImports,
    RouterModule
  ]
})
export class TwoFactorCheckComponent extends BaseForm implements OnInit {

  // formMain: FormGroup;
  // result!: TwoFactorCheckDto;
  constructor(
    private _accountService: AccountService,
    private _activatedRoute: ActivatedRoute
  ) { super() }


  ngOnInit(): void {


    let twoFactorCheck: TwoFactorCheckDto = new TwoFactorCheckDto();
    twoFactorCheck.token = this._activatedRoute.snapshot.params['token'];
    twoFactorCheck.email = this._activatedRoute.snapshot.params['email'];

    this._accountService.twoFactorCheckTokenAsync$(twoFactorCheck).subscribe(
      {
        next: (x => {

          console.log(x)
          if (x.succeeded)
            this.openSnackBar('Autenticação de dois fatores verificada com sucesso.', 'warnings-success');
        }),
        error: (error => {
          {
            console.log(error)
            this.openSnackBar('Falha na verificação de dois fatores.', 'warnings-error');
          }
        })
      }
    )


    console.log(twoFactorCheck)
  }
}

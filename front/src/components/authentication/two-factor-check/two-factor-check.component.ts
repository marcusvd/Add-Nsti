import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';

import { DefaultCompImports } from 'components/imports/default-comp-imports';
import { AccountService } from '../services/account.service';
import { BaseForm } from 'shared/inheritance/forms/base-form';
import { VerifyTwoFactorRequest } from '../dtos/t2-factor';
import { TwoFactorCheckFieldComponent } from './common-components/two-factor-check-field/two-factor-check-field.component';
import { FormBuilder, Validators } from '@angular/forms';

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

  // formMain: FormGroup;
  // result!: TwoFactorCheckDto;
  constructor(
    private _accountService: AccountService,
    private _activatedRoute: ActivatedRoute,
    private _fb: FormBuilder
  ) { super() }

  twoFactorCheck: VerifyTwoFactorRequest = new VerifyTwoFactorRequest();
  
  ngOnInit(): void {

    // this.twoFactorCheck.token = this._activatedRoute.snapshot.params['token'];
    const email = this._activatedRoute.snapshot.params['email'];
    // twoFactorCheck.email = this._activatedRoute.snapshot.params['email'];

    this.formMain = this._fb.group({

      email: [email, [Validators.required]],
      provider: ['Email', [Validators.required]],
      token: ['', [Validators.required]],
      rememberMe: [true, []],
    })


  }




  authenticate() {
    this.twoFactorCheck = {...this.formMain.value};
    // console.log(this.twoFactorCheck)
    this._accountService.twofactorverifyAsync$(this.twoFactorCheck).subscribe(
      {
        next: (x => {
          console.log(x)
          if (x.success)
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

  }
}

import { Component, inject, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';

import { FormBuilder, Validators } from '@angular/forms';
import { DefaultCompImports } from 'components/imports/default-comp-imports';
import { BaseForm } from 'shared/extends/forms/base-form';
import { VerifyTwoFactorRequest } from '../dtos/t2-factor';
import { UserTokenDto } from '../dtos/user-token-dto';
import { AccountService } from '../services/account.service';
import { ApiResponse } from '../two-factor-enable/dtos/authenticator-setup-response';
import { TwoFactorCheckFieldComponent } from './common-components/two-factor-check-field/two-factor-check-field.component';
import { CompanyService } from "components/company/services/company.service";
import { CompaniesQts } from '../dtos/companies-qts';

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

  private _accountService = inject(AccountService);

  private _companyService = inject(CompanyService);
  private _fb = inject(FormBuilder);

  twoFactorCheck: VerifyTwoFactorRequest = new VerifyTwoFactorRequest();

  ngOnInit(): void {

    const userToken: UserTokenDto = JSON.parse(localStorage.getItem('userToken') ?? '');

    this.formMain = this._fb.group({
      email: [userToken.email, [Validators.required]],
      provider: ['Email', [Validators.required]],
      code: ['', [Validators.required]],
      rememberMe: [true, []],
    })
  }

  authenticate() {
    this.twoFactorCheck = { ...this.formMain.value };

    this._accountService.twoFactorVerifyAsync$(this.twoFactorCheck).subscribe(
      {
        next: ((request: ApiResponse<UserTokenDto>) => {
          if (request.success) {
            this.openSnackBar('Autenticação de dois fatores verificada com sucesso.', 'warnings-success');
            this._companyService.companyMoreThenOne$(request?.data?.id)
              .subscribe(
                {
                  next: (x: ApiResponse<CompaniesQts>) => {
                    if (x.data.name != '') {
                      this.callRouter('/users', null)
                      localStorage.setItem("selectedCompany", JSON.stringify(x.data));
                    }
                    else
                      this.callRouter(`/select-company-to-start/${request?.data?.id}`);

                    localStorage.setItem('selectedCompany', JSON.stringify(x.data))
                  },
                  error: (error: any) => console.log(error)
                }
              )
          }
          localStorage.setItem("userToken", JSON.stringify(request.data));
          // console.log(localStorage.getItem("userToken"));
          this.callRouter('/');
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

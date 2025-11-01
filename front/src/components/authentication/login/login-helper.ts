import { BaseForm } from "shared/extends/forms/base-form";
import { ApiResponse } from "../two-factor-enable/dtos/authenticator-setup-response";
import { UserTokenDto } from "../dtos/user-token-dto";
import { inject } from "@angular/core";
import { WarningsService } from "components/warnings/services/warnings.service";
import { ResendConfirmEmail } from "../dtos/resend-confirm-email";
import { LoginService } from "../services/login.service";
import { CompaniesQts } from "../dtos/companies-qts";
import { CompanyService } from "components/company/services/company.service";
export class LoginHelper extends BaseForm {

  protected _loginService = inject(LoginService);
  private _warningsService = inject(WarningsService);
  private _companyService = inject(CompanyService);

  loginErrorMessage: string = '';
  nTry: number = 1;
  // selectCompany: boolean = false;


  loginCalls(request: ApiResponse<UserTokenDto>) {
    if (request?.success) {

      localStorage.setItem("userToken", JSON.stringify(request.data));

      this.loginErrorMessage = '';


      this._companyService.companyMoreThenOne$(request?.data?.id).subscribe(
        {
          next: (x: ApiResponse<CompaniesQts>) => {
            if (x.data.amountCompanies == 1) {
              this._warningsService.openSnackBar(`SEJA BEM-VINDO À ${x.data.name}`, 'warnings-success');
              this.fullScreen();
            }

          },
          error: (error: any) => console.log(error)
        }
      )




      if (request.data.action == "TwoFactor")
        this.callRouter(`two-factor-check`, null);
      else {
        this._companyService.companyMoreThenOne$(request?.data?.id).subscribe(
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
    }
    else {
      localStorage?.removeItem("userToken");
      this._warningsService.openSnackBar('Usuário ou senha incorreto.', 'warnings-error');
      this.loginErrorMessage = this.nTry > 2 ? 'Usuário bloqueado!' : 'Usuário ou senha incorreto. ' + 'total de tentativas 3. TENTATIVAS->' + (this.nTry++);
    }
  }

  loginsErrorHandler(error: any, email: string) {

    localStorage.removeItem("userToken");

    const erroMessage: string = error?.error?.Message;
    const erroCode = erroMessage ? erroMessage.split('|') : ['0', 'Falha na comunicação com o servidor.'];

    switch (erroCode[0]) {
      case '1.0': {
        // this.resendConfirmEmail(email);
        this.loginErrorMessage = erroCode[1]
        break;
      }
      case '100.3': {
        //100.3 user not found;
        this._warningsService.openSnackBar('Usuário ou senha incorreto.', 'warnings-error');
        this.loginErrorMessage = 'Usuário ou senha incorreto.'
        break;
      }
      case '1.4': {
        //1.4 user or pwd incorrect;
        this._warningsService.openSnackBar(erroCode[1], 'warnings-error');
        this.loginErrorMessage = erroCode[1]
        break;
      }
      case '1.11': {
        this._warningsService.openSnackBar(erroCode[1], 'warnings-error');
        this._warningsService.openAuthWarnings({ btnLeft: 'Fechar', btnRight: '', title: 'ERRO DE AUTENTICAÇÃO:', body: erroCode[1] })
        this.loginErrorMessage = erroCode[1];
        break;
      }
      case '1.6': {
        this._warningsService.openSnackBar(erroCode[1], 'warnings-error');
        this.loginErrorMessage = erroCode[1]
        break;
      }
      case '1.15': {
        this._warningsService.openSnackBar(erroCode[1], 'warnings-error');
        this.loginErrorMessage = erroCode[1]
        break;
      }
      case '1.16': {
        this._warningsService.openSnackBar(erroCode[1], 'warnings-error');
        this.loginErrorMessage = erroCode[1]
        break;
      }
      default: {
        this._warningsService.openSnackBar(erroCode[1], 'warnings-error');
        this.loginErrorMessage = erroCode[1];
        break;
      }
    }
  }

  // resendConfirmEmail(email: string) {
  //   const resend = new ResendConfirmEmail();
  //   resend.email = email;
  //   this._loginService?.resendConfirmEmailAsync(resend)?.subscribe({
  //     next: (request: ApiResponse<UserTokenDto>) => {
  //       console.log(request);
  //     }, error: (err: any) => {
  //       console.log(err);
  //     }
  //   });
  // }

}

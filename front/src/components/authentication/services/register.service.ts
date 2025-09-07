import { inject, Injectable } from "@angular/core";
import { BackEndService } from "shared/services/back-end/backend.service";
import { take } from "rxjs";
import { FormGroup } from "@angular/forms";
import { RegisterDto } from "../dtos/register-dto";
import { AddUserExistingCompanyDto } from "../dtos/add-user-existing-company-dto";
import { UserTokenDto } from "../dtos/user-token-dto";
import { UpdateUserAccountEmailDto } from "../dtos/update-user-account-email-dto";
import { WarningsService } from "components/warnings/services/warnings.service";


@Injectable({ providedIn: 'root' })


export class RegisterService extends BackEndService<RegisterDto> {

  private _warningsService  = inject(WarningsService);

  constructor(
  ) { super() }

  AddUser(user: RegisterDto, form: FormGroup, url: string) {
    return this.add$<RegisterDto>(user, url).pipe(take(1));
  }


  RequestEmailChange(user: UpdateUserAccountEmailDto, url: string) {
    this.add$<UpdateUserAccountEmailDto>(user, url).pipe(take(1))
      .subscribe({
        next: () => {

          this.openSnackBar('E-mail mudança de email enviado com sucesso.', 'warnings-success');

          setTimeout(() => {

            this._warningsService.openAuthWarnings({
              btnLeft: 'Fechar', btnRight: '', title: 'AVISO:',
              body: 'E-mail mudança de email enviado com sucesso.',
            }).subscribe(result => {
              // this.callRouter('login');
            })

          }, 5000);

        }, error: (err: any) => {

          this.openSnackBar('Falha ao enviar email de mudança de e-mail.', 'warnings-error');

          setTimeout(() => {

            this._warningsService.openAuthWarnings({
              btnLeft: 'Fechar', btnRight: '', title: 'AVISO:',
              body: 'Falha ao enviar email de mudança de e-mail.',
            }).subscribe(result => {
              console.log(result)
            })

          }, 5000);

        }
      })
  }



  AddUserInAExistingCompany(user: AddUserExistingCompanyDto, url: string) {
    return this.update$<UserTokenDto>(url, user).pipe(take(1));
  }

}

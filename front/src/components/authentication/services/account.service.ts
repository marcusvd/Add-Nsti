
import { Injectable } from '@angular/core';

import { WarningsService } from 'components/warnings/services/warnings.service';
import { environment } from 'environments/environment';
import { Observable, take } from 'rxjs';
import { BackEndService } from '../../../shared/services/back-end/backend.service';
import { UserAccountAuthDto } from "../../authentication/dtos/user-account-auth-dto";
import { AccountLockedOutManualDto } from '../dtos/account-locked-out-manual-dto';
import { AccountStatusDto } from '../dtos/account-status-dto';
import { ConfirmEmail } from '../dtos/confirm-email';
import { ConfirmEmailChangeDto } from '../dtos/confirm-email-change-dto';
import { TimedAccessControlDto } from '../dtos/date-time-access-control-dto';
import { TimedAccessControlStartEndPostDto } from '../dtos/date-time-access-control-start-end-post-dto';
import { EmailConfirmManualDto } from '../dtos/email-confirm-manual-dto';
import { ForgotPassword } from '../dtos/forgot-password';
import { PasswordChangeDto } from '../dtos/password-change-dto';
import { PasswordWillExpiresDto } from '../dtos/password-will-expires-dto';
import { ResetPassword } from '../dtos/reset-password';
import { ResetStaticPasswordDto } from '../dtos/reset-static-password-dto';
import { ResponseIdentiyApiDto } from '../dtos/response-identiy-api-dto';
import { OnOff2FaCodeViaEmail, ToggleAuthenticatorRequestViewModel, TwoFactorStatusViewModel, TwoFactorToggleDto, VerifyTwoFactorRequest } from '../dtos/t2-factor';
import { ApiResponse } from '../two-factor-enable/dtos/authenticator-setup-response';
import { AuthenticatorSetupResponse } from '../two-factor-setup/interfaces/authenticator-setup-response';
import { UserTokenDto } from '../dtos/user-token-dto';


@Injectable({
  providedIn: 'root'
})

export class AccountService extends BackEndService<UserAccountAuthDto> {
  constructor(
    private _warningsService: WarningsService,
  ) { super() }

  result: boolean = false;

  firstEmailConfirmationCheckTokenAsync(confirmEmail: ConfirmEmail) {
    return this.add$<ConfirmEmail>(confirmEmail, `${environment._BACK_END_ROOT_URL}/_EmailUserAccount/FirstEmailConfirmationCheckTokenAsync`).pipe(take(1)).subscribe({
      next: (x: any) => {
        const result = x as ApiResponse<UserTokenDto>;

        localStorage.setItem("userToken", JSON.stringify(result.data));

        console.log(result);

        this.openSnackBar('E-mail Confirmado com sucesso.', 'warnings-success');

        if (result.data.action == "FIRSTREGISTER") {
          this._router.navigateByUrl(`/registerFull/${confirmEmail.email}`)
        }
        else
          this._router.navigateByUrl("/unknown-route")
      }, error: (err: any) => {

        this.openSnackBar('Falha ao confirmar e-mail.', 'warnings-error');

        setTimeout(() => {

          this._warningsService.openAuthWarnings({
            btnLeft: 'Fechar', btnRight: '', title: 'AVISO:',
            body: 'Falha ao confirmar e-mail.',
          }).subscribe(result => {
            console.log(result)
          })

        }, 5000);

      }
    })
  }

  confirmEmail(confirmEmail: ConfirmEmail) {
    return this.add$<ConfirmEmail>(confirmEmail, `${environment._BACK_END_ROOT_URL}/_EmailUserAccount/ConfirmEmailAddress`).pipe(take(1)).subscribe({
      next: () => {

        this.openSnackBar('E-mail Confirmado com sucesso.', 'warnings-success');

        setTimeout(() => {
          this._warningsService.openAuthWarnings({
            btnLeft: 'Fechar', btnRight: '', title: 'AVISO:',
            body: 'E-mail Confirmado com sucesso.',
          }).subscribe(result => {
            this.callRouter('login');
          })

        }, 5000);

      }, error: (err: any) => {

        this.openSnackBar('Falha ao confirmar e-mail.', 'warnings-error');

        setTimeout(() => {

          this._warningsService.openAuthWarnings({
            btnLeft: 'Fechar', btnRight: '', title: 'AVISO:',
            body: 'Falha ao confirmar e-mail.',
          }).subscribe(result => {
            console.log(result)
          })

        }, 5000);

      }
    })

  }

  confirmEmailChange(confirmEmail: ConfirmEmailChangeDto) {

    return this.add$<ConfirmEmailChangeDto>(confirmEmail, `${environment._BACK_END_ROOT_URL}/_EmailUserAccount/ConfirmRequestEmailChange`).pipe(take(1)).subscribe({
      next: (x) => {

        let result: any = x;
        result = result as ResponseIdentiyApiDto;

        this.result = result.succeeded;

        if (result.succeeded) {

          this.openSnackBar('E-mail Confirmado com sucesso.', 'warnings-success');

          setTimeout(() => {

            this._warningsService.openAuthWarnings({
              btnLeft: 'Fechar', btnRight: '', title: 'AVISO:',
              body: 'E-mail Confirmado com sucesso.',
            }).subscribe(result => {
              this._router.navigateByUrl('login');
              this.callRouter('login');
            })

          }, 5000);
        }

        if (!result.succeeded)
          this.failConfirmEmailChange('Falha ao confirmar e-mail.');

      }, error: (err: any) => {

        this.failConfirmEmailChange(err);

      }
    })

  }

  private failConfirmEmailChange(error: any) {

    this.openSnackBar(error, 'warnings-error');

    setTimeout(() => {

      this._warningsService.openAuthWarnings({
        btnLeft: 'Fechar', btnRight: '', title: 'AVISO:',
        body: error,
      }).subscribe(
        //   result => {
        //   console.log(result)
        // }
      )

    }, 5000);
  }

  forgotMyPassword(forgotPassword: ForgotPassword) {
    return this.add$<ForgotPassword>(forgotPassword, `${environment._BACK_END_ROOT_URL}/_Passwords/ForgotPassword`).pipe(take(1)).subscribe({
      next: () => {

        this.openSnackBar('E-mail recuperação de senha enviado com sucesso.', 'warnings-success');

        setTimeout(() => {
          this._warningsService.openAuthWarnings({
            btnLeft: 'Fechar', btnRight: '', title: 'AVISO:',
            body: 'E-mail recuperação de senha enviado com sucesso. Verifique seu e-mail para redefinir sua senha. Caixa de entrada, Spam ou lixo eletrônico. Obrigado!',
          }).subscribe(result => {
            this.callRouter('login');
          })

        }, 5000);

      }, error: (err: any) => {

        const erroCode: string = err.error.Message.split('|');


        switch (erroCode[0]) {
          case 'User not found.': {
            this.openSnackBar('E-mail recuperação de senha enviado com sucesso.', 'warnings-success');

            setTimeout(() => {
              this._warningsService.openAuthWarnings({
                btnLeft: 'Fechar', btnRight: '', title: 'AVISO:',
                body: 'E-mail recuperação de senha enviado com sucesso. Verifique seu e-mail para redefinir sua senha. Caixa de entrada, Spam ou lixo eletrônico. Obrigado!',

              }).subscribe(result => {
                this.callRouter('login');
              })

            }, 5000);
            break;
          }
        }

        if (erroCode[0] != 'User not found.') {
          this.openSnackBar('E-mail recuperação de senha enviado com sucesso.', 'warnings-success');

          setTimeout(() => {

            this._warningsService.openAuthWarnings({
              btnLeft: 'Fechar', btnRight: '', title: 'AVISO:',
              body: 'E-mail recuperação de senha enviado com sucesso. Verifique seu e-mail para redefinir sua senha. Caixa de entrada, Spam ou lixo eletrônico. Obrigado!',

            }).subscribe(result => {
              this.callRouter('login');
              // this._router.navigateByUrl('login');
            })

          }, 5000);

        }



















      }
    })
  }

  reset(resetPassword: ResetPassword) {
    return this.add$(resetPassword, `${environment._BACK_END_ROOT_URL}/auth/ResetPasswordAsync`).pipe(take(1)).subscribe({
      next: () => {
        this.openSnackBar('A senha foi modificada com sucesso!', 'warnings-success');

        setTimeout(() => {

          this._warningsService.openAuthWarnings({
            btnLeft: 'Fechar', btnRight: '', title: 'AVISO:',
            body: 'A senha foi modificada com sucesso!',
          }).subscribe(result => {
            this.callRouter('/login');
          })

        }, 5000);

        this.callRouter('/');
        this.callRouter('/login');
      }, error: (err: any) => {
        const erroCode: string = err.error.Message.split('|');
        switch (erroCode[0]) {
          case '1.12': {

            break;
          }

        }
      }
    })
  }

  passwordChange(passwordChange: PasswordChangeDto) {

    return this.add$<PasswordChangeDto>(passwordChange, `${environment._PASSWORDS_CONTROLLER}/PasswordChangeAsync`).pipe(take(1)).subscribe({
      next: (x) => {

        let result: any = x;
        result = result as ResponseIdentiyApiDto;

        this.result = result.succeeded;

        if (result.succeeded) {

          this.openSnackBar('Senha modificada com sucesso.', 'warnings-success');

          setTimeout(() => {

            this._warningsService.openAuthWarnings({
              btnLeft: 'Fechar', btnRight: '', title: 'AVISO:',
              body: 'Senha modificada com sucesso.',
            }).subscribe(result => {
              this._router.navigateByUrl('login');
              this.callRouter('login');
            })

          }, 5000);
        }

        if (!result.succeeded)
          this.failConfirmEmailChange('Falha ao modificar senha.');

      }, error: (err: any) => {
        console.log(err)
        this.failConfirmEmailChange(err);

      }
    })

  }

  getAccountStatus$(email: string) {
    return this.loadByName$<AccountStatusDto>(`${environment._USER_ACCOUNTS_CONTROLLER}/GetAccountStatusAsync`, email)
  }

  updateAccountStatusEmailConfirm$(emailConfirmManual: EmailConfirmManualDto) {
    return this.updateV2$<ApiResponse<ResponseIdentiyApiDto>>(`${environment._EMAIL_USERACCOUNT_CONTROLLER}/ManualConfirmEmailAddressAsync`, emailConfirmManual)
  }

  updateAccountLockedOutManual$(accountLockedOutManual: AccountLockedOutManualDto) {
    return this.updateV2$<ResponseIdentiyApiDto>(`${environment._USER_ACCOUNTS_CONTROLLER}/ManualAccountLockedOutAsync`, accountLockedOutManual)
  }

  markPasswordExpire$(passwordWillExpires: PasswordWillExpiresDto) {
    return this.updateV2$<ResponseIdentiyApiDto>(`${environment._PASSWORDS_CONTROLLER}/MarkPasswordExpireAsync`, passwordWillExpires)
  }

  TwoFactorToggle$(toggleTwoFactor: TwoFactorToggleDto) {
    return this.updateV2$<ResponseIdentiyApiDto>(`${environment._TWOFACTOR_AUTHENTICATION_CONTROLLER}/TwoFactorToggleAsync`, toggleTwoFactor)
  }

  isPasswordExpires$(id: number) {
    return this.loadById$<ApiResponse<boolean>>(`${environment._PASSWORDS_CONTROLLER}/IsPasswordExpiresAsync`, id.toString())
  }

  staticPasswordDefined$(reset: ResetStaticPasswordDto) {
    return this.updateV2$<ResponseIdentiyApiDto>(`${environment._PASSWORDS_CONTROLLER}/staticPasswordDefined`, reset)
  }

  timedAccessControlStartEndAsync$(timedAccessControlStartEnd: TimedAccessControlStartEndPostDto) {
    return this.update$<any>(`${environment._USER_ACCOUNT_TIMED_ACCESS_CONTROLLER}/TimedAccessControlStartEndAsync`, timedAccessControlStartEnd);
  }

  getTimedAccessControlAsync$(userId: number) {
    return this.loadById$<TimedAccessControlDto>(`${environment._USER_ACCOUNT_TIMED_ACCESS_CONTROLLER}/getTimedAccessControlAsync`, userId.toString())
  }

  twoFactorVerifyAsync$(twoFactorCheck: VerifyTwoFactorRequest) {
    return this.add$<ApiResponse<any>>(twoFactorCheck, `${environment._TWOFACTOR_AUTHENTICATION_CONTROLLER}/TwoFactorVerifyAsync`);
  }

  IsEnabledTwoFactorAsync$(id: number) {
    return this.loadById$<TwoFactorStatusViewModel>(`${environment._TWOFACTOR_AUTHENTICATION_CONTROLLER}/GetTwoFactorStatusAsync`, id.toString())
  }

  GetAuthenticatorSetup(): Observable<AuthenticatorSetupResponse> {
    return this.load$<AuthenticatorSetupResponse>(`${environment._TWOFACTOR_AUTHENTICATION_CONTROLLER}/GetAuthenticatorSetupAsync`);
  }

  enableAuthenticator(request: ToggleAuthenticatorRequestViewModel): Observable<ApiResponse<any>> {
    return this.add$<ApiResponse<any>>(request, `${environment._TWOFACTOR_AUTHENTICATION_CONTROLLER}/EnableAuthenticatorAsync`);
  }

  OnOff2FaCodeViaEmailAsync$(request: OnOff2FaCodeViaEmail): Observable<ResponseIdentiyApiDto> {
    return this.updateV2$<ResponseIdentiyApiDto>(`${environment._TWOFACTOR_AUTHENTICATION_CONTROLLER}/OnOff2FaCodeViaEmailAsync`, request);
  }

}




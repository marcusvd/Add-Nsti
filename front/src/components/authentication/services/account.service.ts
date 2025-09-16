
import { inject, Injectable } from '@angular/core';


import { WarningsService } from 'components/warnings/services/warnings.service';
import { take } from 'rxjs';
import { BackEndService } from '../../../shared/services/back-end/backend.service';
import { ConfirmEmail } from '../dtos/confirm-email';
import { UserAccountAuthDto } from "../../authentication/dtos/user-account-auth-dto";
import { environment } from 'environments/environment';
import { Router } from '@angular/router';
import { ForgotPassword } from '../dtos/forgot-password';
import { ResetPassword } from '../dtos/reset-password';
import { ResponseIdentiyApiDto } from '../dtos/response-identiy-api-dto';
import { ConfirmEmailChangeDto } from '../dtos/confirm-email-change-dto';
import { PasswordChangeDto } from '../dtos/password-change-dto';
import { AccountStatusDto } from '../dtos/account-status-dto';
import { EmailConfirmManualDto } from '../dtos/email-confirm-manual-dto';
import { AccountLockedOutManualDto } from '../dtos/account-locked-out-manual-dto';
import { PasswordWillExpiresDto } from '../dtos/password-will-expires-dto';
import { ResetStaticPasswordDto } from '../dtos/reset-static-password-dto';
import { TimedAccessControlStartEndPostDto } from '../dtos/date-time-access-control-start-end-post-dto';
import { TimedAccessControlDto } from '../dtos/date-time-access-control-dto';
import { ToggleTwoFactorDto } from '../dtos/toggle-two-factor-dto';
import { TwoFactorCheckDto } from '../dtos/two-factor-check-dto';


@Injectable({
  providedIn: 'root'
})

export class AccountService extends BackEndService<UserAccountAuthDto> {
  constructor(
    private _warningsService: WarningsService,
  ) { super() }

  result: boolean = false;

  confirmEmail(confirmEmail: ConfirmEmail) {
    return this.add$<ConfirmEmail>(confirmEmail, `${environment._BACK_END_ROOT_URL}/auth/confirmEmailAddress`).pipe(take(1)).subscribe({
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

    return this.add$<ConfirmEmailChangeDto>(confirmEmail, `${environment._BACK_END_ROOT_URL}/auth/ConfirmRequestEmailChange`).pipe(take(1)).subscribe({
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
    return this.add$<ForgotPassword>(forgotPassword, `${environment._BACK_END_ROOT_URL}/auth/ForgotPassword`).pipe(take(1)).subscribe({
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

    return this.add$<PasswordChangeDto>(passwordChange, `${environment._BACK_END_ROOT_URL}/authadm/PasswordChangeAsync`).pipe(take(1)).subscribe({
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
    return this.loadByName$<AccountStatusDto>(`${environment._BACK_END_ROOT_URL}/authadm/GetAccountStatus`, email)
  }

  updateAccountStatusEmailConfirm$(emailConfirmManual: EmailConfirmManualDto) {
    return this.updateV2$<ResponseIdentiyApiDto>(`${environment._BACK_END_ROOT_URL}/authadm/ManualConfirmEmailAddress`, emailConfirmManual)
  }

  updateAccountLockedOutManual$(accountLockedOutManual: AccountLockedOutManualDto) {
    return this.updateV2$<ResponseIdentiyApiDto>(`${environment._BACK_END_ROOT_URL}/authadm/ManualAccountLockedOut`, accountLockedOutManual)
  }

  markPasswordExpire$(passwordWillExpires: PasswordWillExpiresDto) {
    return this.updateV2$<ResponseIdentiyApiDto>(`${environment._BACK_END_ROOT_URL}/authadm/MarkPasswordExpireAsync`, passwordWillExpires)
  }

  ToggleTwoFactor$(toggleTwoFactor: ToggleTwoFactorDto) {
    return this.updateV2$<ResponseIdentiyApiDto>(`${environment._BACK_END_ROOT_URL}/authadm/ToggleTwoFactorAsync`, toggleTwoFactor)
  }

  isPasswordExpires$(id: number) {
    return this.loadById$<boolean>(`${environment._BACK_END_ROOT_URL}/authadm/IsPasswordExpiresAsync`, id.toString())
  }

  IsEnabledTwoFactorAsync$(id: number) {
    return this.loadById$<boolean>(`${environment._BACK_END_ROOT_URL}/authadm/IsEnabledTwoFactorAsync`, id.toString())
  }

  staticPasswordDefined$(reset: ResetStaticPasswordDto) {
    return this.updateV2$<ResponseIdentiyApiDto>(`${environment._BACK_END_ROOT_URL}/authadm/staticPasswordDefined`, reset)
  }

  timedAccessControlStartEndPostAsync$(timedAccessControlStartEnd: TimedAccessControlStartEndPostDto) {
    return this.add$<any>(timedAccessControlStartEnd, `${environment._BACK_END_ROOT_URL}/authadm/TimedAccessControlStartEndPostAsync`)
  }

  getTimedAccessControlAsync$(userId: number) {
    return this.loadById$<TimedAccessControlDto>(`${environment._BACK_END_ROOT_URL}/authadm/getTimedAccessControlAsync`, userId.toString())
  }

  twoFactorCheckTokenAsync$(twoFactorCheck: TwoFactorCheckDto) {
    return this.add$<ResponseIdentiyApiDto>(twoFactorCheck, `${environment._BACK_END_ROOT_URL}/auth/twofactorverify`);
  }




}





import { Injectable } from '@angular/core';


import { WarningsService } from 'components/warnings/services/warnings.service';
import { take } from 'rxjs';
import { BackEndService } from '../../../shared/services/back-end/backend.service';
import { ConfirmEmail } from '../dtos/confirm-email';
import { MyUser } from '../dtos/my-user';
import { environment } from 'environments/environment';
import { Router } from '@angular/router';
import { ForgotPassword } from '../dtos/forgot-password';
import { ResetPassword } from '../dtos/reset-password';


@Injectable({
  providedIn: 'root'
})

export class AccountService extends BackEndService<MyUser> {

  constructor(
    private _warningsService: WarningsService,
    private _router: Router
  ) { super() }


  confirmEmail(confirmEmail: ConfirmEmail) {
    return this.add$<ConfirmEmail>(confirmEmail, `${environment._BACK_END_ROOT_URL}/auth/confirmEmailAddress`).pipe(take(1)).subscribe({
      next: () => {

        this.openSnackBar('E-mail Confirmado com sucesso.', 'warnings-success');

        setTimeout(() => {

          this._warningsService.openAuthWarnings({
            btnLeft: 'Fechar', btnRight: '', title: 'AVISO:',
            body: 'E-mail Confirmado com sucesso.',
          }).subscribe(result => {
            this._router.navigateByUrl('login');
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

  forgotMyPassword(forgotPassword: ForgotPassword) {
    return this.add$<ForgotPassword>(forgotPassword, `${environment._BACK_END_ROOT_URL}/auth/ForgotPassword`).pipe(take(1)).subscribe({
      next: () => {

        this.openSnackBar('E-mail recuperação de senha enviado com sucesso.', 'warnings-success');

        setTimeout(() => {

          this._warningsService.openAuthWarnings({
            btnLeft: 'Fechar', btnRight: '', title: 'AVISO:',
            body: 'E-mail recuperação de senha enviado com sucesso. Verifique seu e-mail para redefinir sua senha. Caixa de entrada, Spam ou lixo eletrônico. Obrigado!',
          }).subscribe(result => {
            this._router.navigateByUrl('login');
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
                this._router.navigateByUrl('login');
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
              this._router.navigateByUrl('login');
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
            this._router.navigateByUrl('login');
          })

        }, 5000);

        this._router.navigate((['/']));
        this._router.navigateByUrl('/login');
      }, error: (err: any) => {
        const erroCode: string = err.error.Message.split('|');
        switch (erroCode[0]) {
          case '1.12': {
            // this._communicationsAlerts.defaultSnackMsg(erroCode[1], 1, null, 4);
            // this._communicationsAlerts.communicationCustomized({
            //   'message': erroCode[1],
            //   'action': '',
            //   'style': 'red-snackBar-error',
            //   'delay': '',
            //   'positionVertical': 'center',
            //   'positionHorizontal': 'top',
            // });
            // this.openAuthWarnings({ btn1: 'Fechar', btn2: '', messageBody: erroCode[1] })
            break;
          }

        }
      }
    })
  }

}




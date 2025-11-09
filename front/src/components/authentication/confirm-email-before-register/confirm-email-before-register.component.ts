
import { Component, OnInit } from '@angular/core';


import { environment } from 'environments/environment';
import { RegisterService } from '../services/register.service';
import { WarningsService } from 'components/warnings/services/warnings.service';
import { AddressService } from 'shared/components/address/services/address.service';
import { FirstConfirmEmailRegisterDto } from './dtos/first-confirm-email-register-dto';
import { ConfirmEmailBeforeRegisterHelper } from './helper/confirm-email-before-register-helper';
import { ImportsConfirmEmailBeforeRegisterHelper } from './imports/imports-confirm-email-before-register';


@Component({
  selector: 'confirm-email-before-register',
  templateUrl: './confirm-email-before-register.component.html',
  styleUrls: ['./confirm-email-before-register.component.css'],
  standalone: true,
  imports: [
    ImportsConfirmEmailBeforeRegisterHelper,
  ],
  providers: [
    RegisterService,
    AddressService,

  ]
})
export class ConfirmEmailBeforeRegisterComponent extends ConfirmEmailBeforeRegisterHelper implements OnInit {

  constructor(
    private _registerService: RegisterService,
    private _warningsService: WarningsService,

  ) { super() }

  loginErrorMessage: string = '';

  backend = `${environment._BACK_END_ROOT_URL}/_Register/FirstConfirmEmailRegisterAsync`;

  register() {

    const dto: FirstConfirmEmailRegisterDto = this.formMain.value;
    if (this.alertSave(this.formMain)) {
      if (this.formMain.valid) {
        this._registerService.FirstEmailComfirm(dto, this.formMain, this.backend)
          .subscribe({
            next: (response) => {
              console.log(response)
              this._warningsService.openSnackBar('Link de cadastro enviado:' + '   ' + dto.email.toUpperCase() + '.', 'warnings-success');

              setTimeout(() => {
                this._warningsService.openAuthWarnings({
                  btnLeft: 'Fechar', btnRight: '', title: 'AVISO:',
                  body: "Verifique seu e-mail para concluir seu registro. Caixa de entrada, Spam ou lixo eletrônico. Obrigado!",
                }).subscribe(result => {
                  this.callRouter('login');
                  // this._router.navigateByUrl('login');
                })
              },1);
            }, error: (err: any) => {
              const erroCode: string = err?.error?.Message?.split('|');
              console.log(err)
            }
          })
      }
    }

  }



  inputEmail(arg0: string) {
    if (arg0.length == 0)
      this.loginErrorMessage = '';
  }

  back() {
    window.history.back();
  }

  ngOnInit(): void {
    this.formLoad();
  }

}

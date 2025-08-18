
import { Component, inject, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';


import { environment } from 'environments/environment';
import { BaseForm } from 'shared/inheritance/forms/base-form';
// import { AdmService } from '../services/adm.service';

import { PasswordConfirmationValidator } from '../../validators/password-confirmation-validator';
import { PasswordValidator } from '../../validators/password-validator';
// import { IsUserRegistereValidator } from '../../authentication/is-user-registered-validator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { WarningsService } from 'components/warnings/services/warnings.service';
import { AdmService } from '../../services/adm.service';
import { ImportsListAdm } from '../imports/imports-list-adm';
import { ListControlAdm } from '../helpers/list-control-adm';


@Component({
  selector: 'list-adm',
  templateUrl: './list-adm.component.html',
  styleUrls: ['./list-adm.component.scss'],
  standalone: true,
  imports: [
    ImportsListAdm
  ],
  providers: [
    AdmService
  ]
})
export class ListAdmComponent extends ListControlAdm implements OnInit {

  constructor(
    // private _admService: AdmService,
    // private _fb: FormBuilder,
    // // private _isUserAdmedValidator: IsUserRegistereValidator,
    // private _router: Router,
    // private _warningsService: WarningsService,
    // private _snackBar: MatSnackBar
  ) { super() }


  private _admService = inject(AdmService);
  private _fb =  inject(FormBuilder);
  private _warningsService =  inject(WarningsService);
  private _snackBar =  inject(MatSnackBar)


  loginErrorMessage: string = '';

  backend = `${environment._BACK_END_ROOT_URL}/auth/AdmAsync`

  // admTest() {

  //   this.openSnackBar('CADASTRADO!' +'   '+ 'MARCUSMVD@HOTMAIL.COM.BR' + '.', 'warnings-success');

  //   setTimeout(() => {

  //     this.openAuthWarnings({
  //       btn1: 'Fechar', btn2: '', title: 'AVISO:',
  //       messageBody: "Verifique seu e-mail para confirmar seu registro. Caixa de entrada, Spam ou lixo eletrônico. Obrigado!",
  //       next: true, action: 'openLogin'
  //     })

  //   }, 5000);



  // }

  // adm(tokenCaptcha: string | undefined) {

  //   const user: any = this.formMain.value;

  //   if (this.alertSave(this.formMain)) {
  //     if (this.formMain.valid && tokenCaptcha) {
  //       this._admService.AddUser(user, this.formMain, this.backend)
  //         .subscribe({
  //           next: (user) => {
  //             console.log(user)
  //             this._warningsService.openSnackBar('CADASTRADO!' + '   ' + user.email.toUpperCase() + '.', 'warnings-success');

  //             setTimeout(() => {
  //               this._warningsService.openAuthWarnings({
  //                 btnLeft: 'Fechar', btnRight: '', title: 'AVISO:',
  //                 body: "Verifique seu e-mail para confirmar seu registro. Caixa de entrada, Spam ou lixo eletrônico. Obrigado!",
  //               }).subscribe(result => {
  //                 this._router.navigateByUrl('login');
  //               })

  //             }, 5000);

  //           }, error: (err: any) => {
  //             console.log(err)
  //             const erroCode: string = err?.error?.Message?.split('|');
  //             console.log(erroCode)
  //             // switch (erroCode[0]) {
  //             //   case '1.1': {
  //             //     this._communicationsAlerts.defaultSnackMsg(erroCode[1], 1, null, 4);
  //             //     // this._communicationsAlerts.communicationCustomized({
  //             //     //   'message': erroCode[1],
  //             //     //   'action': '',
  //             //     //   'delay': '3',
  //             //     //   'style': 'red-snackBar-error',
  //             //     //   'positionVertical': 'center',
  //             //     //   'positionHorizontal': 'top',
  //             //     // });
  //             //     this._errorMessage.next(erroCode[1])
  //             //     form.controls['email'].setErrors({ errorEmailDuplicated: true })
  //             //     break;
  //             //   }
  //             //   case '1.2': {
  //             //     console.log(err);
  //             //     this._communicationsAlerts.defaultSnackMsg(erroCode[1], 1, null, 4);
  //             //     // this._communicationsAlerts.communicationCustomized({
  //             //     //   'message': erroCode[1],
  //             //     //   'action': '',
  //             //     //   'delay': '3',
  //             //     //   'style': 'red-snackBar-error',
  //             //     //   'positionVertical': 'center',
  //             //     //   'positionHorizontal': 'top',
  //             //     // });
  //             //     this._errorMessage.next(erroCode[1])
  //             //     form.controls['userName'].setErrors({ errorUserNameDuplicated: true })
  //             //     break;
  //             //   }
  //             //   case '200.0': {
  //             //     console.log(err);
  //             //     this._communicationsAlerts.defaultSnackMsg(erroCode[1], 1, null, 4);
  //             //     // this._communicationsAlerts.communicationCustomized({
  //             //     //   'message': erroCode[1],
  //             //     //   'action': '',
  //             //     //   'style': 'red-snackBar-error',
  //             //     //   'delay': '3',
  //             //     //   'positionVertical': 'center',
  //             //     //   'positionHorizontal': 'top',
  //             //     // });
  //             //     this.openAuthWarnings({ btn1: 'Fechar', btn2: '', title: 'Erro de autenticação', messageBody: erroCode[1] })
  //             //     break;
  //             //   }
  //             //   case '1.7': {
  //             //     console.log(err);
  //             //     this._communicationsAlerts.defaultSnackMsg(erroCode[1], 1, null, 4);
  //             //     // this._communicationsAlerts.communicationCustomized({
  //             //     //   'message': erroCode[1],
  //             //     //   'action': '',
  //             //     //   'style': 'red-snackBar-error',
  //             //     //   'delay': '3',
  //             //     //   'positionVertical': 'center',
  //             //     //   'positionHorizontal': 'top',
  //             //     // });

  //             //     this.openAuthWarnings({ btn1: 'Fechar', btn2: '', title: 'Erro de autenticação', messageBody: erroCode[1] })
  //             //     break;
  //             //   }
  //             // }
  //           }
  //         })


  //       // .subscribe((x: string) => {
  //       //   this.loginErrorMessage = x;
  //       //   // this._communicationsAlerts.defaultSnackMsg('7', 0);
  //       //   console.log(x)
  //       // })
  //     }

  //   }
  // }

  // formLoad() {
  //   return this.formMain = this._fb.group({
  //     userName: ['', [Validators.required, Validators.minLength(3)]],
  //     companyName: ['', [Validators.required, Validators.minLength(3)]],
  //     email: new FormControl('', { validators: [Validators.required, Validators.maxLength(50), Validators.email], asyncValidators: [] }),
  //     // email: new FormControl('', { validators: [Validators.required, Validators.maxLength(50), Validators.email], asyncValidators: [this._isUserAdmedValidator.validate.bind(this._isUserAdmedValidator)] }),
  //     password: ['', [Validators.required, Validators.minLength(3)]],
  //     confirmPassword: ['', [Validators.required]],
  //   }, { validators: [PasswordConfirmationValidator(), PasswordValidator()] })
  // }

  pwdType: string = 'password';
  pwdIcon: string = 'visibility_off';


  pwdHideShow() {
    if (this.pwdType === 'password') {
      this.pwdType = 'text';
      this.pwdIcon = 'visibility';
    } else {
      this.pwdType = 'password';
      this.pwdIcon = 'visibility_off';
    }
  }

  inputEmail(arg0: string) {
    if (arg0.length == 0)
      this.loginErrorMessage = '';
  }



  // openSnackBar(message: string, style: string, action: string = 'Fechar', duration: number = 5000, horizontalPosition: any = 'center', verticalPosition: any = 'top') {
  //   this._snackBar?.open(message, action, {
  //     duration: duration, // Tempo em milissegundos (5 segundos)
  //     panelClass: [style], // Aplica a classe personalizada
  //     horizontalPosition: horizontalPosition, // Centraliza horizontalmente
  //     verticalPosition: verticalPosition, // Posição vertical (pode ser 'top' ou 'bottom')
  //   });
  // }
  back() {
    window.history.back();
  }

  ngOnInit(): void {
    // this.formLoad();
  }

}

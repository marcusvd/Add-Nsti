
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';


import { BaseForm } from 'shared/inheritance/forms/base-form';
import { RegisterService } from '../services/register.service';
import { ImportsRegister } from './imports/imports-register';
import { RegisterDto } from '../dtos/register-dto';
import { RECAPTCHA_SETTINGS, RecaptchaSettings } from 'ng-recaptcha';



@Component({
  selector: 'register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  standalone: true,
  imports: [
    ImportsRegister
  ],
  providers:[
    RegisterService
  ]
})
export class RegisterComponent extends BaseForm implements OnInit {

  constructor(
    private _registerService: RegisterService,
    private _fb: FormBuilder,
  ) { super() }


  public loginErrorMessage: string = '';

  register(tokenCaptcha: string | undefined) {
    const user: any = this.formMain.value;
    if (this.alertSave(this.formMain)) {
      if (this.formMain.valid && tokenCaptcha) {
        this._registerService.AddUser(user, this.formMain)
          .subscribe({
            next: (user: RegisterDto) => {
              // this._communicationsAlerts.defaultSnackMsg('7', 0, null, 4);
              // this.openAuthWarnings({
              //   btn1: 'Fechar', btn2: '', title: 'AVISO:',
              //   messageBody: "Verifique seu e-mail para confirmar seu registro. Caixa de entrada, Spam ou lixo eletrônico. Obrigado!",
              //   next: true, action: 'openLogin'
              // })
            }, error: (err: any) => {
              const erroCode: string = err.error.Message.split('|');
              console.log(err)
              console.log(erroCode)
              // switch (erroCode[0]) {
              //   case '1.1': {
              //     this._communicationsAlerts.defaultSnackMsg(erroCode[1], 1, null, 4);
              //     // this._communicationsAlerts.communicationCustomized({
              //     //   'message': erroCode[1],
              //     //   'action': '',
              //     //   'delay': '3',
              //     //   'style': 'red-snackBar-error',
              //     //   'positionVertical': 'center',
              //     //   'positionHorizontal': 'top',
              //     // });
              //     this._errorMessage.next(erroCode[1])
              //     form.controls['email'].setErrors({ errorEmailDuplicated: true })
              //     break;
              //   }
              //   case '1.2': {
              //     console.log(err);
              //     this._communicationsAlerts.defaultSnackMsg(erroCode[1], 1, null, 4);
              //     // this._communicationsAlerts.communicationCustomized({
              //     //   'message': erroCode[1],
              //     //   'action': '',
              //     //   'delay': '3',
              //     //   'style': 'red-snackBar-error',
              //     //   'positionVertical': 'center',
              //     //   'positionHorizontal': 'top',
              //     // });
              //     this._errorMessage.next(erroCode[1])
              //     form.controls['userName'].setErrors({ errorUserNameDuplicated: true })
              //     break;
              //   }
              //   case '200.0': {
              //     console.log(err);
              //     this._communicationsAlerts.defaultSnackMsg(erroCode[1], 1, null, 4);
              //     // this._communicationsAlerts.communicationCustomized({
              //     //   'message': erroCode[1],
              //     //   'action': '',
              //     //   'style': 'red-snackBar-error',
              //     //   'delay': '3',
              //     //   'positionVertical': 'center',
              //     //   'positionHorizontal': 'top',
              //     // });
              //     this.openAuthWarnings({ btn1: 'Fechar', btn2: '', title: 'Erro de autenticação', messageBody: erroCode[1] })
              //     break;
              //   }
              //   case '1.7': {
              //     console.log(err);
              //     this._communicationsAlerts.defaultSnackMsg(erroCode[1], 1, null, 4);
              //     // this._communicationsAlerts.communicationCustomized({
              //     //   'message': erroCode[1],
              //     //   'action': '',
              //     //   'style': 'red-snackBar-error',
              //     //   'delay': '3',
              //     //   'positionVertical': 'center',
              //     //   'positionHorizontal': 'top',
              //     // });

              //     this.openAuthWarnings({ btn1: 'Fechar', btn2: '', title: 'Erro de autenticação', messageBody: erroCode[1] })
              //     break;
              //   }
              // }
            }
          })


        // .subscribe((x: string) => {
        //   this.loginErrorMessage = x;
        //   // this._communicationsAlerts.defaultSnackMsg('7', 0);
        //   console.log(x)
        // })
      }

    }
  }

  formLoad() {
    return this.formMain = this._fb.group({
      // company: this.formCompany(),
      userName: ['', [Validators.required]],
      email: ['', [Validators.required]],
      password: ['', [Validators.required]],
      confirmPassword: ['', [Validators.required]],
    })
  }

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

  // formCompany() {
  //   return this.subForm = this._fb.group({
  //     name: ['', [Validators.required]]
  //   })
  // }

  back() {
    window.history.back();
  }

  ngOnInit(): void {
    this.formLoad();
  }

}


import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';


import { environment } from 'environments/environment';
import { BaseForm } from 'shared/inheritance/forms/base-form';


import { ImportsaddUserCompany } from './imports/imports-add-user-company';

import { MatSnackBar } from '@angular/material/snack-bar';
import { WarningsService } from 'components/warnings/services/warnings.service';
import { ActivatedRoute, Router } from '@angular/router';
import { RegisterService } from 'components/authentication/services/register.service';
import { IsUserRegisteredValidator } from 'components/authentication/validators/is-user-registered-validator';
import { PasswordConfirmationValidator } from 'components/authentication/validators/password-confirmation-validator';
import { PasswordValidator } from 'components/authentication/validators/password-validator';
import { CompanyService } from '../../../authentication/services/company.service';
import { CompanyAuth } from 'components/authentication/dtos/company-auth';
import { AddUserExistingCompanyDto } from 'components/authentication/dtos/add-user-existing-company-dto';


@Component({
  selector: 'add-user-company',
  templateUrl: './add-user-company.component.html',
  styleUrls: ['./add-user-company.component.css'],
  standalone: true,
  imports: [
    ImportsaddUserCompany
  ],
  providers: [
    RegisterService
  ]
})
export class AddUserCompanyComponent extends BaseForm implements OnInit {

  constructor(
    private _registerService: RegisterService,
    private _addUserCompanyService: CompanyService,
    private _fb: FormBuilder,
    private _isUserRegisteredValidator: IsUserRegisteredValidator,
    private _router: Router,
    private _actRouter: ActivatedRoute,
    private _warningsService: WarningsService,
    private _snackBar: MatSnackBar
  ) { super() }


  loginErrorMessage: string = '';
  businessId!:number;

  backend = `${environment._BACK_END_ROOT_URL}/AuthAdm/AddUserAccountAsync`

  // add-user-companyTest() {

  //   this.openSnackBar('CADASTRADO!' +'   '+ 'MARCUSMVD@HOTMAIL.COM.BR' + '.', 'warnings-success');

  //   setTimeout(() => {

  //     this.openAuthWarnings({
  //       btn1: 'Fechar', btn2: '', title: 'AVISO:',
  //       messageBody: "Verifique seu e-mail para confirmar seu registro. Caixa de entrada, Spam ou lixo eletrônico. Obrigado!",
  //       next: true, action: 'openLogin'
  //     })

  //   }, 5000);



  // }

  register() {

    const user: AddUserExistingCompanyDto = this.formMain.value;

    if (this.alertSave(this.formMain)) {
      if (this.formMain.valid) {
        this._registerService.AddUserInAExistingCompany(user, this.backend)
          .subscribe({
            next: (user) => {
              this._warningsService.openSnackBar('CADASTRADO!' + '   ' + user.email.toUpperCase() + '.', 'warnings-success');

              this._router.navigateByUrl(`users/adm-list/${this.businessId}`);
              // setTimeout(() => {
              //   this._warningsService.openAuthWarnings({
              //     btnLeft: 'Fechar', btnRight: '', title: 'AVISO:',
              //     body: "Verifique seu e-mail para confirmar seu registro. Caixa de entrada, Spam ou lixo eletrônico. Obrigado!",
              //   }).subscribe(result => {
              //     this._router.navigateByUrl('login');
              //   })

              // }, 5000);

            }, error: (err: any) => {
              console.log(err)
              const erroCode: string = err?.error?.Message?.split('|');
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

  formLoad(x?: CompanyAuth) {
    return this.formMain = this._fb.group({
      id:[x?.id, [Validators.required]],
      companyAuthId:[x?.id, [Validators.required]],
      userName: ['', [Validators.required, Validators.minLength(3)]],
      companyName: [x?.name, [Validators.required, Validators.minLength(3)]],
      email: new FormControl('', { validators: [Validators.required, Validators.maxLength(50), Validators.email], asyncValidators: [this._isUserRegisteredValidator.validate.bind(this._isUserRegisteredValidator)] }),
      password: ['', [Validators.required, Validators.minLength(3)]],
      confirmPassword: ['', [Validators.required]],
    }, { validators: [PasswordConfirmationValidator(), PasswordValidator()] })
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

  back() {
    window.history.back();
  }

  ngOnInit(): void {
    const id = this._actRouter.snapshot.params['id'] as number;
    const backend = `${environment._BACK_END_ROOT_URL}/authadm/GetCompanyAuthAsync`
    this._addUserCompanyService.loadById$<CompanyAuth>(backend, id.toString()).subscribe((x: CompanyAuth) => {
      this.formLoad(x);
      this.businessId = x.businessId;
    })

  }

}

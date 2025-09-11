
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';


import { environment } from 'environments/environment';
import { RegisterService } from '../services/register.service';

import { WarningsService } from 'components/warnings/services/warnings.service';
import { AddressService } from 'shared/components/address/services/address.service';
import { CpfCnpjComponent } from 'shared/components/administrative/cpf-cnpj/cpf-cnpj.component';
import { CaptchaComponent } from 'shared/components/captcha/captcha.component';
import { ContactService } from 'shared/components/contact/services/contact.service';
import { IsMobileNumberPipe } from 'shared/pipes/is-mobile-number.pipe';
import { IsUserRegisteredValidator } from '../validators/is-user-registered-validator';
import { PasswordConfirmationValidator } from '../validators/password-confirmation-validator';
import { PasswordValidator } from '../validators/password-validator';
import { RegisterHelper } from './helper/register-helper';
import { ImportsRegister } from './imports/imports-register';


@Component({
  selector: 'register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  standalone: true,
  imports: [
    ImportsRegister,
    CpfCnpjComponent
  ],
  providers: [
    RegisterService,
    AddressService,
    IsMobileNumberPipe
  ]
})
export class RegisterComponent extends RegisterHelper implements OnInit {

  @ViewChild('token') reCaptcha!: CaptchaComponent;

  constructor(
    private _registerService: RegisterService,
    private _fb: FormBuilder,
    private _isUserRegisteredValidator: IsUserRegisteredValidator,
    private _router: Router,
    private _warningsService: WarningsService,
    private _contactService: ContactService,
    override _addressService: AddressService,
  ) { super(_addressService) }


  loginErrorMessage: string = '';

  backend = `${environment._BACK_END_ROOT_URL}/auth/RegisterAsync`;


  register(tokenCaptcha: string | undefined) {

    const user: any = this.formMain.value;

    if (this.alertSave(this.formMain)) {
      if (this.formMain.valid && tokenCaptcha) {
        this._registerService.AddUser(user, this.formMain, this.backend)
          .subscribe({
            next: (user) => {
              this._warningsService.openSnackBar('CADASTRADO!' + '   ' + user.email.toUpperCase() + '.', 'warnings-success');

              setTimeout(() => {
                this._warningsService.openAuthWarnings({
                  btnLeft: 'Fechar', btnRight: '', title: 'AVISO:',
                  body: "Verifique seu e-mail para confirmar seu registro. Caixa de entrada, Spam ou lixo eletrônico. Obrigado!",
                }).subscribe(result => {
                  this._router.navigateByUrl('login');
                })

              }, 5000);

            }, error: (err: any) => {
              this.reCaptcha.resetCaptcha();
              const erroCode: string = err?.error?.Message?.split('|');
              console.log(erroCode)

            }
          })
      }

    }
  }

  formLoad() {
    return this.formMain = this._fb.group({
      userName: ['', [Validators.required, Validators.minLength(3)]],
      companyName: ['', [Validators.required, Validators.minLength(3)]],
      email: new FormControl('', { validators: [Validators.required, Validators.maxLength(50), Validators.email], asyncValidators: [this._isUserRegisteredValidator.validate.bind(this._isUserRegisteredValidator)] }),
      password: ['', [Validators.required, Validators.minLength(3)]],
      cnpj: ['', [Validators.required]],
      confirmPassword: ['', [Validators.required]],
      address: this.address = this._addressService.formLoad(),
      contact: this.contact = this._contactService.formLoad()
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
    this.formLoad();
  }

}

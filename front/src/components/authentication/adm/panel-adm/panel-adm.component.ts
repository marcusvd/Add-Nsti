
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';


import { environment } from 'environments/environment';
import { BaseForm } from 'shared/inheritance/forms/base-form';
// import {  ProfileService } from '../services/profile.service';

// import { PasswordConfirmationValidator } from '../validators/password-confirmation-validator';
// import { PasswordValidator } from '../validators/password-validator';
// import { IsUserRegisteredValidator } from '../validators/is-user-registered-validator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { WarningsService } from 'components/warnings/services/warnings.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ImportsPanelAdm } from './imports/imports-panel-adm';
import { DefaultComponent } from 'shared/components/default-component/default-component';
import { EditCompanyComponent } from 'components/company/components/edit/edit-company.component';
import { ListGComponent } from 'shared/components/list-g/list/list-g.component';
import { ProfileService } from 'components/authentication/services/profile.service';
import { CompanyProfileService, CompanyService } from 'components/authentication/services/company.service';
import { CompanyAuth } from 'components/authentication/dtos/company-auth';
import { ContactService } from 'shared/components/contact/services/contact.service';
import { AddressService } from 'shared/components/address/services/address.service';
import { MatTabsModule } from '@angular/material/tabs';
import { NameCpfCnpjComponent } from 'shared/components/administrative/name-cpf-cnpj/name-cpf-cnpj.component';
import { BusinessData } from 'shared/components/administrative/name-cpf-cnpj/dto/business-data';
import { IsMobileNumberPipe } from 'shared/pipes/is-mobile-number.pipe';
import { AddressComponent } from 'shared/components/address/component/address.component';
import { ContactComponent } from 'shared/components/contact/component/contact.component';
import { CompanyProfile } from 'components/company/dtos/company-profile';


@Component({
  selector: 'panel-adm',
  templateUrl: './panel-adm.component.html',
  styleUrls: ['./panel-adm.component.css'],
  standalone: true,
  imports: [
    ImportsPanelAdm,
    DefaultComponent,
    NameCpfCnpjComponent,
    ListGComponent,
    AddressComponent,
    ContactComponent,
    MatTabsModule
  ],
  providers: [
    ContactService,
    AddressService,
    IsMobileNumberPipe
  ]
})
export class PanelAdmComponent extends BaseForm implements OnInit {

  constructor(
    private _companyService: CompanyService,
    private _companyProfileService: CompanyProfileService,
    private _fb: FormBuilder,
    // private _isUserRegisteredValidator: IsUserRegisteredValidator,
    private _router: Router,
    private _actRoute: ActivatedRoute,
    private _warningsService: WarningsService,
    private _snackBar: MatSnackBar,
    private _isMobileNumberPipe: IsMobileNumberPipe,
    private _contactService: ContactService,
    private _addressService: AddressService,
  ) { super() }


  loginErrorMessage: string = '';

  backend = `${environment._BACK_END_ROOT_URL}/auth/ProfileAsync`

  entitiesFiltered$: any;
  address!: FormGroup;
  contact!: FormGroup;

  labelHeadersMiddle = () => {
    return [
      { key: '', style: 'cursor: pointer;' },
      { key: 'Nome', style: 'cursor: pointer;' },
      { key: 'Email', style: 'cursor: pointer;' }
    ]
  }

  fieldsHeadersMiddle = () => {
    return [
      { key: 'id', style: '' },
      { key: 'name', style: '' },
      { key: 'email', style: '' }
    ]
  }

  cpfCnpjBusinessData(data: BusinessData) {

    this.setFormMain(data);
    this.setEditressForm(data);
    this.setContactForm(data);


    this.sanitizeFormFields(this.formMain);
  }

  setFormMain(data: BusinessData) {
    if (data.nome.length > 0)
      this.formMain?.get('company')?.get('name')?.setValue(data.nome);
  }

  setEditressForm(data: BusinessData) {
    this.address.reset();
    this.address.get('zipcode')?.setValue(data.cep);
    this._addressService.query(data.cep)
    this.address.get('number')?.setValue(data.numero);
    this.address.get('id')?.setValue(0);
  }

  setContactForm(data: BusinessData) {
    this.contact.reset();
    this.contact.get('id')?.setValue(0);
    this.contact.get('email')?.setValue(data.email);

    const isMobile = this._isMobileNumberPipe.transform(data.telefone)

    if (isMobile.isMobile)
      this.contact.get('cel')?.setValue(isMobile.phoneNum);
    else
      this.contact.get('landline')?.setValue(isMobile.phoneNum);
  }

  sanitizeFormFields(form: FormGroup): void {
    Object.keys(form.controls).forEach(field => {
      const control = form.get(field);
      if (control instanceof FormGroup) {
        this.sanitizeFormFields(control);
      } else if (control && (control.value === null || control.value === undefined)) {
        control.setValue('');
      }
    })
  }


  // formLoad() {
  //   return this.formMain = this._fb.group(
  //     {
  //       userName: ['', [Validators.required, Validators.minLength(3)]],
  //       displayUserName: ['', [Validators.required, Validators.minLength(3)]],
  //       companyName: ['', [Validators.required, Validators.minLength(3)]],
  //       email: new FormControl(''),
  //       // email: new FormControl('', { validators: [Validators.required, Validators.maxLength(50), Validators.email], asyncValidators: [this._isUserRegisteredValidator.validate.bind(this._isUserRegisteredValidator)] }),
  //       password: ['', [Validators.required, Validators.minLength(3)]],
  //       confirmPassword: ['', [Validators.required]],
  //     }
  //     // }, { validators: [PasswordConfirmationValidator(), PasswordValidator()] }

  //   )
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

  formLoad(auth?: CompanyAuth, profile?: CompanyProfile): FormGroup {

    return this.formMain = this._fb.group({
      id: [auth?.id ?? 0, [Validators.required]],
      name: [auth?.name ?? '', [Validators.required, Validators.maxLength(100)]],
      tradeName: [auth?.tradeName ?? '', [Validators.required, Validators.maxLength(100)]],
      companyProfileId: [auth?.companyProfileId],
      businessId: [auth?.businessId],

      // companyId: [0, [Validators.required]],
      // companyId: [localStorage.getItem("companyId"), [Validators.required]],
      cnpj: [auth?.cnpj ?? '', []],
      // description: ['', [Validators.maxLength(500)]],
      entityType: [true, []],
      // registered: [new Date(), [Validators.required]],
      address: this.address = this._addressService.formLoad(profile?.address),
      contact: this.contact = this._contactService.formLoad(profile?.contact)
    })

  }

  // deleted
  // Registered


  ngOnInit(): void {
    const id = this._actRoute.snapshot.params['id'];



    this._companyService.loadById$<CompanyAuth>('http://localhost:5156/api/AuthAdm/GetCompanyAuthAsync', id).subscribe(
      (companyAuth: CompanyAuth) => {

        this._companyService.loadById$<CompanyProfile>('http://localhost:5156/api/AuthAdm/GetCompanyProfileAsync', companyAuth.companyProfileId).subscribe(
          (companyProfile: CompanyProfile) => {
            console.log(companyProfile)
            console.log(companyAuth)
            this.formLoad(companyAuth, companyProfile);
          }
        )


      }
    )
  }

}

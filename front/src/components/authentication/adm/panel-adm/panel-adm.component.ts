
import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';


import { environment } from 'environments/environment';
// import {  ProfileService } from '../services/profile.service';

// import { PasswordConfirmationValidator } from '../validators/password-confirmation-validator';
// import { PasswordValidator } from '../validators/password-validator';
// import { IsUserRegisteredValidator } from '../validators/is-user-registered-validator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTabsModule } from '@angular/material/tabs';
import { ActivatedRoute, Router } from '@angular/router';
import { CompanyAuth } from 'components/authentication/dtos/company-auth';
import { UserAccountAuthDto } from "../../../authentication/dtos/user-account-auth-dto";
import { CompanyProfileService, CompanyService } from 'components/authentication/services/company.service';
import { CompanyNamesComponent } from 'components/company/components/commons-components/company-names/company-names.component';
import { CompanyProfile } from 'components/company/dtos/company-profile';
import { WarningsService } from 'components/warnings/services/warnings.service';
import { AddressComponent } from 'shared/components/address/component/address.component';
import { AddressService } from 'shared/components/address/services/address.service';
import { CpfCnpjComponent } from 'shared/components/administrative/cpf-cnpj/cpf-cnpj.component';
import { DocType } from 'shared/components/administrative/cpf-cnpj/dto/doc-type';
import { BusinessData } from 'shared/components/administrative/name-cpf-cnpj/dto/business-data';
import { ContactComponent } from 'shared/components/contact/component/contact.component';
import { ContactService } from 'shared/components/contact/services/contact.service';
import { DefaultComponent } from 'shared/components/default-component/default-component';
import { ListGComponent } from 'shared/components/list-g/list/list-g.component';
import { IsMobileNumberPipe } from 'shared/pipes/is-mobile-number.pipe';
import { ListPanelControlAdm } from './helpers/list-panel-control-adm';
import { ImportsPanelAdm } from './imports/imports-panel-adm';


@Component({
  selector: 'panel-adm',
  templateUrl: './panel-adm.component.html',
  styleUrls: ['./panel-adm.component.css'],
  standalone: true,
  imports: [
    ImportsPanelAdm,
    DefaultComponent,
    CompanyNamesComponent,
    CpfCnpjComponent,
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
export class PanelAdmComponent extends ListPanelControlAdm implements OnInit{

  constructor(
    private _companyService: CompanyService,
    private _companyProfileService: CompanyProfileService,
    private _fb: FormBuilder,
    // private _isUserRegisteredValidator: IsUserRegisteredValidator,
    override _router: Router,
    private _actRoute: ActivatedRoute,
    private _warningsService: WarningsService,
    private _snackBar: MatSnackBar,
    private _isMobileNumberPipe: IsMobileNumberPipe,
    private _contactService: ContactService,
    private _addressService: AddressService,
  ) { super() }

  companyName = '';

  backend = `${environment._BACK_END_ROOT_URL}/auth/ProfileAsync`

  // entitiesFiltered$: any;
  address!: FormGroup;
  contact!: FormGroup;

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

  isValidCpf(isCpfValid: DocType) {
    this.formMain?.get('companyName')?.setValue('');
    this.address?.reset({ id: 0 });
    this.contact?.reset({ id: 0 });
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
      cnpj: [profile?.cnpj ?? '', []],
      // description: ['', [Validators.maxLength(500)]],
      entityType: [true, []],
      // registered: [new Date(), [Validators.required]],
      address: this.address = this._addressService.formLoad(profile?.address),
      contact: this.contact = this._contactService.formLoad(profile?.contact)
    })

  }

  // deleted
  // Registered


  getCompanyAuth(id: string) {
    return this._companyService.loadById$<CompanyAuth>(`${environment._BACK_END_ROOT_URL}/AuthAdm/GetCompanyAuthFullAsync`, id);
  }

  getCompanProfile(companyProfileId: string) {
    return this._companyService.loadById$<CompanyProfile>(`${environment._BACK_END_ROOT_URL}/AuthAdm/GetCompanyProfileAsync`, companyProfileId);
  }

  getUsersByCompanyIdAsync(companyAuthId: string) {
    return this._companyService.loadById$<UserAccountAuthDto[]>(`${environment._BACK_END_ROOT_URL}/AuthAdm/GetUsersByCompanyIdAsync`, companyAuthId);
  }




  ngOnInit(): void {
    const id = this._actRoute.snapshot.params['id'];


    // this._companyService.loadById$<CompanyProfile>('http://localhost:5156/api/AuthAdm/GetCompanyProfileAsync', companyAuth.companyProfileId).subscribe(
    //   (companyProfile: CompanyProfile) => {

    //     console.log(companyAuth.id)
    //     this.formLoad(companyAuth, companyProfile);
    //   }
    // )

    this.getCompanyAuth(id).subscribe(

      (companyAuth: CompanyAuth) => {


        this.startSupply(`${environment._BACK_END_ROOT_URL}/AuthAdm/GetUsersByCompanyIdAsync`, companyAuth?.id ?? 0)


        // this.getUsersByCompanyIdAsync(companyAuth.id.toString()).subscribe(
        //   (users: UserAccount[]) => {
        //     console.log(users)
        //   }
        // )



        this.getCompanProfile(companyAuth.companyProfileId).subscribe(
          (companyProfile: CompanyProfile) => {
            this.formLoad(companyAuth, companyProfile);
          }
        )


      }
    )
  }

}

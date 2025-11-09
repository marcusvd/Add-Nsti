import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { CompanyProfile } from 'components/company/dtos/company-profile';
import { environment } from 'environments/environment';
import { AddressService } from 'shared/components/address/services/address.service';
import { DocType } from 'shared/components/administrative/cpf-cnpj/dto/doc-type';
import { BusinessData } from 'shared/components/administrative/name-cpf-cnpj/dto/business-data';
import { ContactDto } from 'shared/components/contact/dtos/contact-dto';
import { ContactService } from 'shared/components/contact/services/contact.service';
import { IsMobileNumberPipe } from 'shared/pipes/is-mobile-number.pipe';
import { TruncatePipe } from 'shared/pipes/truncate.pipe';
import { UserAccountAuthDto } from "../../../authentication/dtos/user-account-auth-dto";
import { ListPanelControlAdm } from './helpers/list-panel-control-adm';
import { ImportsPanelAdm, ProvidersPanelAdm } from './imports/imports-panel-adm';
import { CompanyAuth } from '../../../company/dtos/company-auth';
import { Update_Auth_ProfileDto } from '../../../company/dtos/update-auth_profile-dto';
import { CompanyService } from 'components/company/services/company.service';
import { AddressDto } from 'shared/components/address/dtos/address-dto';


@Component({
  selector: 'panel-adm',
  templateUrl: './panel-adm.component.html',
  styleUrls: ['./panel-adm.component.css'],
  standalone: true,
  imports: [
    ImportsPanelAdm,
  ],
  providers: [
    ProvidersPanelAdm,
    TruncatePipe
  ]
})
export class PanelAdmComponent extends ListPanelControlAdm implements OnInit {

  private _companyService = inject(CompanyService);
  private _actRoute = inject(ActivatedRoute);
  private _isMobileNumberPipe = inject(IsMobileNumberPipe);
  private _contactService = inject(ContactService);
  private _addressService = inject(AddressService);
  private _fb = inject(FormBuilder);
  private _truncate = inject(TruncatePipe);
  backendAddress = `${environment._BACK_END_ROOT_URL}/_address/UpdateAddressAsync`
  backendContact = `${environment._BACK_END_ROOT_URL}/_contact/UpdateContactAsync`

  // companyName = '';
  backend = `${environment._BACK_END_ROOT_URL}/auth/ProfileAsync`
  labelTitleBar!: string;
  address!: FormGroup;
  contact!: FormGroup;

  ngOnInit(): void {
    const id = this._actRoute.snapshot.params['id'];

    this.getCompanyAuth(id).subscribe(

      (companyAuth: CompanyAuth) => {
        // console.log(companyAuth)
        this.startSupply(`${environment._USER_ACCOUNTS_CONTROLLER}/GetUsersByCompanyIdAsync`, companyAuth?.id ?? 0)

        this.getCompanProfile(companyAuth.cnpj).subscribe(
          (companyProfile: CompanyProfile) => {

            this.formLoad(companyAuth, companyProfile);

            const contact: ContactDto = this.formMain.get('contact')?.value;

            this.objContactNoRegister(contact?.cel ?? 'cadastro incompleto', 'cel', '0');
            this.objContactNoRegister(contact?.zap ?? 'cadastro incompleto', 'zap', '0');
            this.objContactNoRegister(contact?.landline ?? 'cadastro incompleto', 'landline', '0');
          }
        )
      }
    )

  }

  objContactNoRegister(value: string, field: string, characters: string) {
    const val = value.toLocaleLowerCase();
    const compare = 'cadastro incompleto'
    if (val === compare && field != 'landline')
      this.contact.get(field)?.patchValue(characters.repeat(10))
    else
      this.contact.get(field)?.patchValue(characters.repeat(11))
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

  formLoad(auth?: CompanyAuth, profile?: CompanyProfile): FormGroup {
    this.labelTitleBar = this._truncate.transform(`Dados da empresa ${auth?.tradeName}`, 33);
    return this.formMain = this._fb.group({
      id: [auth?.id ?? 0, [Validators.required]],
      idProfile: [profile?.id ?? 0, [Validators.required]],
      name: [auth?.name ?? '', [Validators.required, Validators.maxLength(100)]],
      tradeName: [auth?.tradeName ?? '', [Validators.required, Validators.maxLength(100)]],
      // companyProfileId: [auth?.companyProfileId],
      businessId: [auth?.businessId, Validators.required],
      businessProfileId: [profile?.businessProfileId, Validators.required],
      cnpj: [(auth?.cnpj ?? profile?.cnpj) ?? '0000000000', []],
      entityType: [true, []],
      address: this.address = this._addressService.formLoad(profile?.address) ?? new FormGroup({}),
      contact: this.contact = this._contactService.formLoad(profile?.contact) ?? new FormGroup({})
    })

  }

  updateFull() {

    const update_Auth_Profile: Update_Auth_ProfileDto = this.objHandler(this.formMain);

    console.log(this.formMain)
    console.log(update_Auth_Profile)


    if (this.alertSave(this.formMain)) {
      this._companyService.update_Auth_Profile$(update_Auth_Profile).subscribe({
        next: (result: boolean) => {

          if (result)
            this.openSnackBar('Atualizado, com sucesso, ' + update_Auth_Profile.companyAuth.name + '!', 'warnings-success');

          // this.callRouter(`/users/adm-list/${toSave.id}`);


        },
        error: (err) => {
          console.log(err)
          const erroCode: string = err.error.Message
        }
      })
    }
  }

  objHandler(form: FormGroup): Update_Auth_ProfileDto {

    const update_Auth_ProfileDto: Update_Auth_ProfileDto = new Update_Auth_ProfileDto();

    const companyAuth: CompanyAuth = this.companyAuthMakeFormUpdate(form)
    const companyProfile: CompanyProfile = this.companyProfileMakeFormUpdate(form)

    update_Auth_ProfileDto.companyAuth = companyAuth;
    update_Auth_ProfileDto.companyProfile = companyProfile;


    update_Auth_ProfileDto.companyAuthId = companyAuth.id;
    update_Auth_ProfileDto.companyProfileId = companyProfile.id;

    return update_Auth_ProfileDto;
  }


  companyAuthMakeFormUpdate(form: FormGroup) {

    const companyAuth: CompanyAuth = new CompanyAuth();

    companyAuth.id = form.get('id')?.value;
    companyAuth.businessId = form.get('businessId')?.value;
    companyAuth.name = form.get('name')?.value;
    companyAuth.tradeName = form.get('tradeName')?.value;
    companyAuth.cnpj = form.get('cnpj')?.value;

    return companyAuth;
  }

  companyProfileMakeFormUpdate(form: FormGroup) {

    const companyProfile: CompanyProfile = new CompanyProfile();

    companyProfile.id = form.get('idProfile')?.value;
    companyProfile.cnpj = form.get('cnpj')?.value;
    companyProfile.businessProfileId = form.get('businessProfileId')?.value;
    companyProfile.address = form.get('address')?.value;
    companyProfile.contact = form.get('contact')?.value;

    return companyProfile;
  }





  updateAddress(form: FormGroup) {
    const address: AddressDto = { ...form.value }
    if (this.alertSave(this.address))
      this._companyService.update$<AddressDto>(this.backendAddress, address).subscribe({
        next: (result: any) => {

          if (result)
            this.openSnackBar('Endereço atualizado, com sucesso! ' + '!', 'warnings-success');

          // this.callRouter(`/users/adm-list/${toSave.id}`)

        },
        error: (err) => {
          console.log(err)
          const erroCode: string = err.error.Message
        }
      })
  }
  updateContact(form: FormGroup) {
    const contact: ContactDto = { ...form.value }
    if (this.alertSave(this.contact))
      this._companyService.update$<ContactDto>(this.backendContact, contact).subscribe({
        next: (result: any) => {

          if (result)
            this.openSnackBar('Contato atualizado, com sucesso! ' + '!', 'warnings-success');

          // this.callRouter(`/users/adm-list/${toSave.id}`)

        },
        error: (err) => {
          console.log(err)
          const erroCode: string = err.error.Message
        }
      })
  }



  getCompanyAuth(id: string) {
    return this._companyService.loadById$<CompanyAuth>(`${environment._BACK_END_ROOT_URL}/_Companies/GetCompanyAuthFullAsync`, id);
  }

  getCompanProfile(cnpj: string) {
    return this._companyService.loadById$<CompanyProfile>(`${environment._BACK_END_ROOT_URL}/_Companies/GetCompanyProfileAsync`, cnpj);
  }

  // getUsersByCompanyIdAsync(companyAuthId: string) {
  //   return this._companyService.loadById$<UserAccountAuthDto[]>(`${environment._BACK_END_ROOT_URL}/_UserAccounts/GetUsersByCompanyIdAsync`, companyAuthId);
  // }


}

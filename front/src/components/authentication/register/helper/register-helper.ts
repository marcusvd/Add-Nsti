
import { inject } from '@angular/core';
import { FormGroup } from '@angular/forms';


import { BaseForm } from 'shared/inheritance/forms/base-form';

import { AddressService } from 'shared/components/address/services/address.service';
import { BusinessData } from 'shared/components/administrative/cpf-cnpj/dto/business-data';
import { DocType } from 'shared/components/administrative/cpf-cnpj/dto/doc-type';
import { IsMobileNumberPipe } from 'shared/pipes/is-mobile-number.pipe';



export class RegisterHelper extends BaseForm {

  constructor(
    protected _addressService: AddressService,
  ) {
    super()
  }

  private _isMobileNumberPipe = inject(IsMobileNumberPipe);

  address!: FormGroup;
  contact!: FormGroup;
  isCpf = true;

  cpfCnpjBusinessData(data: BusinessData) {

    this.setFormMain(data);
    this.setAddressForm(data);
    this.setContactForm(data);
    this.isCpf = false;
    this.formMain.get('address')?.setValue(this.address.value)
    this.formMain.get('contact')?.setValue(this.contact.value)

    this.sanitizeFormFields(this.formMain);

  }

  setFormMain(data: BusinessData) {
    if (data.nome.length > 0)
      this.formMain?.get('companyName')?.setValue(data.nome);
  }

  isValidCpf(isCpfValid: DocType) {
    this.formMain?.get('companyName')?.setValue('');
    this.address?.reset();
    this.contact?.reset();
  }

  setAddressForm(data: BusinessData) {
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

}

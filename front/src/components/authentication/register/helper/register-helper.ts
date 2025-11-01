
import { inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';


import { BaseForm } from 'shared/extends/forms/base-form';

import { AddressService } from 'shared/components/address/services/address.service';
import { BusinessData } from 'shared/components/administrative/cpf-cnpj/dto/business-data';
import { DocType } from 'shared/components/administrative/cpf-cnpj/dto/doc-type';
import { IsMobileNumberPipe } from 'shared/pipes/is-mobile-number.pipe';
import { PasswordConfirmationValidator } from 'components/authentication/validators/password-confirmation-validator';
import { PasswordValidator } from 'components/authentication/validators/password-validator';
import { ContactService } from 'shared/components/contact/services/contact.service';
import { IsUserRegisteredValidator } from 'components/authentication/validators/is-user-registered-validator';
import { Register } from 'components/authentication/dtos/register';



export class RegisterHelper extends BaseForm {

  private readonly _fb = inject(FormBuilder);
  private readonly _contactService = inject(ContactService);
  private readonly _addressService = inject(AddressService);
  private readonly _isUserRegisteredValidator = inject(IsUserRegisteredValidator);
  private readonly _isMobileNumberPipe = inject(IsMobileNumberPipe);

  address!: FormGroup;
  contact!: FormGroup;
  isCpf = true;

  resetFormEmailNoLoss!: string;

  cpfCnpjBusinessData(data: BusinessData) {

    this.setFormMain(data);
    this.setAddressForm(data);
    this.setContactForm(data);
    this.isCpf = false;

    this.formMain.get('address')?.setValue(this.address.value)
    this.formMain.get('contact')?.setValue(this.contact.value)

    this.keepEmailFieldForm(this.formMain);
    this.sanitizeFormFields(this.formMain);
    this.setFormFieldValue(this.formMain, 'email', this.resetFormEmailNoLoss);

  }


  isValidCpf(isCpfValid: DocType) {

    this.keepEmailFieldForm(this.formMain);

    this.formMain?.get('companyName')?.setValue('');
    this.address?.reset();
    this.contact?.reset();

    const register: Register = { ... this.formMain.value };

    if (isCpfValid.result) {
      this.formLoadCpf(register);
      this.isCpf = true;
    }
    else {
      this.formLoadCnpj(register);
      this.isCpf = false;
    }

    this.setFormFieldValue(this.formMain, 'email', this.resetFormEmailNoLoss);

    //console.log(isCpfValid);
  }

  private keepEmailFieldForm(form: FormGroup) {

    const result: boolean = (form.get('email')?.value as string).length > 0;

    if (result)
      this.resetFormEmailNoLoss = form.get('email')?.value;
  }

  setFormMain(data: BusinessData) {
    if (data.nome.length > 0)
      this.formMain?.get('companyName')?.setValue(data.nome);
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

    const isMobile = this._isMobileNumberPipe.transform(data.telefone)

    if (isMobile.isMobile)
      this.contact.get('cel')?.setValue(isMobile.phoneNum);
    else
      this.contact.get('landline')?.setValue(isMobile.phoneNum);
  }


  formLoadCnpj(register?: Register) {
    return this.formMain = this._fb.group({
      userName: [register?.userName, [Validators.required, Validators.minLength(3)]],
      companyName: [register?.companyName, [Validators.required, Validators.minLength(3)]],
      email: new FormControl(this.resetFormEmailNoLoss ?? '', { validators: [Validators.required, Validators.maxLength(50), Validators.email], asyncValidators: [this._isUserRegisteredValidator.validate.bind(this._isUserRegisteredValidator)] }),
      password: [register?.password, [Validators.required, Validators.minLength(3)]],
      cnpj: [register?.cnpj, [Validators.required]],
      confirmPassword: [register?.confirmPassword, [Validators.required]],
      address: this.address = this._addressService.formLoad(register?.address),
      contact: this.contact = this._contactService.formLoad(register?.contact)
    }, { validators: [PasswordConfirmationValidator(), PasswordValidator()] })
  }

  formLoadCpf(register?: Register) {
    return this.formMain = this._fb.group({
      userName: [register?.userName, [Validators.required, Validators.minLength(3)]],
      companyName: [register?.companyName, [Validators.required, Validators.minLength(3)]],
      email: new FormControl(this.resetFormEmailNoLoss ?? '', { validators: [Validators.required, Validators.maxLength(50), Validators.email], asyncValidators: [this._isUserRegisteredValidator.validate.bind(this._isUserRegisteredValidator)] }),
      password: [register?.password, [Validators.required, Validators.minLength(3)]],
      cnpj: [register?.cnpj, [Validators.required]],
      confirmPassword: [register?.confirmPassword, [Validators.required]],
      address: new FormGroup({}),
      contact: new FormGroup({}),
    }, { validators: [PasswordConfirmationValidator(), PasswordValidator()] })
  }

}


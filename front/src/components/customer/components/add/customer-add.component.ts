import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";



import { CustomerListService } from "components/customer/services/customer-list.service";
import { IsMobileNumberPipe } from "shared/pipes/is-mobile-number.pipe";
import { AddressService } from "../../../../shared/components/address/services/address.service";
import { BusinessData } from "../../../../shared/components/administrative/name-cpf-cnpj/dto/business-data";
import { ContactService } from "../../../../shared/components/contact/services/contact.service";
import { BaseForm } from '../../../../shared/extends/forms/base-form';
import { AddImports } from "../../../imports/crud-customer-equipment-employees-imports";
import { DefaultCompImports } from "../../../imports/default-comp-imports";
import { CustomerAddProviders } from '../../imports/providers-customer';
import { CustomerAddService } from "../../services/customer-add.service";

@Component({
  selector: 'customer-add',
  templateUrl: './customer-add.component.html',
  styleUrls: ['./customer-add.component.scss'],
  standalone: true,
  imports: [
    DefaultCompImports,
    AddImports
  ],
  providers: [
    CustomerAddProviders,
    CustomerListService,
    CustomerAddService
  ]
})

export class CustomerAddComponent extends BaseForm implements OnInit {


  title: string = 'Cadastro';
  subTitle: string = 'Cliente';
  borderAround: boolean = false;

  screenFieldPosition: string = 'row';

  address!: FormGroup;
  contact!: FormGroup;

  constructor(
    private _customerService: CustomerAddService,
    private _contactService: ContactService,
    private _addressService: AddressService,
    private _isMobileNumberPipe: IsMobileNumberPipe,
    private _fb: FormBuilder
  ) { super() }



  cpfCnpjBusinessData(data: BusinessData) {

    this.setFormMain(data);
    this.setAddressForm(data);
    this.setContactForm(data);

  }

  setFormMain(data: BusinessData) {
    if (data.qsa.length > 0)
      this.formMain.get('responsible')?.setValue(data.qsa[0].nome);
    else
      this.formMain.get('responsible')?.setValue(data.nome);

    this.formMain.get('name')?.setValue(data.nome);
    this.formMain.get('businessLine')?.setValue(data.atividade_principal[0].text);
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


  save() {

    if (this.alertSave(this.formMain)) {
      this._customerService.save(this.formMain);
      this.formLoad();
    }
  }

  additionalCosts!: FormGroup;
  formLoad(): FormGroup {
    return this.formMain = this._fb.group({
      id: [0, [Validators.required]],
      name: ['', [Validators.required, Validators.maxLength(100)]],
      tradeName: ['', [Validators.required, Validators.maxLength(100)]],
      companyId: [1, [Validators.required]],
      // companyId: [localStorage.getItem("companyId"), [Validators.required]],
      cnpj: ['', []],
      description: ['', [Validators.maxLength(500)]],
      entityType: [true, []],
      registered: [new Date(), [Validators.required]],
      address: this.address = this._addressService.formLoad(),
      contact: this.contact = this._contactService.formLoad()
    })
  }

  ngOnInit(): void {
    this.formLoad();
  }

}








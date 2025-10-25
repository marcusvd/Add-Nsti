import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute } from "@angular/router";



import { Observable, of } from "rxjs";
import { IsMobileNumberPipe } from "shared/pipes/is-mobile-number.pipe";
import { AddressService } from "../../../../shared/components/address/services/address.service";
import { BusinessData } from "../../../../shared/components/administrative/name-cpf-cnpj/dto/business-data";
import { ContactService } from "../../../../shared/components/contact/services/contact.service";
import { BaseForm } from '../../../../shared/extends/forms/base-form';
import { CustomerEditProviders } from "../../../customer/imports/providers-customer";
import { EditImports } from "../../../imports/crud-customer-equipment-employees-imports";
import { DefaultCompImports } from "../../../imports/default-comp-imports";
import { CustomerEditService } from "../../services/customer-edit.service";
import { CustomerDto } from "../commons-components/dtos/customer-dto";



@Component({
  selector: 'customer-edit',
  templateUrl: './customer-edit.component.html',
  styleUrls: ['./customer-edit.component.css'],
  standalone: true,
  imports: [
    DefaultCompImports,
    EditImports
  ],
  providers: [
    CustomerEditProviders
  ]
})

export class CustomerEditComponent extends BaseForm implements OnInit {

  address!: FormGroup;
  contact!: FormGroup;

  constructor(
    private _customerService: CustomerEditService,
    private _contactService: ContactService,
    private _addressService: AddressService,
    private _fb: FormBuilder,
    private _actRouter: ActivatedRoute,
    private _isMobileNumberPipe: IsMobileNumberPipe


  ) { super() }


  formLoad(customer?: CustomerDto): FormGroup {



    return this.formMain = this._fb.group({
      id: [customer?.id, [Validators.required]],
      name: [customer?.name, [Validators.required, Validators.maxLength(100)]],
      tradeName: ['', [Validators.required, Validators.maxLength(100)]],
      companyId: [localStorage.getItem("companyId"), [Validators.required]],
      cnpj: [customer?.cnpj, []],
      description: [customer?.description, [Validators.maxLength(500)]],
      entityType: [customer?.entityType == 0 ? true : false, []],
      registered: [customer?.registered, [Validators.required]],
      address: this.address = this._addressService.formLoad(customer?.address),
      contact: this.contact = this._contactService.formLoad(customer?.contact)
    })

  }

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
    this.address.get('zipcode')?.setValue(data.cep);
    this._addressService.query(data.cep)
    this.address.get('number')?.setValue(data.numero);
    this.address.get('id')?.setValue(0);
  }

  setContactForm(data: BusinessData) {
    this.contact.get('id')?.setValue(0);
    this.contact.get('email')?.setValue(data.email);

    const isMobile = this._isMobileNumberPipe.transform(data.telefone)

    if (isMobile.isMobile)
      this.contact.get('cel')?.setValue(isMobile.phoneNum);
    else
      this.contact.get('landline')?.setValue(isMobile.phoneNum);


  }

  rows: number = 0;
  calcRows(value: string) {
    this.rows = value.length / 80;
  }

  getEntityId(id: number) {
// const customer: Observable<CustomerDto> = this._customerService.loadById$('Customers/GetCustomerByIdAllIncluded', id.toString());
// const customer: Observable<CustomerDto> = of(this._customerService.getCustomersMoc().find(x => x.id == id));
const customer: Observable<CustomerDto> = of(
  this._customerService.getCustomersMoc().find(x => x.id == id) ?? {} as CustomerDto
);

    customer.subscribe(x => {
      this.formLoad(x);
      this._contactService.seedingSocialnetworks(x.contact.socialMedias);
      this.calcRows(x.description)
    });

  }

  save() {

    if (this.alertSave(this.formMain)) {
      this._customerService.update(this.formMain);
    }
  }

  ngOnInit(): void {
    const id = this._actRouter.snapshot.params['id'];
    this.getEntityId(id);

  }

}








import { Component, inject, Input, OnInit } from "@angular/core";
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";


import { IsMobileNumberPipe } from "shared/pipes/is-mobile-number.pipe";
import { AddressService } from "../../../../shared/components/address/services/address.service";
import { BusinessData } from "../../../../shared/components/administrative/name-cpf-cnpj/dto/business-data";
import { ContactService } from "../../../../shared/components/contact/services/contact.service";
import { BaseForm } from '../../../../shared/extends/forms/base-form';

import { DefaultCompImports } from "../../../imports/default-comp-imports";
import { CompanyAddService } from "../../services/company-add.service";
import { AddImports } from "components/imports/crud-customer-equipment-employees-imports";
import { CompanyAddProviders } from "components/company/imports/providers-customer";
import { ImportsCompany } from "components/company/imports/imports-company";
import { ActivatedRoute, Router, RouterModule } from "@angular/router";
import { BusinessAuth } from "components/authentication/dtos/business-auth";
import { map, Observable } from "rxjs";
import { DefaultComponent } from "shared/components/default-component/default-component";
import { CpfCnpjComponent } from "shared/components/administrative/cpf-cnpj/cpf-cnpj.component";
import { CompanyNamesComponent } from "../commons-components/company-names/company-names.component";
import { DocType } from "shared/components/administrative/cpf-cnpj/dto/doc-type";

@Component({
  selector: 'add-company',
  templateUrl: './add-company.component.html',
  styleUrls: ['./add-company.component.scss'],
  standalone: true,
  imports: [
    DefaultCompImports,
    ImportsCompany,
    DefaultComponent,
    CpfCnpjComponent,
    CompanyNamesComponent
  ],
  providers: [
    CompanyAddProviders,
    CompanyAddService
  ]
})

export class AddCompanyComponent extends BaseForm implements OnInit {

  @Input('entity') entityBusiness$!: Observable<BusinessAuth>;

  private _route = inject(Router);

  address!: FormGroup;
  contact!: FormGroup;

  constructor(
    private _companyService: CompanyAddService,
    private _contactService: ContactService,
    private _addressService: AddressService,
    private _isMobileNumberPipe: IsMobileNumberPipe,
    private _actRoute: ActivatedRoute,
    private _fb: FormBuilder
  ) { super() }

  cpfCnpjBusinessData(data: BusinessData) {

    this.setFormMain(data);
    this.setAddressForm(data);
    this.setContactForm(data);

    this.address?.setValue(this.address.value)
    this.contact?.setValue(this.contact.value)

    this.sanitizeFormFields(this.formMain);
  }

  setFormMain(data: BusinessData) {
    if (data.nome.length > 0) {
      this.formMain?.get('company')?.get('name')?.setValue(data.nome);
      this.formMain?.get('company')?.get('tradeName')?.setValue(data.nome);
    }
  }

  isValidCpf(isCpfValid: DocType) {
    this.formMain?.get('companyName')?.setValue('');
    this.address?.reset({ id: 0 });
    this.contact?.reset({ id: 0 });
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
    if (this.alertSave(this.formMain))
      this._companyService.save(this.formMain);

  }

  additionalCosts!: FormGroup;
  formLoad(x?: BusinessAuth): FormGroup {

    return this.formMain = this._fb.group({
      id: [x?.id, [Validators.required]],
      businessId: [x?.id, [Validators.required]],
      businessProfileId: [x?.businessProfileId, [Validators.required]],
      name: [x?.name, [Validators.required, Validators.maxLength(100)]],
      company: this.subForm = this._fb.group({
        id: [0, [Validators.required]],
        name: ['', [Validators.required, Validators.maxLength(100)]],
        tradeName: ['', [Validators.required, Validators.maxLength(100)]],
        companyProfileId: ['back-end'],
        businessId: [x?.id, [Validators.required]],

      }),
      cnpj: [''],
      address: this.address = this._addressService.formLoad(),
      contact: this.contact = this._contactService.formLoad()
    })
  }

  ngOnInit(): void {

    const id = this._actRoute.snapshot.params['id'];
    this._companyService.loadById$<BusinessAuth>('http://localhost:5156/api/_Businesses/GetBusinessAsync', id).subscribe(
      (x: BusinessAuth) => {
        this.formLoad(x)
      }
    )
  }

}









import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';


import { environment } from 'environments/environment';
import { BaseForm } from 'shared/inheritance/forms/base-form';


import { ImportsaddUserCompany } from './imports/imports-add-user-company';

import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { AddUserExistingCompanyDto } from 'components/authentication/dtos/add-user-existing-company-dto';
import { CompanyAuth } from 'components/authentication/dtos/company-auth';
import { RegisterService } from 'components/authentication/services/register.service';
import { IsUserRegisteredValidator } from 'components/authentication/validators/is-user-registered-validator';
import { PasswordConfirmationValidator } from 'components/authentication/validators/password-confirmation-validator';
import { PasswordValidator } from 'components/authentication/validators/password-validator';
import { WarningsService } from 'components/warnings/services/warnings.service';
import { AddressComponent } from 'shared/components/address/component/address.component';
import { AddressService } from 'shared/components/address/services/address.service';
import { ContactComponent } from 'shared/components/contact/component/contact.component';
import { ContactService } from 'shared/components/contact/services/contact.service';
import { CompanyService } from '../../../authentication/services/company.service';
import { MatCheckboxChange, MatCheckboxModule } from '@angular/material/checkbox';


@Component({
  selector: 'add-user-company',
  templateUrl: './add-user-company.component.html',
  styleUrls: ['./add-user-company.component.css'],
  standalone: true,
  imports: [
    ImportsaddUserCompany,
    MatCheckboxModule,
    AddressComponent,
    ContactComponent
  ],
  providers: [
    RegisterService,
    AddressService,
    ContactService
  ]
})
export class AddUserCompanyComponent extends BaseForm implements OnInit {



  private _addressService = inject(AddressService);
  private _contactService = inject(ContactService);

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



  backend = `${environment._BACK_END_ROOT_URL}/AuthAdm/AddUserAccountAsync`
  businessId!: number;
  formFull!: boolean;
  address!: FormGroup;
  contact!: FormGroup;

  registerFull(full: MatCheckboxChange) {
    this.formFull = full.checked;
  }



  register() {



    if (this.formFull) {
      if (this.alertSave(this.formMain) && this.alertSave(this.address) && this.alertSave(this.contact)) {
        this.registerAfterChecked(this.address, this.contact);
      }
    }
    else {
      if (this.alertSave(this.formMain)) {
        this.registerAfterChecked();
      }
    }

  }

  registerAfterChecked(address?: FormGroup, contact?: FormGroup) {

    const user: AddUserExistingCompanyDto = this.formMain.value;

    if (address != null && contact != null) {
      user.address = address.value
      user.contact = contact.value
    }

    if (this.formMain.valid) {
      this._registerService.AddUserInAExistingCompany(user, this.backend)
        .subscribe({
          next: (user) => {
            this._warningsService.openSnackBar('CADASTRADO!' + '   ' + user.email.toUpperCase() + '.', 'warnings-success');

            this._router.navigateByUrl(`users/adm-list/${this.businessId}`);

          }, error: (err: any) => {
            console.log(err)
            const erroCode: string = err?.error?.Message?.split('|');
            console.log(erroCode)

          }
        })
    }

  }



  formLoad(x?: CompanyAuth) {
    return this.formMain = this._fb.group({
      id: [x?.id, [Validators.required]],
      companyAuthId: [x?.id, [Validators.required]],
      userName: ['', [Validators.required, Validators.minLength(3)]],
      companyName: [x?.name, [Validators.required, Validators.minLength(3)]],
      email: new FormControl('', { validators: [Validators.required, Validators.maxLength(50), Validators.email], asyncValidators: [this._isUserRegisteredValidator.validate.bind(this._isUserRegisteredValidator)] }),
      password: ['', [Validators.required, Validators.minLength(3)]],
      confirmPassword: ['', [Validators.required]],
    }, { validators: [PasswordConfirmationValidator(), PasswordValidator()] })
  }

  //  typeRegister(full: boolean) {

  //   if (full) {
  //     this.address = this._addressService.formLoad();
  //     this.contact = this._contactService.formLoad();
  //     this.formMain.setControl('address', this.address);
  //     this.formMain.setControl('contact', this.contact);
  //   } else {
  //     this.address = this._fb.group({});
  //     this.contact = this._fb.group({});
  //     this.formMain.setControl('address', this.address);
  //     this.formMain.setControl('contact', this.contact);
  //   }

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

    this.address = this._addressService.formLoad();
    this.contact = this._contactService.formLoad();
  }

}

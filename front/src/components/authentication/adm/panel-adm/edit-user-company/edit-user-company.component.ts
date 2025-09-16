
import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';


import { environment } from 'environments/environment';
import { BaseForm } from 'shared/inheritance/forms/base-form';


import { ImportsEditUserCompany } from './imports/imports-edit-user-company';

import { ActivatedRoute, Router } from '@angular/router';
import { RegisterService } from 'components/authentication/services/register.service';
import { IsUserRegisteredValidator } from 'components/authentication/validators/is-user-registered-validator';
// import { AddUserCompanyService } from 'components/authentication/services/edit-user-company.service';

import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatTabsModule } from '@angular/material/tabs';
import { AccountStatusComponent } from 'components/authentication/account-status/account-status.component';
import { EmailComponent } from 'components/authentication/common-components/email/email.component';
import { UpdateUserAccountEmailDto } from 'components/authentication/dtos/update-user-account-email-dto';
import { UserAccountAuthDto } from 'components/authentication/dtos/user-account-auth-dto';
import { UserAuthProfileDto } from 'components/authentication/dtos/user-auth-profile-dto';
import { UserProfileDto } from 'components/authentication/dtos/user-profile-dto';
import { PasswordExpiresComponent } from 'components/authentication/password-expires/password-expires.component';
import { PasswordResetAdmComponent } from 'components/authentication/password-reset-adm/password-reset-adm.component';
import { ProfileService } from 'components/authentication/services/profile.service';
import { AddressComponent } from 'shared/components/address/component/address.component';
import { AddressDto } from 'shared/components/address/dtos/address-dto';
import { AddressService } from 'shared/components/address/services/address.service';
import { ContactComponent } from 'shared/components/contact/component/contact.component';
import { ContactDto } from 'shared/components/contact/dtos/contact-dto';
import { ContactService } from 'shared/components/contact/services/contact.service';
import { TimedAccessControlComponent } from 'components/authentication/common-components/timed-access-control/timed-access-control.component';
import { ToggleTwoFactorComponent } from 'components/authentication/common-components/toggle-two-factor/toggle-two-factor.component';
// import { IgxTimePickerComponent } from 'igniteui-angular';
// import { AddUserExistingCompanyDto } from 'components/authentication/dtos/edit-user-existing-company-dto';


@Component({
  selector: 'edit-user-company',
  templateUrl: './edit-user-company.component.html',
  styleUrls: ['./edit-user-company.component.css'],
  standalone: true,
  imports: [
    ImportsEditUserCompany,
    EmailComponent,
    MatCheckboxModule,
    MatTabsModule,
    AddressComponent,
    ContactComponent,
    PasswordExpiresComponent,
    AccountStatusComponent,
    PasswordResetAdmComponent,
    TimedAccessControlComponent,
    ToggleTwoFactorComponent
    // IgxTimePickerComponent
  ],
  providers: [
    RegisterService,
    AddressService,
    ContactService
  ]
})
export class EditUserCompanyComponent extends BaseForm implements OnInit {


  constructor(
    // private _addUserCompanyService: AddUserCompanyService,
    private _fb: FormBuilder,
    private _isUserRegisteredValidator: IsUserRegisteredValidator,
    private _router: Router,
    private _actRouter: ActivatedRoute,
    // private _warningsService: WarningsService,
    // private _snackBar: MatSnackBar
  ) { super() }

  private _addressService = inject(AddressService);
  private _contactService = inject(ContactService);
  private _registerService = inject(RegisterService);
  private _profileService = inject(ProfileService);


  // formFull!: boolean;

  address!: FormGroup;
  contact!: FormGroup;
  oldEmail: string | undefined = '';
  email: string | undefined = '';
  userIdRoute!: number;
  lastLogin!: Date;
  userAuth!: UserAccountAuthDto | undefined;
  userProfile!: UserProfileDto | undefined;
  willExpires!: Date;

  horaInicio: string = '08:00';
  horaFim: string = '18:00';



  backend = `${environment._BACK_END_ROOT_URL}/AuthAdm/AddUserAccountAsync`
  backendEmailUpdate = `${environment._BACK_END_ROOT_URL}/auth/RequestEmailChange`
  backendAddress = `${environment._BACK_END_ROOT_URL}/Address/UpdateAddressAsync`
  backendContact = `${environment._BACK_END_ROOT_URL}/contact/UpdateContactAsync`

  emailChange(newEmail: string) {

    const emailChanged = newEmail === this.oldEmail;
    const emailIsValid = this.formMain.get('email')?.valid;
    if (!emailChanged && emailIsValid) {
      const changeEmail = new UpdateUserAccountEmailDto(this.oldEmail ?? '', newEmail)
      this._registerService.RequestEmailChange(changeEmail, this.backendEmailUpdate);
    }

  }


  contactUpdate() {

    if (this.alertSave(this.contact)) {
      const toUpdate: ContactDto = this.contact.value;
      this._profileService.updateContactUserProfile(toUpdate, this.backendContact)
        .subscribe({
          next: (x => {
            console.log(x);
          })
        });
    }

  }

  addressUpdate() {

    if (this.alertSave(this.address)) {
      const toUpdate: AddressDto = this.address.value;
      this._profileService.updateAddressUserProfile(toUpdate, this.backendAddress)
        .subscribe({
          next: (x => {
            console.log(x);
          })
        });
    }
  }




  // registerFull(full: MatCheckboxChange) {
  //   this.formFull = full.checked;
  // }
  // register() {

  //   const user: any = this.formMain.value;

  //   if (this.alertSave(this.formMain)) {
  //     if (this.formMain.valid) {
  //       this._registerService.AddUserInAExistingCompany(user, this.backend)
  //         .subscribe({
  //           next: (user) => {
  //             this._warningsService.openSnackBar('CADASTRADO!' + '   ' + user.email.toUpperCase() + '.', 'warnings-success');

  //           }, error: (err: any) => {
  //             console.log(err)
  //             const erroCode: string = err?.error?.Message?.split('|');
  //             console.log(erroCode)

  //           }
  //         })

  //     }

  //   }
  // }




  formLoad(x?: UserAuthProfileDto) {

    this.oldEmail = x?.userAccountAuth.email;

    this.userAuth = x?.userAccountAuth
    this.lastLogin = x?.userAccountAuth.lastLogin ?? this.minDate;
    this.userProfile = x?.userAccountProfile;
    this.email = x?.userAccountAuth.email;

    return this.formMain = this._fb.group({
      id: [x?.id, [Validators.required]],
      companyAuthId: [x?.id, [Validators.required]],
      displayUserName: [x?.userAccountAuth.displayUserName, [Validators.required, Validators.minLength(3)]],
      email: new FormControl(x?.userAccountAuth.email, { validators: [Validators.required, Validators.maxLength(50), Validators.email], asyncValidators: [this._isUserRegisteredValidator.validate.bind(this._isUserRegisteredValidator)] }),

      address: this.address = this._addressService.formLoad(x?.userAccountProfile.address),
      contact: this.contact = this._contactService.formLoad(x?.userAccountProfile.contact)
    })

  }





  emailUserName = () => this.formMain?.get('email')?.value as string;

  back() {
    window.history.back();
  }

  ngOnInit(): void {
    const id = this._actRouter.snapshot.params['id'] as number;
    this.userIdRoute = id;
    const backend = `${environment._BACK_END_ROOT_URL}/authadm/GetUserByIdFullAsync`

    this._registerService.loadById$<UserAuthProfileDto>(backend, id.toString()).subscribe((x: UserAuthProfileDto) => {
      this.formLoad(x);
    })

  }

}

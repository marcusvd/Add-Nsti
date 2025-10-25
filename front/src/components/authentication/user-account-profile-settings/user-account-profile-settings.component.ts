
import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatTabsModule } from '@angular/material/tabs';
import { environment } from 'environments/environment';
import { BaseForm } from 'shared/extends/forms/base-form';


import { RegisterService } from 'components/authentication/services/register.service';
import { IsUserRegisteredValidator } from 'components/authentication/validators/is-user-registered-validator';
import { AccountStatusComponent } from 'components/authentication/account-status/account-status.component';
import { EmailComponent } from 'components/authentication/common-components/email/email.component';
import { UpdateUserAccountEmailDto } from 'components/authentication/dtos/update-user-account-email-dto';
import { UserAccountAuthDto } from 'components/authentication/dtos/user-account-auth-dto';
import { UserAuthProfileDto } from 'components/authentication/dtos/user-auth-profile-dto';
import { UserProfileDto } from 'components/authentication/dtos/user-profile-dto';
import { Email2faTokenSendComponent } from 'components/authentication/email-2fa-token-send/email-2fa-token-send.component';
import { ProfileService } from 'components/authentication/services/profile.service';
import { TwoFactorEnableComponent } from 'components/authentication/two-factor-enable/two-factor-enable.component';
import { TwoFactorSetupComponent } from 'components/authentication/two-factor-setup/two-factor-setup.component';
import { AddressComponent } from 'shared/components/address/component/address.component';
import { AddressDto } from 'shared/components/address/dtos/address-dto';
import { AddressService } from 'shared/components/address/services/address.service';
import { ContactComponent } from 'shared/components/contact/component/contact.component';
import { ContactDto } from 'shared/components/contact/dtos/contact-dto';
import { ContactService } from 'shared/components/contact/services/contact.service';
import { ImportsUserAccountProfileSettings } from './imports/imports-user-account-profile-settings';
import { PasswordChangeComponent } from '../password-change/password-change.component';




@Component({
  selector: 'user-account-profile-settings',
  templateUrl: './user-account-profile-settings.component.html',
  styleUrls: ['./user-account-profile-settings.component.css'],
  standalone: true,
  imports: [
    ImportsUserAccountProfileSettings,
    EmailComponent,
    MatCheckboxModule,
    MatTabsModule,
    AddressComponent,
    ContactComponent,
    AccountStatusComponent,
    TwoFactorEnableComponent,
    TwoFactorSetupComponent,
    Email2faTokenSendComponent,
    PasswordChangeComponent

  ],
  providers: [
    RegisterService,
    AddressService,
    ContactService
  ]
})


export class UserAccountProfileSettingsComponent extends BaseForm implements OnInit {

  private _fb = inject(FormBuilder);
  private _isUserRegisteredValidator = inject(IsUserRegisteredValidator);
  private _actRouter = inject(ActivatedRoute);
  private _addressService = inject(AddressService);
  private _contactService = inject(ContactService);
  private _registerService = inject(RegisterService);
  private _profileService = inject(ProfileService);


  // formFull!: boolean;

  address!: FormGroup;
  contact!: FormGroup;
  oldEmail: string | undefined = '';
  email: string = '';
  userIdRoute!: number;
  lastLogin!: Date;
  onOffCode2FaSendEmail!: Date;
  userAuth!: UserAccountAuthDto | undefined;
  userProfile!: UserProfileDto | undefined;
  toggleRefresh!: boolean;

  horaInicio: string = '08:00';
  horaFim: string = '18:00';

  backend = `${environment._BACK_END_ROOT_URL}/UserAccounts/AddUserAccountAsync`
  backendEmailUpdate = `${environment._BACK_END_ROOT_URL}/auth/RequestEmailChange`
  backendAddress = `${environment._BACK_END_ROOT_URL}/_Address/UpdateAddressAsync`
  backendContact = `${environment._BACK_END_ROOT_URL}/_contact/UpdateContactAsync`

  emailChange(newEmail: string) {

    const emailChanged = newEmail === this.oldEmail;
    const emailIsValid = this.formMain.get('email')?.valid;
    if (!emailChanged && emailIsValid) {
      const changeEmail = new UpdateUserAccountEmailDto(this.oldEmail ?? '', newEmail)
      this._registerService.RequestEmailChange(changeEmail, this.backendEmailUpdate);
    }

  }

  toggleBtn(toggle: boolean) {
    this.toggleRefresh = toggle;
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


  formLoad(x?: UserAuthProfileDto) {

    this.oldEmail = x?.userAccountAuth.email;

    this.userAuth = x?.userAccountAuth
    this.lastLogin = x?.userAccountAuth.lastLogin ?? this.minDate;
    this.userProfile = x?.userAccountProfile;
    this.email = x?.userAccountAuth.email ?? 'Inválido.';
    this.onOffCode2FaSendEmail = x?.userAccountAuth.code2FaSendEmail ?? new Date();

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
    const backend = `${environment._BACK_END_ROOT_URL}/useraccounts/GetUserByIdFullAsync`

    this._registerService.loadById$<UserAuthProfileDto>(backend, id?.toString()).subscribe((x: UserAuthProfileDto) => {
      console.log(x)
      this.formLoad(x);
    })

  }

}

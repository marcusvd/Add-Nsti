import { Injectable } from "@angular/core";
import { BackEndService } from "shared/services/back-end/backend.service";
import { take } from "rxjs";
import { FormGroup } from "@angular/forms";
import { RegisterDto } from "../dtos/register-dto";
import { UserAccountProfileUpdateDto } from "../dtos/user-account-profile-update-dto";
import { ResponseIdentiyApiDto } from "../dtos/response-identiy-api-dto";
import { AddressDto } from "shared/components/address/dtos/address-dto";
import { ContactDto } from "shared/components/contact/dtos/contact-dto";


@Injectable({ providedIn: 'root' })


export class ProfileService extends BackEndService<RegisterDto> {

  constructor() { super() }

  // AddUser(user: RegisterDto, form: FormGroup, url:string) {
  //   return this.add$<RegisterDto>(user, url).pipe(take(1));
  // }

  updateAddressUserProfile(user: AddressDto, url: string) {
    return this.update$<boolean>(url, user).pipe(take(1));
  }
  updateContactUserProfile(user: ContactDto, url: string) {
    return this.update$<boolean>(url, user).pipe(take(1));
  }

}

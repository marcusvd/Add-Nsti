import { Injectable } from "@angular/core";
import { take } from "rxjs";
import { AddressDto } from "shared/components/address/dtos/address-dto";
import { ContactDto } from "shared/components/contact/dtos/contact-dto";
import { BackEndService } from "shared/services/back-end/backend.service";
import { Register } from "../dtos/register";


@Injectable({ providedIn: 'root' })


export class ProfileService extends BackEndService<Register> {

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

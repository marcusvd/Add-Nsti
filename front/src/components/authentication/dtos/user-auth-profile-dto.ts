import { AddressDto } from "shared/components/address/dtos/address-dto";
import { ContactDto } from "shared/components/contact/dtos/contact-dto";
import { RootBase } from "shared/entities-dtos/root-base";
import { UserAccountAuthDto } from "./user-account-auth-dto";
import { UserProfileDto } from "./user-profile-dto";

export class UserAuthProfileDto extends RootBase {

  // email!: string;
  // displayUserName!: string;
  // userName!: string;
  // address!: AddressDto;
  // contact!: ContactDto;
  userAccountAuth!: UserAccountAuthDto;
  userAccountProfile!: UserProfileDto;


}

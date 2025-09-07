import { AddressDto } from "shared/components/address/dtos/address-dto";
import { ContactDto } from "shared/components/contact/dtos/contact-dto";
import { RootBase } from "shared/entities-dtos/root-base";

export class UserAccountProfileUpdateDto extends RootBase {
  address!: AddressDto;
  contact!: ContactDto;
}

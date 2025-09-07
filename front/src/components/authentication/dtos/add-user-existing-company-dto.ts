import { AddressDto } from "shared/components/address/dtos/address-dto";
import { ContactDto } from "shared/components/contact/dtos/contact-dto";

export class AddUserExistingCompanyDto {
  companyAuthId!: number;
  email!: string;
  userName!: string;
  password!: string;
  address!: AddressDto;
  contact!: ContactDto;
}

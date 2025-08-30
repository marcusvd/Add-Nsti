import { AddressDto } from "shared/components/address/dtos/address-dto";
import { ContactDto } from "shared/components/contact/dtos/contact-dto";

export class CompanyProfile {
  companyAuthId!: number;
  businessProfileId!: number;
  businessProfile!: number;
  address!: AddressDto;
  contact!: ContactDto;
  deleted!: Date;
  Registered!: Date;
}

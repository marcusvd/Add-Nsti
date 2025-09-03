import { AddressDto } from "shared/components/address/dtos/address-dto";
import { ContactDto } from "shared/components/contact/dtos/contact-dto";
import { RootBase } from "shared/entities-dtos/root-base";

export class EmployeeDto extends RootBase {
  name!: string;
  abbreviatedName!: string;
  userId!:number;
  companyId!:number;
  description!: string;
  address!: AddressDto;
  contact!: ContactDto;
}

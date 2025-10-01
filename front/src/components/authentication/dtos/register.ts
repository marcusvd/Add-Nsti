import { AddressDto } from "shared/components/address/dtos/address-dto";
import { ContactDto } from "shared/components/contact/dtos/contact-dto";

export class Register {
  email!: string;
  userName!: string;
  password!: string;
  confirmPassword!: string;
  companyName!: string;
  cnpj!: string;
  address!: AddressDto;
  contact!: ContactDto;
}

import { CompanyAuth } from "components/authentication/dtos/company-auth";
import { AddressDto } from "shared/components/address/dtos/address-dto";
import { ContactDto } from "shared/components/contact/dtos/contact-dto";
import { RootBase } from "shared/entities-dtos/root-base";

export class AddCompanyUpdateBusinessAuth extends RootBase {
  businessProfileId!: string;
  cnpj!: string;
  company!: CompanyAuth;
  address!: AddressDto;
  contact!: ContactDto;
}

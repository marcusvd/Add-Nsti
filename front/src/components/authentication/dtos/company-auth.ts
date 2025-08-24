import { BusinessAuth } from "./business-auth";
import { CompanyUserAccount } from "./company-user-account";


export class CompanyAuth {
  id!: number;
  addressId!: number;
  contactId!: number;
  name!:string;
  businessId!: number;
  business!: BusinessAuth;
  companyUserAccounts!: CompanyUserAccount[];
}

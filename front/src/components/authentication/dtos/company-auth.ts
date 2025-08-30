import { CompanyUserAccount } from "./company-user-account";


export class CompanyAuth {
  id!: number;
  businessId!: number;
  name!: string;
  tradeName!: string;
  cnpj!: string;
  companyProfileId!: string;
  companyUserAccounts!: CompanyUserAccount[];
  deleted!: Date;
  Registered!: Date;

}

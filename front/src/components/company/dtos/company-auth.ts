import {CompanyUserAccount} from '../../authentication/dtos/company-user-account';

export class CompanyAuth {
  id!: number;
  businessId!: number;
  name!: string;
  tradeName!: string;
  // companyProfileId!: string;
  cnpj!:string;
  companyUserAccounts!: CompanyUserAccount[];
  deleted!: Date;
  Registered!: Date;

}

import { CompanyAuth } from "./company-auth";
import { UserAccount } from "./user-account";

export class CompanyUserAccount
{
     companyAuthId!: number;
     companyAuth!: CompanyAuth;

     userAccountId!: number;
     userAccount!: UserAccount;

     LinkedOn!: Date;
}


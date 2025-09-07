import { CompanyAuth } from "./company-auth";
import { UserAccountAuthDto } from "../../authentication/dtos/user-account-auth-dto";

export class CompanyUserAccount
{
     companyAuthId!: number;
     companyAuth!: CompanyAuth;

     userAccountId!: number;
     userAccount!: UserAccountAuthDto;

     LinkedOn!: Date;
}


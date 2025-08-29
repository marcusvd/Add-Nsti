
import { CompanyAuth } from "./company-auth";
import { CompanyUserAccount } from "./company-user-account";
import { UserAccount } from "./user-account";
import { RootBase } from "shared/entities-dtos/root-base";

export class BusinessAuth extends RootBase {
  name!: string;
  businessProfileId!:string;
  usersAccounts!: UserAccount[];
  companies!: CompanyAuth[];

}

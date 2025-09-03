
import { RootBase } from "shared/entities-dtos/root-base";
import { CompanyAuth } from "./company-auth";
import { UserAccount } from "./user-account";

export class BusinessAuth extends RootBase {
  name!: string;
  businessProfileId!:string;
  usersAccounts!: UserAccount[];
  companies!: CompanyAuth[];

}

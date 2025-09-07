
import { RootBase } from "shared/entities-dtos/root-base";
import { CompanyAuth } from "./company-auth";
import { UserAccountAuthDto } from "../../authentication/dtos/user-account-auth-dto";

export class BusinessAuth extends RootBase {
  name!: string;
  businessProfileId!:string;
  usersAccounts!: UserAccountAuthDto[];
  companies!: CompanyAuth[];

}

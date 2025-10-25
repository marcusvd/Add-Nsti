
import { RootBase } from "shared/entities-dtos/root-base";

import { UserAccountAuthDto } from "../../authentication/dtos/user-account-auth-dto";
import { CompanyAuth } from "components/company/dtos/company-auth";

export class BusinessAuth extends RootBase {
  name!: string;
  businessProfileId!:string;
  usersAccounts!: UserAccountAuthDto[];
  companies!: CompanyAuth[];

}

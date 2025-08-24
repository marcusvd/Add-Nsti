import { CompanyAuth } from "components/authentication/dtos/company-auth";
import { UserAccount } from "../../components/authentication/dtos/user-account";

export abstract class RootBase {
  id!: number;
  userId!: number;
  user?: UserAccount;
  companyId!: number;
  company?: CompanyAuth;
  deleted!: Date;
  registered!: Date;
}

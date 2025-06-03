import { MyUser } from "../../components/authentication/dtos/my-user";
import { CompanyDto } from "./company-dto";

export abstract class RootBase {
  id!: number;
  userId!: number;
  user?: MyUser;
  companyId!: number;
  company?: CompanyDto;
  deleted!: Date;
  registered!: Date;
}

import { Injectable } from "@angular/core";


import { CompanyAuth } from "components/authentication/dtos/company-auth";
import { BackEndService } from "../../../shared/services/back-end/backend.service";


@Injectable({ providedIn: 'root' })
export class AddUserCompanyService extends BackEndService<CompanyAuth> {

  constructor() {
    super();
  }


}

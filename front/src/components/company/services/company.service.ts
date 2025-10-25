import { inject, Injectable } from "@angular/core";
import { FormGroup } from "@angular/forms";

import { BackEndService } from "../../../shared/services/back-end/backend.service";
import { AddCompanyUpdateBusinessAuth } from "../../company/components/add/dto/add-company-update-business/add-company-update-business-auth";
import { CompanyProfile } from "components/company/dtos/company-profile";
import { environment } from "environments/environment";
import { Subscription } from "rxjs";
import { WarningsService } from "components/warnings/services/warnings.service";
import { CompanyAuth } from "components/company/dtos/company-auth";
import { ApiResponse } from "components/authentication/two-factor-enable/dtos/authenticator-setup-response";
import { CompaniesQts } from "components/authentication/dtos/companies-qts";
import { Update_Auth_ProfileDto } from "../dtos/update-auth_profile-dto";



@Injectable({ providedIn: 'root' })
export class CompanyService extends BackEndService<CompanyAuth> {

  private _warningsService = inject(WarningsService);

  getCompaniesByUserId$ = (userId: number) => this.loadById$<CompanyAuth[]>(`${environment._BACK_END_ROOT_URL}/_companies/GetCompaniesByUserIdAsync`, userId.toString());
  getAmountCompaniesByUserId$ = (userId: number) => this.loadById$<ApiResponse<CompaniesQts>>(`${environment._BACK_END_ROOT_URL}/_companies/GetAmountCompaniesByUserIdAsync`, userId.toString());
  companyMoreThenOne$ = (userId: number) => this.getAmountCompaniesByUserId$(userId);

  update_Auth_Profile$ = (update: Update_Auth_ProfileDto) => this.updateV2$<boolean>('http://localhost:5156/api/_companies/UpdateCompany_Auth_Profile', update);



  save(form: FormGroup) {

  form.get('address')

  const toSave: AddCompanyUpdateBusinessAuth = { ...form.value }

  this.update$<AddCompanyUpdateBusinessAuth>('http://localhost:5156/api/BaseManagement/UpdateBusinessAddCompanyAsync', toSave).subscribe({
    next: (_cli: AddCompanyUpdateBusinessAuth) => {
      this.openSnackBar('Cadastrado, com sucesso., ' + toSave.company.name + '!', 'warnings-success');

      this.callRouter(`/users/adm-list/${toSave.id}`);


    },
    error: (err) => {
      console.log(err)
      const erroCode: string = err.error.Message
    }
  })

}

}

@Injectable({ providedIn: 'root' })
export class CompanyProfileService extends BackEndService<CompanyProfile> {

  constructor() {
    super();
  }


}

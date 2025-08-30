import { inject, Injectable } from "@angular/core";
import { FormGroup } from "@angular/forms";

import { BackEndService } from "../../../shared/services/back-end/backend.service";
import { CompanyAuth } from "components/authentication/dtos/company-auth";
import { AddCompanyUpdateBusinessAuth } from "../../company/components/add/dto/add-company-update-business/add-company-update-business-auth";
import { CompanyProfile } from "components/company/dtos/company-profile";



@Injectable({ providedIn: 'root' })
export class CompanyService extends BackEndService<CompanyAuth> {

  constructor() {
    super();
  }

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

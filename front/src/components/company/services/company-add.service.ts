import { inject, Injectable } from "@angular/core";
import { FormGroup } from "@angular/forms";

import { BackEndService } from "../../../shared/services/back-end/backend.service";
import { CompanyAuth } from "components/authentication/dtos/company-auth";
import { AddCompanyUpdateBusinessAuth } from "../components/add/dto/add-company-update-business/add-company-update-business-auth";



@Injectable({ providedIn: 'root' })
export class CompanyAddService extends BackEndService<AddCompanyUpdateBusinessAuth> {

  constructor() {
    super();
  }



  save(form: FormGroup) {
    const toSave: AddCompanyUpdateBusinessAuth = { ...form.value }
    console.log(toSave)
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

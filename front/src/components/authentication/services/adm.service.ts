import { Injectable } from "@angular/core";
import { BackEndService } from "shared/services/back-end/backend.service";
import { take } from "rxjs";
import { FormGroup } from "@angular/forms";
import { Register } from "../dtos/register";


@Injectable({ providedIn: 'root' })


export class  AdmService extends BackEndService<Register> {

  constructor() { super() }

  AddUser(user: Register, form: FormGroup, url:string) {

    return this.add$<Register>(user, url).pipe(take(1));
  }

}

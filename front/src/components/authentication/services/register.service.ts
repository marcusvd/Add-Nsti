import { Injectable } from "@angular/core";
import { BackEndService } from "shared/services/back-end/backend.service";
import { take } from "rxjs";
import { FormGroup } from "@angular/forms";
import { RegisterDto } from "../dtos/register-dto";


@Injectable({ providedIn: 'root' })


export class RegisterService extends BackEndService<RegisterDto> {

  constructor() { super() }

  AddUser(user: RegisterDto, form: FormGroup, url:string) {

    return this.add$<RegisterDto>(user, url).pipe(take(1));
  }

}

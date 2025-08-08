import { AbstractControl, AsyncValidator, ValidationErrors, ValidatorFn } from "@angular/forms";
import { AccountService } from "../services/account.service";
import { map, Observable, of } from "rxjs";
import { environment } from "environments/environment";
import { HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BackEndService } from "shared/services/back-end/backend.service";
import { MyUser } from "../dtos/my-user";


@Injectable({ providedIn: 'root' })
export class IsUserRegisteredValidator extends  BackEndService<MyUser> implements AsyncValidator {

  constructor() {super()}

  validate(control: AbstractControl): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> {

    if (!control.dirty)
      return of(null)

    if (control.value.includes('@') && control.value.includes('.')) {

      return this.loadByName$<boolean>(`${environment._BACK_END_ROOT_URL}/auth/IsUserExistCheckByEmailAsync`, control.value)
        .pipe(map(x => {
          return x ? { inUse: true } : null;
        }))
    }

    return of(null);

  }
}
// export class IsUserRegisteredValidator implements AsyncValidator {

//   constructor(private _accountService: AccountService) { }

//   validate(control: AbstractControl): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> {

//     if (!control.dirty)
//       return of(null)

//     if (control.value.includes('@') && control.value.includes('.')) {
//       return this._accountService.getUserByEmail$(`${environment._BACK_END_ROOT_URL}/auth/IsUserExistCheckByEmail`, control.value)
//         .pipe(map(x => {
//           return x ? { inUse: true } : null;
//         }))
//     }

//     return of(null);

//   }
// }

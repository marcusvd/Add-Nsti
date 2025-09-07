import { Injectable } from "@angular/core";
import { CommunicationAlerts } from "shared/services/messages/snack-bar.service";
import { UserAccountAuthDto } from "../../authentication/dtos/user-account-auth-dto";
import { MatSnackBar } from "@angular/material/snack-bar";
import { BackEndService } from "shared/services/back-end/backend.service";
import { LoginDto } from "../dtos/login-dto";
import { Router } from "@angular/router";
import { UserTokenDto } from "../dtos/user-token-dto";
import { environment } from "environments/environment";


@Injectable({ providedIn: 'root' })

export class LoginService extends BackEndService<UserTokenDto> {

  constructor(
    // private _router: Router
  ) { super() }


  login$(user: UserAccountAuthDto) {
    return this.add$<UserAccountAuthDto>(user, `${environment._BACK_END_ROOT_URL}/auth/LoginAsync`);
  }

  logOut() {
    this.callRouter('/login')
    // this.openDialogLogin();
    localStorage.clear();
    // this._communicationsAlerts.defaultSnackMsg('5', 0, null, 4);
    // this.currentUserSubject.complete();
    // this.currentUserSubject.next(null);
    // this.currentUser = null;
  }





}

import { Injectable } from "@angular/core";
import { CommunicationAlerts } from "shared/services/messages/snack-bar.service";
import { UserAccountAuthDto } from "../../authentication/dtos/user-account-auth-dto";
import { MatSnackBar } from "@angular/material/snack-bar";
import { BackEndService } from "shared/services/back-end/backend.service";
import { LoginDto } from "../dtos/login-dto";
import { Router } from "@angular/router";
import { UserTokenDto } from "../dtos/user-token-dto";
import { environment } from "environments/environment";
import { ApiResponse } from "../two-factor-enable/dtos/authenticator-setup-response";
import { ResendConfirmEmail } from "../dtos/resend-confirm-email";


@Injectable({ providedIn: 'root' })

export class LoginService extends BackEndService<UserTokenDto> {

  constructor(
    // private _router: Router
  ) { super() }


  login$(user: LoginDto) {
    return this.add$<ApiResponse<any>>(user, `${environment._BACK_END_ROOT_URL}/auth/LoginAsync`);
  }

  logOut() {
     return this.add$<ApiResponse<any>>(null, `${environment._BACK_END_ROOT_URL}/auth/LogoutAsync`);
  }

  resendConfirmEmailAsync(emailConfirm:ResendConfirmEmail) {
     return this.add$<ApiResponse<any>>(emailConfirm, `${environment._BACK_END_ROOT_URL}/auth/ResendConfirmEmailAsync`);
  }





}

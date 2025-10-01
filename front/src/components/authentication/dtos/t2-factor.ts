export class T2Factor {
  userId!: number;
  token!: string;
}
export class TwoFactorStatusViewModel {
  isEnabled!: boolean;
  hasAuthenticator!: boolean;
  recoveryCodesLeft!: number;
}

export class TwoFactorToggleDto {
  userId!: number;
  enable: boolean = false;
}
export class ToggleAuthenticatorRequestViewModel {
  enabled!: boolean;
  code!: string;

}



// export class TwoFactorCheckDto {
//   email!: string;
//   token!: string;
// }

export class VerifyTwoFactorRequest {

  email!: string;
  provider!: string;
  token!: string;
  rememberMe!: boolean;
}
export class OnOff2FaCodeViaEmail {

  email!: string;
  onOff!: boolean;
}

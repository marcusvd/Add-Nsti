export class AuthenticatorSetupResponse {
  sharedKey!: string;
  authenticatorUri!: string;
  isTwoFactorEnabled!: boolean;
  errors!: string;
  message!: string;
  success!: boolean;

}

export interface EnableAuthenticatorRequest {
  code: string;
}

export interface EnableAuthenticatorResponse {
  recoveryCodes: string[];
  isTwoFactorEnabled: boolean;
}


export interface AuthenticatorSetupResponse {
  sharedKey: string;
  authenticatorUri: string;
  isTwoFactorEnabled: boolean;
}

export interface EnableAuthenticatorRequest {
  code: string;
}

export interface EnableAuthenticatorResponse {
  recoveryCodes: string[];
  isTwoFactorEnabled: boolean;
}

export interface ApiResponse<T> {
  success: boolean;
  message: string;
  data: T;
  errors: string[];
}

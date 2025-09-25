namespace Application.Services.Operations.Auth.Dtos;

// public class EnableTwoFactorRequestViewModel
// {
//     public required string Code { get; set; }
// }

public class TwoFactorToggleViewModel
{
    public int UserId { get; set; } = 0;
    public bool Enable { get; set; } = false;
}

public class TwoFactorStatusViewModel
{
    public bool IsEnabled { get; set; }
    public bool HasAuthenticator { get; set; }

    public int RecoveryCodesLeft { get; set; }
}


// public class TwoFactorResponseViewModel
// {
//     public bool Success { get; set; }
//     public string Message { get; set; }
//     public string[] RecoveryCodes { get; set; } = [];
//     public string SharedKey { get; set; } = string.Empty;
//     public string AuthenticatorUri { get; set; } = string.Empty;
// }

// public class VerifyTwoFactorRequestViewModel
// {
//     public string Code { get; set; } = string.Empty;
//     public bool RememberMachine { get; set; }
//     public bool RememberMe { get; set; }

// }

public class RecoveryCodeRequestViewModel
{
    public string RecoveryCode { get; set; } = string.Empty;
}

public class ResponseIdentiyApiDto
{
   public bool succeeded { get; set; }
   public ErrosDto[] errosDtos { get; set; }
}

public  class ErrosDto
{
    public bool code { get; set; }
    public string description { get; set; } = string.Empty;
}

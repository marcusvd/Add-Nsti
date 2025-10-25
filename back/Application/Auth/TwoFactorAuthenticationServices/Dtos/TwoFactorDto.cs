using System.ComponentModel.DataAnnotations;

namespace Application.Auth.TwoFactorAuthentication.Dtos;


public class OnOff2FaCodeViaEmailDto
{
    [Required]
    public bool OnOff { get; set; } = true;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}

public class VerifyTwoFactorRequestDto
{
    public required string Email { get; set; }
    public string Provider { get; set; } = "Email";
    public required string Code { get; set; }
    public bool RememberMe { get; set; } = true;
}

public class ToggleAuthenticatorRequestDto
{
    public bool Enabled { get; set; }
    public required string Code { get; set; }
}

public class EnableAuthenticatorResponseDto
{
    public string[] RecoveryCodes { get; set; } = [];
    public bool IsTwoFactorEnabled { get; set; } = false;
}

public class AuthenticatorSetupResponseDto
{
    public string SharedKey { get; set; }
    public string AuthenticatorUri { get; set; }
    public bool IsTwoFactorEnabled { get; set; }
}
public class TwoFactorStatusDto
{
    public bool IsEnabled { get; set; }
    public bool HasAuthenticator { get; set; }

    public int RecoveryCodesLeft { get; set; }
}
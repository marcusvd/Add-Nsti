using System.ComponentModel.DataAnnotations;
using Org.BouncyCastle.Asn1.Misc;

namespace Application.Services.Operations.Auth.Account.dtos;

public class LoginModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Lembrar-me?")]
    public bool RememberMe { get; set; }
}

// Models/VerifyTwoFactorModel.cs
public class VerifyTwoFactorModel
{
    [Required]
    [Display(Name = "Token 2FA")]
    public string Token { get; set; } = string.Empty;

    [Display(Name = "Lembrar-me?")]
    public bool RememberMe { get; set; }

    public string? ReturnUrl { get; set; }
    public string? SelectedProvider { get; set; }
    public IList<string>? Providers { get; set; }

    [EmailAddress]
    public string? Email { get; set; } // Para caso precise recarregar
}

// Models/EnableAuthenticatorModel.cs
public class EnableAuthenticatorModel
{
    [Required]
    [StringLength(7, ErrorMessage = "the {0} must be at least {2} and at most {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Text)]
    [Display(Name = "Verification Code")]
    public string Code { get; set; } = string.Empty;

    public string SharedKey { get; set; } = string.Empty;
    public string AuthenticatorUri { get; set; } = string.Empty;
}

public class OnOff2FaCodeViaEmailViewModel
{
    [Required]
    public bool OnOff { get; set; } = true;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T Data { get; set; }
    public IEnumerable<string> Errors { get; set; } = [];
}
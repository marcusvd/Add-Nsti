using System.ComponentModel.DataAnnotations;

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
    [StringLength(7, ErrorMessage = "O {0} deve ter pelo menos {2} e no máximo {1} caracteres.", MinimumLength = 6)]
    [DataType(DataType.Text)]
    [Display(Name = "Código de Verificação")]
    public string Code { get; set; } = string.Empty;

    public string SharedKey { get; set; } = string.Empty;
    public string AuthenticatorUri { get; set; } = string.Empty;
}
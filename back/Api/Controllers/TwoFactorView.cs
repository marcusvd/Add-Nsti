// DTOs (Data Transfer Objects)
public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}

public class VerifyTwoFactorRequestViewModel
{
    public string Email { get; set; }
    public string Provider { get; set; } = "Email";
    public string Token { get; set; }
    public bool RememberMe { get; set; } = true;
}

public class ToggleAuthenticatorRequestViewModel
{

    public bool Enabled { get; set; }

    public required string Code { get; set; }
}






public class RegisterRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public IEnumerable<string> Errors { get; set; }
}

public class LoginResponse
{
    public int UserId { get; set; }
    public string Email { get; set; }
    public bool RequiresTwoFactor { get; set; }
    public IList<string> TwoFactorProviders { get; set; }
    public IList<string> Roles { get; set; }
}

public class TokenResponse
{
    public string Token { get; set; }
    public int ExpiresIn { get; set; }
    public UserInfoResponse User { get; set; }
}

public class UserInfoResponse
{
    public int Id { get; set; }
    public string Email { get; set; }
    public IList<string> Roles { get; set; }
}

public class EnableAuthenticatorResponse
{
    public string[] RecoveryCodes { get; set; }
    public bool IsTwoFactorEnabled { get; set; }
}

public class AuthenticatorSetupResponse
{
    public string SharedKey { get; set; }
    public string AuthenticatorUri { get; set; }
    public bool IsTwoFactorEnabled { get; set; }
}
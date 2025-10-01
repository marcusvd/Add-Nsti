using Domain.Entities.Authentication;
using Application.Services.Operations.Auth.Dtos;
using UnitOfWork.Persistence.Operations;
using Application.Services.Operations.Auth;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Application.Services.Operations.Auth.Account.dtos;
using System.Reflection.Metadata;

namespace Authentication.Operations.TwoFactorAuthentication;

public class TwoFactorAuthenticationServices : AuthenticationBase, ITwoFactorAuthenticationServices
{
    private readonly IUnitOfWork _GENERIC_REPO;
    private readonly IAuthServicesInjection _AUTH_SERVICES_INJECTION;

    public TwoFactorAuthenticationServices(
                    IUnitOfWork GENERIC_REPO,
                    IAuthServicesInjection AUTH_SERVICES_INJECTION

      ) : base(GENERIC_REPO, AUTH_SERVICES_INJECTION)
    {
        // _genericValidatorServices = genericValidatorServices;
        _GENERIC_REPO = GENERIC_REPO;
        _AUTH_SERVICES_INJECTION = AUTH_SERVICES_INJECTION;
    }


    public async Task<TwoFactorStatusViewModel> GetTwoFactorStatus(int userId)
    {
        var userAccount = await FindUserByIdAsync(userId);
        _GENERIC_REPO._GenericValidatorServices.IsObjNull(userAccount);

        var is2FaEnable = await _AUTH_SERVICES_INJECTION.UsersManager.GetTwoFactorEnabledAsync(userAccount);
        var hasAuthenticator = await _AUTH_SERVICES_INJECTION.UsersManager.GetAuthenticatorKeyAsync(userAccount) != null;
        var recoveryCodesLeft = await _AUTH_SERVICES_INJECTION.UsersManager.CountRecoveryCodesAsync(userAccount);

        return new TwoFactorStatusViewModel()
        {
            IsEnabled = is2FaEnable,
            HasAuthenticator = hasAuthenticator,
            RecoveryCodesLeft = recoveryCodesLeft,
        };
    }
    public async Task<IdentityResult> OnOff2FaCodeViaEmailAsync(OnOff2FaCodeViaEmailViewModel request)
    {
        var userAccount = await FindUserAsync(request.Email);

        _GENERIC_REPO._GenericValidatorServices.IsObjNull(userAccount);

        if (request.OnOff)
            userAccount.Code2FaSendEmail = DateTime.MinValue;
        else
            userAccount.Code2FaSendEmail = DateTime.Now;

        return await _AUTH_SERVICES_INJECTION.UsersManager.UpdateAsync(userAccount);

    }

    public async Task<bool> HandleTwoFactorAuthenticationAsync(UserAccount userAccount)
    {
        if (!await _AUTH_SERVICES_INJECTION.UsersManager.GetTwoFactorEnabledAsync(userAccount))
            return false;

        var validProviders = await _AUTH_SERVICES_INJECTION.UsersManager.GetValidTwoFactorProvidersAsync(userAccount);

        if (!validProviders.Contains("Email"))
            return false;

        //    var d = _AUTH_SERVICES_INJECTION.HttpContextAccessor?.HttpContext?.SignInAsync(IdentityConstants.TwoFactorUserIdScheme, Store2FA(userAccount.Id, "Email"));


        // var token = await _GENERIC_REPO.UsersManager.GenerateTwoFactorTokenAsync(userAccount, "Email");
        // var token = await GenerateTwoFactorTokenAsync(userAccount, "twofactorverify", "auth");

        await _AUTH_SERVICES_INJECTION.SignInManager.RememberTwoFactorClientAsync(userAccount);


        // var providers = await _AUTH_SERVICES_INJECTION.UsersManager.GetValidTwoFactorProvidersAsync(userAccount);


        var token = await _AUTH_SERVICES_INJECTION.UsersManager.GenerateTwoFactorTokenAsync(userAccount, "Email");

        string linkToken = $"http://localhost:4200/two-factor-check/{token}" ;
        // string linkToken = $"http://localhost:4200/two-factor-check/{token}/{userAccount.Email}";



        if (userAccount.Code2FaSendEmail == DateTime.MinValue)
            await SendTwoFactorTokenAsync(userAccount, token);
        // if (userAccount.Code2FaSendEmail == DateTime.MinValue)
        //     await SendTwoFactorTokenAsync(userAccount, linkToken);

        return true;
    }


    private async Task SendTwoFactorTokenAsync(UserAccount userAccount, string token)
    {
       string styleCss = @"
          body {
            font-family: 'Segoe UI', Arial, sans-serif;
            line-height: 1.6;
            color: #333;
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
            background-color: #f9f9f9;
        }
        .container {
            background-color: #ffffff;
            border-radius: 8px;
            padding: 30px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
            border: 1px solid #e1e1e1;
        }
        .header {
            text-align: center;
            margin-bottom: 25px;
            border-bottom: 1px solid #eeeeee;
            padding-bottom: 20px;
        }
        .logo {
            color: #0556cb;
            font-size: 24px;
            font-weight: bold;
            margin-bottom: 10px;
        }
        .title {
            font-size: 20px;
            font-weight: 600;
            margin: 15px 0;
            color: #1a1a1a;
        }
        .code-container {
            background-color: #f0f7ff;
            border: 1px dashed #0556cb;
            border-radius: 6px;
            padding: 20px;
            text-align: center;
            margin: 25px 0;
        }
        .verification-code {
            font-size: 42px;
            font-weight: bold;
            letter-spacing: 5px;
            color: #0556cb;
            margin: 15px 0;
        }
        .info-box {
            background-color: #f8f9fa;
            border-left: 4px solid #0556cb;
            padding: 15px;
            margin: 20px 0;
            border-radius: 0 4px 4px 0;
        }
        .details-grid {
            display: grid;
            grid-template-columns: 1fr;
            gap: 12px;
            margin: 20px 0;
        }
        .detail-item {
            display: flex;
        }
        .detail-label {
            font-weight: 600;
            min-width: 120px;
        }
        .footer {
            margin-top: 30px;
            padding-top: 20px;
            border-top: 1px solid #eeeeee;
            font-size: 14px;
            color: #666;
            text-align: center;
        }
        .warning {
            color: #d32f2f;
            font-weight: 600;
            margin: 15px 0;
        }
        .support-link {
            color: #0556cb;
            text-decoration: none;
        }
        .support-link:hover {
            text-decoration: underline;
        }
        @media (max-width: 480px) {
            body {
                padding: 10px;
            }
            .container {
                padding: 20px 15px;
            }
            .verification-code {
                font-size: 36px;
                letter-spacing: 3px;
            }
        }
       ";
       
        string htmlString = @$"
<!DOCTYPE html>
<html lang=""pt-BR"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <style>
     {styleCss}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""header"">
            <div class=""logo"">I.M Integrações</div>
            <h1 class=""title"">Autenticação de Login 2FA</h1>
        </div>
        
        <p>Use o código abaixo para autenticar sua tentativa de login.</p>
  

        <div class=""code-container"">
            <div class=""verification-code"">{token}</div>
            <p>O código permanecerá válido pelos próximos 10 minutos.</p>
        </div>
        
        <div class="" warning"">⚠️ Não compartilhe este código com ninguém.</div>
        
        <p>Se você não reconhece esta tentativa de login, ignore este e-mail e verifique a segurança de sua conta.</p>
        
        <div class=""footer"">
            <p><strong>I.M Integrações</strong><br>
            Caso tenha fechado a tela apos o login.  <a href=""http://localhost:4200/two-factor-check"" class=""support-link"">Clique aqui</a>.</p>
        </div>
    </div>
</body>
</html>";
        try
        {
            await SendAsync(To: userAccount.Email,
                    Subject: "I.M: Autenticação de dois fatores",
                    Body: htmlString);
            // Body: $"Código: Autenticação de dois fatores: {"http://localhost:4200/two-factor-check"}{token.Replace("api/auth/twofactorverify", "")}");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }




    private ClaimsPrincipal Store2FA(int id, string provider)
    {
        var identity = new ClaimsIdentity(new List<Claim>
        {
            new Claim("sub", id.ToString()),
            new Claim("amr", provider)

        }, IdentityConstants.TwoFactorUserIdScheme);


        return new ClaimsPrincipal(identity);
    }

    // private protected async Task<UserToken> CreateAuthenticationResponseAsync(UserAccount userAccount)
    // {
    //     var claimsList = await BuildUserClaims(userAccount);
    //     var roles = await _AUTH_SERVICES_INJECTION.UsersManager.GetRolesAsync(userAccount);
    //     var token = await _GENERIC_REPO.JwtHandler.GenerateUserToken(claimsList, userAccount, roles);
    //     return token;
    // }




    // private protected async Task<bool> IsEnabledTwoFactorAsync(UserAccount userAccount) => await _AUTH_SERVICES_INJECTION.UsersManager.GetTwoFactorEnabledAsync(userAccount);

    // private protected async Task<bool> VerifyTwoFactorTokenAsync(string email, string token)
    // {

    //     if (_AUTH_SERVICES_INJECTION.HttpContextAccessor?.HttpContext == null)
    //         return false;


    //     var httpContext = _AUTH_SERVICES_INJECTION.HttpContextAccessor.HttpContext;


    //     var result = await httpContext.AuthenticateAsync(IdentityConstants.TwoFactorUserIdScheme);

    //     if (!result.Succeeded) return false;

    //     var userId = result.Principal.FindFirstValue("sub");


    //     var userAccount = await _AUTH_SERVICES_INJECTION.UsersManager.FindByIdAsync(userId);


    //     _GENERIC_REPO._GenericValidatorServices.IsObjNull<UserAccount>(userAccount);

    //     var tokenProvider = result.Principal.FindFirstValue("amr");

    //     var isValid = await _AUTH_SERVICES_INJECTION.UsersManager.VerifyTwoFactorTokenAsync(userAccount, tokenProvider, token);

    //     if (!isValid) return false;

    //     await httpContext.SignOutAsync(IdentityConstants.TwoFactorUserIdScheme);

    //     var claimsPrincipal = await _AUTH_SERVICES_INJECTION.UserClaimsPrincipalFactory.CreateAsync(userAccount);

    //     await httpContext.SignInAsync(IdentityConstants.ApplicationScheme, claimsPrincipal);

    //     // var userAccount = await FindUserAsync(email);

    //     // _GENERIC_REPO._GenericValidatorServices.IsObjNull(userAccount);

    //     // if (!await _AUTH_SERVICES_INJECTION.UsersManager.GetTwoFactorEnabledAsync(userAccount)) return false;

    //     // var validProviders = await _AUTH_SERVICES_INJECTION.UsersManager.GetValidTwoFactorProvidersAsync(userAccount);

    //     // if (!validProviders.Contains("Email"))
    //     //     return false;

    //     // var isValid = await _AUTH_SERVICES_INJECTION.UsersManager.VerifyTwoFactorTokenAsync(userAccount, "Email", token);

    //     // if (isValid)
    //     // {
    //     //     await _GENERIC_REPO.SignInManager.SignInAsync(userAccount, isPersistent: false);
    //     //     return true;
    //     // }

    //     return false;
    // }

    // public async Task<bool> TwoFactorVerifyAsync(string email, string token) => await VerifyTwoFactorTokenAsync(email, token);





    // private protected async Task<List<Claim>> BuildUserClaims(UserAccount userAccount)
    // {
    //     var getRoles = await _AUTH_SERVICES_INJECTION.UsersManager.GetRolesAsync(userAccount);

    //     var claims = new List<Claim>
    //     {

    //         new Claim("sub", userAccount.Id.ToString()),
    //         new Claim("amr", "Email")
    //         //  new Claim(ClaimTypes.NameIdentifier, userAccount.Id.ToString()),
    //         //   new Claim(ClaimTypes.Name, userAccount.UserName!),
    //         // new Claim(ClaimTypes.Name, userAccount.Email!),
    //     };

    //     foreach (var role in getRoles)
    //         claims.Add(new Claim(ClaimTypes.Role, role));


    //     return claims;
    // }


}

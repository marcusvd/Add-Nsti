using Domain.Entities.Authentication;
using Application.Exceptions;
using Application.Services.Operations.Auth.Dtos;
using Application.Services.Operations.Companies.Dtos;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.System.BusinessesCompanies;
using Application.Services.Shared.Dtos;
using UnitOfWork.Persistence.Operations;
using Application.Services.Operations.Auth;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Authentication.Jwt;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;
using Application.Services.Operations.Auth.Account.dtos;

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
    public async Task<IdentityResult> TwoFactorToggleAsync(TwoFactorToggleViewModel toggleTwoFactor)
    {
        var userAccount = await FindUserByIdAsync(toggleTwoFactor.UserId);

        _GENERIC_REPO._GenericValidatorServices.IsObjNull(userAccount);

        return await _AUTH_SERVICES_INJECTION.UsersManager.SetTwoFactorEnabledAsync(userAccount, toggleTwoFactor.Enable);
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

        string linkToken = $"http://localhost:4200/two-factor-check/{token}/{userAccount.Email}";

        if (userAccount.Code2FaSendEmail == DateTime.MinValue)
            await SendTwoFactorTokenAsync(userAccount, linkToken);

        return true;
    }


    private async Task SendTwoFactorTokenAsync(UserAccount userAccount, string token)
    {
        try
        {
            await SendAsync(To: userAccount.Email,
                    Subject: "I.M: Autenticação de dois fatores",
                    Body: $"Código: Autenticação de dois fatores: {token}");
            // Body: $"Código: Autenticação de dois fatores: {"http://localhost:4200/two-factor-check"}{token.Replace("api/auth/twofactorverify", "")}");
        }
        catch (Exception ex)
        {
            // 
            // 
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



    public async Task<bool> VerifyTwoFactor(TwoFactorCheckViewModel model)
    {

        // ⭐⭐ ALTERNATIVA PRINCIPAL AO AuthenticateAsync ⭐⭐
        // Recupera o usuário usando SignInManager (em vez de HttpContext.AuthenticateAsync)
        var user = await _AUTH_SERVICES_INJECTION.SignInManager.GetTwoFactorAuthenticationUserAsync();

        if (user == null)
        {
            // Se não encontrou o usuário, a sessão 2FA pode ter expirado

            // Recarrega os provedores para a view
            var tempUser = await _AUTH_SERVICES_INJECTION.UsersManager.FindByEmailAsync(model.Email);
            if (tempUser != null)
            {

                model.Providers = await _AUTH_SERVICES_INJECTION.UsersManager.GetValidTwoFactorProvidersAsync(tempUser);
            }

            return false;


        }

        // Verifica o token 2FA
        var isValidToken = await _AUTH_SERVICES_INJECTION.UsersManager.VerifyTwoFactorTokenAsync(
            user, model.SelectedProvider, model.Token);

        if (!isValidToken)
        {
            model.Providers = await _AUTH_SERVICES_INJECTION.UsersManager.GetValidTwoFactorProvidersAsync(user);
            return false;
        }

        // Login bem-sucedido com 2FA
        var result = await _AUTH_SERVICES_INJECTION.SignInManager.TwoFactorSignInAsync(
            model.SelectedProvider, model.Token, model.RememberMe, rememberClient: false);

        if (result.Succeeded)
        {

            return result.Succeeded;
        }
        else if (result.IsLockedOut)
        {

            return false;
        }
        else
        {
            model.Providers = await _AUTH_SERVICES_INJECTION.UsersManager.GetValidTwoFactorProvidersAsync(user);
            return false;
        }


    }



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

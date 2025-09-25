using Microsoft.AspNetCore.Identity;

using Domain.Entities.Authentication;
using Authentication.Exceptions;
using UnitOfWork.Persistence.Operations;
using Application.Services.Operations.Auth.Account.dtos;
using Application.Services.Operations.Auth.Dtos;
using Authentication.Operations.TwoFactorAuthentication;
using Microsoft.AspNetCore.Authentication;


namespace Application.Services.Operations.Auth.Login;

public partial class LoginServices : AuthenticationBase, ILoginServices
{
    private readonly IAuthServicesInjection _AUTH_SERVICES_INJECTION;
    private readonly IUnitOfWork _GENERIC_REPO;
    // private readonly ServiceLaucherService _SERVICE_LAUCHER_SERVICE;



    public LoginServices(
          IUnitOfWork GENERIC_REPO,
          IAuthServicesInjection AUTH_SERVICES_INJECTION
      //   ServiceLaucherService SERVICE_LAUCHER_SERVICE
      ) : base(GENERIC_REPO, AUTH_SERVICES_INJECTION)
    {
        _AUTH_SERVICES_INJECTION = AUTH_SERVICES_INJECTION;
        _GENERIC_REPO = GENERIC_REPO;
        // _SERVICE_LAUCHER_SERVICE = SERVICE_LAUCHER_SERVICE;
    }

    public async Task<UserToken> LoginAsync(LoginModelDto user)
    {
        _GENERIC_REPO._GenericValidatorServices.IsObjNull(user);

        var userAccount = await FindUserAsync(user.Email);

        // await IsValidUserAccount(userAccount.Email, userAccount.Id == -1);

        //checking if the hour allow access
        var timedAccessControl = await GetTimedAccessControl(userAccount.Id);
        if (!await CheckTimeInterval(timedAccessControl)) throw new AuthServicesException(AuthErrorsMessagesException.TimeIsOutside);
        //

        if (userAccount.WillExpire.Year != DateTime.MinValue.Year)
        {
            await ForgotPasswordAsync(new ForgotPasswordDto() { Email = user.Email });
            throw new AuthServicesException(AuthErrorsMessagesException.PasswordWillExpire);
        }

        _GENERIC_REPO._GenericValidatorServices.IsObjNull(userAccount);

        await ValidateAccountStatusAsync(userAccount);

        // var result = await IsPasswordValidAsync(userAccount, user.Password);
        var result = await LoginAction(userAccount, user.Password);

        // await IsValidUserAccount(userAccount.Email, !result.Succeeded);

        var twoFactor = new TwoFactorAuthenticationServices(_GENERIC_REPO, _AUTH_SERVICES_INJECTION);

        if (await twoFactor.HandleTwoFactorAuthenticationAsync(userAccount))
        {


            //     _logger.LogInformation("2FA required for user: {UserId}", userAccount.Id);
            return await CreateTwoFactorResponse(userAccount);
        }

        // _logger.LogInformation("Successful login for user: {UserId}", userAccount.Id);

        await WriteLastLogin(userAccount.Email);

        return await CreateAuthenticationResponseAsync(userAccount);

        // return new UserToken();
    }




    private async Task<SignInResult> LoginAction(UserAccount userAccount, string pwd)
    {
        var result = await _AUTH_SERVICES_INJECTION.SignInManager.PasswordSignInAsync(userAccount, pwd, true, true);

        if (result.Succeeded)
        {
            // _logger.LogInformation("Usuário logado.");
            // return RedirectToLocal(returnUrl);
        }
        if (result.RequiresTwoFactor)
        {
            return result;
            // Redireciona para página de verificação 2FA
            // return RedirectToAction(nameof(VerifyTwoFactor), new { returnUrl, model.RememberMe });
        }
        if (result.IsLockedOut)
        {
            return result;
            // _logger.LogWarning("Conta bloqueada.");
            // return RedirectToAction(nameof(Lockout));
        }
        else
        {
            return result;
            // ModelState.AddModelError(string.Empty, "Tentativa de login inválida.");
            // return View(model);
        }
    }


    public async Task<UserAccount> GetUser(string email)
    {
        return await FindUserAsync(email);
    }
    // public async Task<UserToken> LoginAsync(LoginModelDto user)
    // {
    //     _GENERIC_REPO._GenericValidatorServices.IsObjNull(user);

    //     var userAccount = await FindUserAsync(user.Email);

    //     await IsValidUserAccount(userAccount.Email, userAccount.Id == -1);

    //     //checking if the hour allow access
    //     var timedAccessControl = await GetTimedAccessControl(userAccount.Id);
    //     if (!await CheckTimeInterval(timedAccessControl)) throw new AuthServicesException(AuthErrorsMessagesException.TimeIsOutside);
    //     //

    //     if (userAccount.WillExpire.Year != DateTime.MinValue.Year)
    //     {
    //         await ForgotPasswordAsync(new ForgotPasswordDto() { Email = user.Email });
    //         throw new AuthServicesException(AuthErrorsMessagesException.PasswordWillExpire);
    //     }

    //     _GENERIC_REPO._GenericValidatorServices.IsObjNull(userAccount);

    //     await ValidateAccountStatusAsync(userAccount);

    //     var result = await IsPasswordValidAsync(userAccount, user.Password);

    //     await IsValidUserAccount(userAccount.Email, !result.Succeeded);

    //     await WriteLastLogin(userAccount.Email);


    //     var twoFactor = new TwoFactorAuthenticationServices(_GENERIC_REPO, _AUTH_SERVICES_INJECTION);

    //     if (await twoFactor.HandleTwoFactorAuthenticationAsync(userAccount))
    //     {
    //         //     _logger.LogInformation("2FA required for user: {UserId}", userAccount.Id);
    //         return await CreateTwoFactorResponse(userAccount);
    //     }

    //     // _logger.LogInformation("Successful login for user: {UserId}", userAccount.Id);

    //     return await CreateAuthenticationResponseAsync(userAccount);

    //     // return new UserToken();
    // }


    private async Task<bool> IsValidUserAccount(string userEmail, bool result)
    {
        if (result)
        {

            // _logger.LogWarning("Invalid password attempt for user: {Email}", userEmail);
            throw new AuthServicesException(AuthErrorsMessagesException.InvalidUserNameOrPassword);
        }

        return await Task.FromResult(result);
    }

    private async Task ValidateAccountStatusAsync(UserAccount userAccount)
    {
        if (await IsAccountLockedOutAsync(userAccount))
        {

            // _logger.LogWarning("Account locked out: {UserId}", userAccount.Id);
            await NotifyAccountLockedAsync(userAccount);
            throw new AuthServicesException(AuthErrorsMessagesException.UserIsLocked);
        }

        if (!await IsEmailConfirmedAsync(userAccount))
        {

            // _logger.LogWarning("Email not confirmed for user: {UserId}", userAccount.Id);
            throw new AuthServicesException(AuthErrorsMessagesException.EmailIsNotConfirmed);
        }
    }

    private async Task NotifyAccountLockedAsync(UserAccount userAccount)
    {
        try
        {
            // await _emailService.SendAsync(To: userAccount.Email,
            //            Subject: "Sonny conta bloqueada",
            //            Body: "O número de dez tentativas de login foi esgotado e a conta foi bloqueada por atingir dez tentativas com senhas incorretas. Sugerimos troque sua senha.");
        }
        catch (Exception ex)
        {
            // _logger.LogError(ex, "Failed to send account locked notification to {Email}", userAccount.Email);
            // _GENERIC_REPO.Logger.LogError(ex, "Failed to send account locked notification to {Email}", userAccount.Email);
        }
    }
    private async Task<IdentityResult> WriteLastLogin(string email)
    {
        var userAccount = await FindUserAsync(email);

        _GENERIC_REPO._GenericValidatorServices.IsObjNull(userAccount);

        userAccount.LastLogin = DateTime.Now;

        var Update = await _AUTH_SERVICES_INJECTION.UsersManager.UpdateAsync(userAccount);

        return Update;
    }
    public async Task<DateTime> GetLastLogin(string email)
    {
        var userAccount = await FindUserAsync(email);

        _GENERIC_REPO._GenericValidatorServices.IsObjNull(userAccount);

        return userAccount.LastLogin;
    }

}
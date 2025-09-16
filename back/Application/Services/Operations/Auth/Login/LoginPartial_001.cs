using Microsoft.AspNetCore.Identity;

using Domain.Entities.Authentication;
using Authentication.Exceptions;
using Authentication.Helpers;
using Authentication.Jwt;
using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Mvc;
using UnitOfWork.Persistence.Operations;
using Application.Services.Operations.Auth.Account.dtos;
using Application.Exceptions;
using Application.Services.Operations.Auth.Dtos;


namespace Application.Services.Operations.Auth.Login;

public partial class LoginServices : AuthenticationBase, ILoginServices
{


    private async Task<TimedAccessControlDto> GetTimedAccessControl(int userId)
    {
        var userIdIncludeTAC = await _GENERIC_REPO.UsersAccounts.GetUserAccountFull(userId);

        if (userIdIncludeTAC.Id == -1) throw new AuthServicesException(GlobalErrorsMessagesException.IsObjNull);

        return userIdIncludeTAC?.TimedAccessControl?.ToDto() ?? (TimedAccessControlDto)_GENERIC_REPO._GenericValidatorServices.ReplaceNullObj<TimedAccessControlDto>();
    }

    private async Task<bool> CheckTimeInterval(TimedAccessControlDto timedAccessControl)
    {
        DateTime NowDt = DateTime.Now;
        TimeSpan Now = DateTime.Now.TimeOfDay;
        TimeSpan Free = new DateTime(NowDt.Year, NowDt.Month, NowDt.Day, 00, 00, 00).TimeOfDay;
        TimeSpan start = timedAccessControl.Start.TimeOfDay;
        TimeSpan end = timedAccessControl.End.TimeOfDay;

        if (Free == start && Free == end) return true;//Sem restrição.

        if (start <= end)// Intervalo normal: ex. 08:00–18:00
            return Now >= timedAccessControl.Start.TimeOfDay && Now <= timedAccessControl.End.TimeOfDay;
        else// Intervalo que cruza a meia-noite: ex. 22:00–02:00
            return Now >= start || Now <= end;

    }



    // public async Task<UserToken> LoginAsync(LoginModelDto user)
    // {
    //     _GENERIC_REPO._GenericValidatorServices.IsObjNull(user);

    //     var userAccount = await FindUserAsync(user.Email);

    //     await IsValidUserAccount(userAccount.Email, userAccount.Id == -1);


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

    //     if (await HandleTwoFactorAuthenticationAsync(userAccount))
    //     {
    //         _logger.LogInformation("2FA required for user: {UserId}", userAccount.Id);
    //         return await CreateTwoFactorResponse(userAccount);
    //     }

    //     _logger.LogInformation("Successful login for user: {UserId}", userAccount.Id);

    //     return await CreateAuthenticationResponseAsync(userAccount);
    // }

    // private async Task<bool> IsValidUserAccount(string userEmail, bool result)
    // {
    //     if (result)
    //     {
    //         _logger.LogWarning("Invalid password attempt for user: {Email}", userEmail);
    //         throw new AuthServicesException(AuthErrorsMessagesException.InvalidUserNameOrPassword);
    //     }

    //     return await Task.FromResult(false);
    // }

    // private async Task ValidateAccountStatusAsync(UserAccount userAccount)
    // {
    //     if (await IsAccountLockedOutAsync(userAccount))
    //     {
    //         _logger.LogWarning("Account locked out: {UserId}", userAccount.Id);
    //         await NotifyAccountLockedAsync(userAccount);
    //         throw new AuthServicesException(AuthErrorsMessagesException.UserIsLocked);
    //     }

    //     if (!await IsEmailConfirmedAsync(userAccount))
    //     {
    //         _logger.LogWarning("Email not confirmed for user: {UserId}", userAccount.Id);
    //         throw new AuthServicesException(AuthErrorsMessagesException.EmailIsNotConfirmed);
    //     }
    // }

    // private async Task NotifyAccountLockedAsync(UserAccount userAccount)
    // {
    //     try
    //     {
    //         // await _emailService.SendAsync(To: userAccount.Email,
    //         //            Subject: "Sonny conta bloqueada",
    //         //            Body: "O número de dez tentativas de login foi esgotado e a conta foi bloqueada por atingir dez tentativas com senhas incorretas. Sugerimos troque sua senha.");
    //     }
    //     catch (Exception ex)
    //     {
    //         _logger.LogError(ex, "Failed to send account locked notification to {Email}", userAccount.Email);
    //     }
    // }
    // private async Task<IdentityResult> WriteLastLogin(string email)
    // {
    //     var userAccount = await FindUserAsync(email);

    //     _GENERIC_REPO._GenericValidatorServices.IsObjNull(userAccount);

    //     userAccount.LastLogin = DateTime.Now;

    //     var Update = await _GENERIC_REPO.UsersManager.UpdateAsync(userAccount);

    //     return Update;
    // }
    // public async Task<DateTime> GetLastLogin(string email)
    // {
    //     var userAccount = await FindUserAsync(email);

    //     _GENERIC_REPO._GenericValidatorServices.IsObjNull(userAccount);

    //     return userAccount.LastLogin;
    // }

}
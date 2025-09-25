using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities.Authentication;
using Authentication.Jwt;
using Microsoft.Extensions.Logging;
using Application.Services.Operations.Auth.Dtos;
using Application.Services.Operations.Auth;
using Application.Exceptions;
using Domain.Entities.System.Profiles;
using UnitOfWork.Persistence.Operations;
using Application.Services.Operations.Profiles.Dtos;
using Authentication.Exceptions;
using Microsoft.EntityFrameworkCore;



namespace Application.Services.Operations.Account;

public class AccountManagerServices : AuthenticationBase, IAccountManagerServices
{
    // private readonly ILogger<AccountManagerServices> _logger;
    private readonly IUnitOfWork _GENERIC_REPO;
    // private readonly IUrlHelper _url;
    private readonly IAuthServicesInjection _AUTH_SERVICES_INJECTION;

    // private readonly IServiceLaucherService _ServiceLaucherService;
    public AccountManagerServices(

          //   ILogger<AccountManagerServices> logger,
          IUnitOfWork GENERIC_REPO,
          IAuthServicesInjection AUTH_SERVICES_INJECTION

      ) : base(GENERIC_REPO, AUTH_SERVICES_INJECTION)  //   
    //    : base(jwtHandler, logger, url, GENERIC_REPO)
    {
        // _logger = logger;
        // _url = url;
        _GENERIC_REPO = GENERIC_REPO;
        _AUTH_SERVICES_INJECTION = AUTH_SERVICES_INJECTION;
        // _ServiceLaucherService = ServiceLaucherService;
    }

    public async Task<IdentityResult> UpdateUserAccountAuthAsync(UserAccountAuthUpdateDto userAccount, int id)
    {

        _GENERIC_REPO._GenericValidatorServices.Validate(userAccount.Id, id, GlobalErrorsMessagesException.EntityFromIdIsNull);

        var userAccountFromDb = await _AUTH_SERVICES_INJECTION.UsersManager.FindByEmailAsync(userAccount.Email) ?? (UserAccount)_GENERIC_REPO._GenericValidatorServices.ReplaceNullObj<UserAccount>();

        var toUpdate = userAccount.ToUpdate(userAccountFromDb);

        return await _AUTH_SERVICES_INJECTION.UsersManager.UpdateAsync(toUpdate);

    }
    public async Task<IdentityResult> UpdateUserAccountProfileAsync(UserProfileDto userAccount, int id)
    {

        _GENERIC_REPO._GenericValidatorServices.Validate(userAccount.Id, id, GlobalErrorsMessagesException.EntityFromIdIsNull);

        var userAccountFromDb = await GetUserProfileAsync(id);

        var toUpdate = userAccount.ToUpdate(userAccountFromDb);

        _GENERIC_REPO.UsersProfiles.Update(toUpdate);

        if (await _GENERIC_REPO.Save())
            return IdentityResult.Success;
        else
            return IdentityResult.Failed(new IdentityError() { Description = "Faild update profile user" });

    }
    public async Task<bool> IsAccountLockedOut(string email)
    {
        var user = await FindUserAsync(email);

        return await IsAccountLockedOutAsync(user);
    }
    public async Task<bool> IsEmailConfirmedAsync(string email)
    {
        var user = await FindUserAsync(email);

        return await IsEmailConfirmedAsync(user);
    }
    public async Task<IdentityResult> ManualConfirmEmailAddress(EmailConfirmManualDto emailConfirmManual)
    {
        var userAccount = await FindUserAsync(emailConfirmManual.Email);

        userAccount.EmailConfirmed = emailConfirmManual.EmailConfirmed;

        return await _AUTH_SERVICES_INJECTION.UsersManager.UpdateAsync(userAccount);
    }
    public async Task<IdentityResult> ManualAccountLockedOut(AccountLockedOutManualDto emailConfirmManual)
    {
        var userAccount = await FindUserAsync(emailConfirmManual.Email);

        if (emailConfirmManual.AccountLockedOut)
            userAccount.LockoutEnd = DateTime.Now.AddYears(10);
        else
            userAccount.LockoutEnd = DateTime.MinValue;
        return await _AUTH_SERVICES_INJECTION.UsersManager.UpdateAsync(userAccount);
    }
    public async Task<IdentityResult> PasswordChangeAsync(PasswordChangeDto passwordChange)
    {

        _GENERIC_REPO._GenericValidatorServices.IsObjNull(passwordChange);

        if (passwordChange.UserId <= 0)
            return IdentityResult.Failed(new IdentityError() { Description = "userId is required." });

        if (string.IsNullOrWhiteSpace(passwordChange.Password) || string.IsNullOrWhiteSpace(passwordChange.CurrentPwd))
            return IdentityResult.Failed(new IdentityError() { Description = "'current password and new password are required.'" });

        var userFromDb = await FindUserByIdAsync(passwordChange.UserId);

        _GENERIC_REPO._GenericValidatorServices.Validate(userFromDb.Id, passwordChange.UserId, GlobalErrorsMessagesException.IdIsDifferentFromEntityUpdate);

        if (userFromDb is null)
        {
            // _logger.LogWarning("User with ID {UserId} not found.", passwordChange.UserId);
            return IdentityResult.Failed(new IdentityError() { Description = "USER NOT F OUND." });
        }

        return await PasswdChangeAsync(userFromDb, passwordChange.CurrentPwd, passwordChange.Password);
    }
    public async Task<bool> IsPasswordExpiresAsync(int userId)
    {
        var user = await FindUserByIdAsync(userId);
        if (user.WillExpire.Year == DateTime.MinValue.Year)
            return false;
        else
            return true;
    }
    public async Task<IdentityResult> MarkPasswordExpireAsync(PasswordWillExpiresDto passwordWillExpires)
    {
        var userAccount = await FindUserByIdAsync(passwordWillExpires.UserId);

        _GENERIC_REPO._GenericValidatorServices.IsObjNull(userAccount);

        if (passwordWillExpires.UserId <= 0) throw new AuthServicesException(GlobalErrorsMessagesException.IdIsNull);

        return await WillExpire(userAccount, passwordWillExpires.WillExpires);

    }
    private async Task<IdentityResult> WillExpire(UserAccount userAccount, bool expires)
    {

        // var genToken = await GenerateUrlTokenPasswordReset(userAccount, "ForgotPassword", "auth");
        var genToken = await _AUTH_SERVICES_INJECTION.UsersManager.GeneratePasswordResetTokenAsync(userAccount);

        if (expires && (await _AUTH_SERVICES_INJECTION.UsersManager.ResetPasswordAsync(userAccount, genToken, "123456")).Succeeded)
        {
            userAccount.WillExpire = DateTime.Now;
            userAccount.EmailConfirmed = true;
        }
        else
            userAccount.WillExpire = DateTime.MinValue;

        return await _AUTH_SERVICES_INJECTION.UsersManager.UpdateAsync(userAccount);


    }
    public async Task<IdentityResult> StaticPasswordDefined(ResetStaticPasswordDto reset)
    {
        var userAccount = await FindUserAsync(reset.Email);

        var genToken = await _AUTH_SERVICES_INJECTION.UsersManager.GeneratePasswordResetTokenAsync(userAccount);

        userAccount.WillExpire = DateTime.MinValue;
        userAccount.LockoutEnd = DateTimeOffset.MinValue;

        if ((await _AUTH_SERVICES_INJECTION.UsersManager.UpdateAsync(userAccount)).Succeeded)
            return await _AUTH_SERVICES_INJECTION.UsersManager.ResetPasswordAsync(userAccount, genToken, reset.Password);

        return IdentityResult.Failed(new IdentityError() { Description = "Fail when trying to change password." });


        // return await _AUTH_SERVICES_INJECTION.UsersManager.ResetPasswordAsync(userAccount, genToken, reset.Password);
    }
    public async Task<IdentityResult> TimedAccessControlStartEndUpdateAsync(TimedAccessControlStartEndPostDto timedAccessControl)
    {
        var userAccount = await GetUserIncluded(timedAccessControl.UserId);

        _GENERIC_REPO._GenericValidatorServices.IsObjNull(timedAccessControl);

        var id = userAccount?.TimedAccessControl?.Id;

        if (userAccount?.TimedAccessControl?.Id > 0)
        {
            _AUTH_SERVICES_INJECTION.TimedAccessControls.Update(timedAccessControl.ToUpdate(id ?? throw new AuthServicesException(GlobalErrorsMessagesException.IdIsNull)));

            if (await _GENERIC_REPO.SaveID())
                return IdentityResult.Success;
        }
        else
            userAccount.TimedAccessControl = timedAccessControl.ToPost();

        return await _AUTH_SERVICES_INJECTION.UsersManager.UpdateAsync(userAccount);
    }
    public async Task<TimedAccessControlDto> GetTimedAccessControlAsync(int userId)
    {

        var _now = DateTime.Now;
        var start = new DateTime(_now.Year, _now.Month, _now.Day, 00, 00, 00);
        var end = new DateTime(_now.Year, _now.Month, _now.Day, 00, 00, 00);

        var times = await GetUserIncluded(userId);

        _GENERIC_REPO._GenericValidatorServices.IsObjNull(times);

        return times.TimedAccessControl.ToDto() ?? new TimedAccessControlDto() { Start = start, End = end };
    }
    // public async Task<IdentityResult> ToggleTwoFactorAsync(ToggleTwoFactorDto toggleTwoFactor)
    // {
    //     var userAccount = await FindUserByIdAsync(toggleTwoFactor.UserId);

    //     _GENERIC_REPO._GenericValidatorServices.IsObjNull(userAccount);

    //     return await _AUTH_SERVICES_INJECTION.UsersManager.SetTwoFactorEnabledAsync(userAccount, toggleTwoFactor.Enable);
    // }
    // public async Task<bool> TwoFactorVerifyAsync(string email, string token) => await VerifyTwoFactorTokenAsync(email, token);
    private async Task<UserAccount> GetUserIncluded(int userId)
    {
        return await _AUTH_SERVICES_INJECTION.UsersAccounts.GetByPredicate(x =>
                       x.Id == userId && x.Deleted.Year == DateTime.MinValue.Year,
                       add => add.Include(x => x.TimedAccessControl),
                       selector => selector,
                       null);
    }
    private async Task<UserProfile> GetUserProfileAsync(int id)
    {
        return await _GENERIC_REPO.UsersProfiles.GetByPredicate(
                   x => x.Id == id,
                   null,
                   selector => selector,
                   null
                   ) ?? (UserProfile)_GENERIC_REPO._GenericValidatorServices.ReplaceNullObj<UserProfile>();
    }



    // private async Task<bool> HandleTwoFactorAuthenticationAsync(UserAccount userAccount)
    // {
    //     if (!await _AUTH_SERVICES_INJECTION.UsersManager.GetTwoFactorEnabledAsync(userAccount))
    //         return false;

    //     var validProviders = await _AUTH_SERVICES_INJECTION.UsersManager.GetValidTwoFactorProvidersAsync(userAccount);

    //     if (!validProviders.Contains("Email"))
    //         return false;

    //     // var token = await _AUTH_SERVICES_INJECTION.UsersManager.GenerateTwoFactorTokenAsync(userAccount, "Email");
    //     // var token = await GenerateTwoFactorTokenAsync(userAccount, "twofactorverify", "auth");
    //     var token = await _AUTH_SERVICES_INJECTION.UsersManager.GenerateTwoFactorTokenAsync(userAccount, "Email");

    //     string linkToken = $"http://localhost:4200/two-factor-check/{token}/{userAccount.Email}";

    //     await SendTwoFactorTokenAsync(userAccount, linkToken);

    //     await _ServiceLaucherService.HttpContextAccessors?.HttpContext?.SignInAsync(IdentityConstants.TwoFactorUserIdScheme, Store2FA(userAccount.Id, "Email"));

    //     return true;
    // }

    // private ClaimsPrincipal Store2FA(int id, string provider)
    // {
    //     var identity = new ClaimsIdentity(new List<Claim>
    //     {
    //         new Claim("sub", id.ToString()),
    //         new Claim("amr", provider)

    //     }, IdentityConstants.TwoFactorUserIdScheme);


    //     return new ClaimsPrincipal(identity);
    // }

    // private protected async Task<bool> IsEnabledTwoFactorAsync(UserAccount userAccount) => await _AUTH_SERVICES_INJECTION.UsersManager.GetTwoFactorEnabledAsync(userAccount);

    // private protected async Task<bool> VerifyTwoFactorTokenAsync(string email, string token)
    // {

    //     var result = await _ServiceLaucherService.HttpContextAccessors?.HttpContext.AuthenticateAsync(IdentityConstants.TwoFactorUserIdScheme);

    //     if (!result.Succeeded) return false;

    //     var userAccount = await _AUTH_SERVICES_INJECTION.UsersManager.FindByIdAsync(result.Principal.FindFirstValue("sub"));


    //     _GENERIC_REPO._GenericValidatorServices.IsObjNull<UserAccount>(userAccount);

    //     var isValid = await _AUTH_SERVICES_INJECTION.UsersManager.VerifyTwoFactorTokenAsync(userAccount, result.Principal.FindFirstValue("amr"), token);

    //     if (!isValid) return false;

    //     await _ServiceLaucherService.HttpContextAccessors?.HttpContext.SignOutAsync(IdentityConstants.TwoFactorUserIdScheme);

    //     var claimsPrincipal = await _ServiceLaucherService.UserClaimsPrincipalFactory.CreateAsync(userAccount);

    //     await _ServiceLaucherService.HttpContextAccessors?.HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, claimsPrincipal);

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



    // private async Task SendTwoFactorTokenAsync(UserAccount userAccount, string token)
    // {
    //     try
    //     {
    //         await SendAsync(To: userAccount.Email,
    //                 Subject: "I.M: Autenticação de dois fatores",
    //                 Body: $"Código: Autenticação de dois fatores: {token}");
    //         // Body: $"Código: Autenticação de dois fatores: {"http://localhost:4200/two-factor-check"}{token.Replace("api/auth/twofactorverify", "")}");
    //     }
    //     catch (Exception ex)
    //     {
    // _logger.LogError(ex, "Failed to send 2FA token to {Email}", userAccount.Email);
    //         throw new AuthServicesException(AuthErrorsMessagesException.TwoFactorTokenSendFailed);
    //     }
    // }





}


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


namespace Application.Services.Operations.Account;

public class AccountManagerServices : AuthenticationBase, IAccountManagerServices
{
    private readonly ILogger<AccountManagerServices> _logger;
    private readonly IUnitOfWork _GENERIC_REPO;
    private readonly IUrlHelper _url;
    public AccountManagerServices(
          JwtHandler jwtHandler,
          IUrlHelper url,
          ILogger<AccountManagerServices> logger,
          IUnitOfWork GENERIC_REPO
      ) : base(jwtHandler, logger, url, GENERIC_REPO)
    {
        _logger = logger;
        _url = url;
        _GENERIC_REPO = GENERIC_REPO;
    }

    public async Task<IdentityResult> UpdateUserAccountAuthAsync(UserAccountAuthUpdateDto userAccount, int id)
    {

        _GENERIC_REPO._GenericValidatorServices.Validate(userAccount.Id, id, GlobalErrorsMessagesException.EntityFromIdIsNull);

        var userAccountFromDb = await _GENERIC_REPO.UsersManager.FindByEmailAsync(userAccount.Email) ?? (UserAccount)_GENERIC_REPO._GenericValidatorServices.ReplaceNullObj<UserAccount>();

        var toUpdate = userAccount.ToUpdate(userAccountFromDb);

        return await _GENERIC_REPO.UsersManager.UpdateAsync(toUpdate);

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

        return await _GENERIC_REPO.UsersManager.UpdateAsync(userAccount);
    }
    public async Task<IdentityResult> ManualAccountLockedOut(AccountLockedOutManualDto emailConfirmManual)
    {
        var userAccount = await FindUserAsync(emailConfirmManual.Email);

        if (emailConfirmManual.AccountLockedOut)
            userAccount.LockoutEnd = DateTime.Now.AddYears(10);
        else
            userAccount.LockoutEnd = DateTime.MinValue;
        return await _GENERIC_REPO.UsersManager.UpdateAsync(userAccount);
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
            _logger.LogWarning("User with ID {UserId} not found.", passwordChange.UserId);
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
        var genToken = await _GENERIC_REPO.UsersManager.GeneratePasswordResetTokenAsync(userAccount);

        if (expires && (await _GENERIC_REPO.UsersManager.ResetPasswordAsync(userAccount, genToken, "123456")).Succeeded)
        {
            userAccount.WillExpire = DateTime.Now;
            userAccount.EmailConfirmed = true;
        }
        else
            userAccount.WillExpire = DateTime.MinValue;

        return await _GENERIC_REPO.UsersManager.UpdateAsync(userAccount);


    }
    public async Task<IdentityResult> StaticPasswordDefined(ResetStaticPasswordDto reset)
    {
        var userAccount = await FindUserAsync(reset.Email);

        var genToken = await _GENERIC_REPO.UsersManager.GeneratePasswordResetTokenAsync(userAccount);

        userAccount.WillExpire = DateTime.MinValue;
        userAccount.LockoutEnd = DateTimeOffset.MinValue;

        if ((await _GENERIC_REPO.UsersManager.UpdateAsync(userAccount)).Succeeded)
            return await _GENERIC_REPO.UsersManager.ResetPasswordAsync(userAccount, genToken, reset.Password);

        return IdentityResult.Failed(new IdentityError() { Description = "Fail when trying to change password." });


        // return await _GENERIC_REPO.UsersManager.ResetPasswordAsync(userAccount, genToken, reset.Password);
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



}


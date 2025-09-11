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
        {
            userAccount.LockoutEnd = DateTimeNow;
        }
        else
            userAccount.LockoutEnd = DateTimeOffset.MinValue;

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
            return IdentityResult.Failed(new IdentityError() { Description = "USER NOT FOUND." });
        }

        return await PasswdChangeAsync(userFromDb, passwordChange.CurrentPwd, passwordChange.Password);
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


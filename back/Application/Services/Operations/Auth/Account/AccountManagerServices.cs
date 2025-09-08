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
    // public async Task<IdentityResult> UpdateUserAccountProfileAsync(UserAccountProfileUpdateDto userAccount, int id)
    // {

    //     _GENERIC_REPO._GenericValidatorServices.Validate(userAccount.Id, id, GlobalErrorsMessagesException.EntityFromIdIsNull);

    //     var userAccountFromDb = await GetUserProfileAsync(id);

    //     var toUpdate = userAccount.ToUpdate(userAccountFromDb);

    //     _GENERIC_REPO.UsersProfiles.Update(toUpdate);

    //     if (await _GENERIC_REPO.Save())
    //         return IdentityResult.Success;
    //     else
    //         return IdentityResult.Failed(new IdentityError() { Description = "Faild update profile user" });

    // }

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


using Application.Auth.UsersAccountsServices.Dtos;
using Application.Exceptions;
using Application.Helpers.Inject;
using Application.UsersAccountsServices.Dtos.Mappers;
using Domain.Entities.System.Profiles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UnitOfWork.Persistence.Operations;

namespace Application.Auth.UsersAccountsServices.Profile;

public class UserAccountProfileServices : UserAccountBase, IUserAccountProfileServices
{
    private readonly IUnitOfWork _genericRepo;
    
    private readonly IValidatorsInject _validatorsInject;
    public UserAccountProfileServices(
                        IUnitOfWork genericRepo,
                        IValidatorsInject validatorsInject
                        ) 
    {
        _genericRepo = genericRepo;
        _validatorsInject = validatorsInject;
    }
    public async Task<UserProfile> GetUserProfileByProfileIdAsync(string userProfileId)
    {
        return await _genericRepo.UsersProfiles.GetByPredicate(
         x => x.UserAccountId == userProfileId,
         add => add.Include(x => x.Address)
         .Include(x => x.Contact),
         selector => selector,
         null
         );
    }

    public async Task<IdentityResult> UpdateUserAccountProfileAsync(UserProfileDto userAccount, int id)
    {

        _validatorsInject.GenericValidators.Validate(userAccount.Id, id, GlobalErrorsMessagesException.EntityFromIdIsNull);

        var userAccountFromDb = await GetUserProfileAsync(id);

        var toUpdate = userAccount.ToUpdate(userAccountFromDb);

        _genericRepo.UsersProfiles.Update(toUpdate);

        if (await _genericRepo.Save())
            return IdentityResult.Success;
        else
            return IdentityResult.Failed(new IdentityError() { Description = "Faild update profile user" });

    }
    private async Task<UserProfile> GetUserProfileAsync(int id)
    {
        return await _genericRepo.UsersProfiles.GetByPredicate(
                   x => x.Id == id,
                   null,
                   selector => selector,
                   null
                   );
    }

}

public class UserAccountBase
{
}
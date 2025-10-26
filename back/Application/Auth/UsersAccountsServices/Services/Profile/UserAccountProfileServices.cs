using Application.Auth.UsersAccountsServices.Dtos;
using Application.Auth.UsersAccountsServices.Exceptions;
using Application.Auth.UsersAccountsServices.Extends;
using Application.Exceptions;
using Application.Helpers.Inject;
using Application.Shared.Dtos;
using Application.UsersAccountsServices.Dtos.Mappers;
using Domain.Entities.Authentication.extends;
using Domain.Entities.System.Profiles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UnitOfWork.Persistence.Operations;

namespace Application.Auth.UsersAccountsServices.Profile;

public class UserAccountProfileServices : UserAccountServicesBase, IUserAccountProfileServices
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

    public async Task<ApiResponse<bool>> UpdateUserAccountProfileAsync(UserProfileDto userAccount, int id)
    {
        int idValidated = ValidateUserId(id);

        _validatorsInject.GenericValidators.Validate(userAccount.Id, id, GlobalErrorsMessagesException.EntityFromIdIsNull);

        var userAccountFromDb = await GetUserProfileAsync(idValidated);

        var toUpdate = userAccount.ToUpdate(userAccountFromDb);

        _genericRepo.UsersProfiles.Update(toUpdate);

        return await ResponseUpdateUserAccountProfileAsync(userAccount.Id);
    }
   
    private async Task<ApiResponse<bool>> ResponseUpdateUserAccountProfileAsync(int id)
    {
        if (await _genericRepo.Save())
            return ApiResponse<bool>.Response([""], true, $@"{"User Account successfully updated. UserId: "} - {id}", true);
        else
            return ApiResponse<bool>.Response([$@"{"Faild update profile user. UserId: "} - {id}"], true, "ResponseUpdateUserAccountProfileAsync", true);
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

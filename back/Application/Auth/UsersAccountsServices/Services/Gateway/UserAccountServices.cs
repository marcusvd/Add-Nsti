using Domain.Entities.Authentication;

using Domain.Entities.System.Profiles;
using Application.Auth.UsersAccountsServices.Dtos;
using Application.UsersAccountsServices.Dtos;
using Application.Auth.UsersAccountsServices.Profile;
using Application.Auth.UsersAccountsServices.Extends;
using Application.Auth.UsersAccountsServices.Services.Auth;


namespace Application.Auth.UsersAccountsServices.Services.Gateway;

public class UserAccountServices : IUserAccountServicesBase, IUserAccountServices
{

    private readonly IUserAccountAuthServices _userAccountAuthServices;
    private readonly IUserAccountProfileServices _userAccountProfileServices;

    public UserAccountServices(
          IUserAccountAuthServices userAccountAuthServices,
          IUserAccountProfileServices userAccountProfileServices
        )
    {
        _userAccountAuthServices = userAccountAuthServices;
        _userAccountProfileServices = userAccountProfileServices;
    }

    public async Task<UserAuthProfileDto> GetUserByIdFullAsync(int id)
    {

        var userAccount = await _userAccountAuthServices.GetUserAccountByUserIdAsync(id);
        var userprofile = await _userAccountProfileServices.GetUserProfileByProfileIdAsync(userAccount.UserProfileId)  ?? new UserProfileDto(){UserAccountId = "Incompleto"} ;

        return MakerUserAccountProfile(userAccount, userprofile);
    }
    private UserAuthProfileDto MakerUserAccountProfile(UserAccount userAccountAuth, UserProfile userProfile)
    {
        return new UserAuthProfileDto()
        {
            UserAccountAuth = (UserAccountDto)userAccountAuth,
            UserAccountProfile = (UserProfileDto)userProfile
        };
    }

    public async Task<List<UserAccountDto>> GetUsersByCompanyIdAsync(int companyAuthId)
    {
        var companyUserAccount = await _userAccountAuthServices.GetCompanyUserAccountByCompanyId(companyAuthId);

        var userAccounts = companyUserAccount.Select(x => x.UserAccount).ToList();

        return userAccounts.Select(x => (UserAccountDto)x).ToList() ?? [];
    }





}

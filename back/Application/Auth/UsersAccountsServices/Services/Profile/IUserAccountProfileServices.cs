using Domain.Entities.System.Profiles;
using Microsoft.EntityFrameworkCore;

namespace Application.Auth.UsersAccountsServices.Profile;

public interface IUserAccountProfileServices
{
    Task<UserProfile> GetUserProfileByProfileIdAsync(string userProfileId);
}

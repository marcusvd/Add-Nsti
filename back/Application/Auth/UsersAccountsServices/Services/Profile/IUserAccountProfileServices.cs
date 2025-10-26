using Application.Auth.UsersAccountsServices.Dtos;
using Application.Shared.Dtos;
using Domain.Entities.System.Profiles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Auth.UsersAccountsServices.Profile;

public interface IUserAccountProfileServices
{
    Task<UserProfile> GetUserProfileByProfileIdAsync(string userProfileId);
    Task<ApiResponse<bool>> UpdateUserAccountProfileAsync(UserProfileDto userAccount, int id);
}

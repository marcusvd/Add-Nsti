using Application.Auth.Dtos;
using Application.Shared.Dtos;
using Application.UsersAccountsServices.Dtos;
using Domain.Entities.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Application.Auth.UsersAccountsServices.Services.Auth;

public interface IUserAccountAuthServices
{
    Task<List<CompanyUserAccount>> GetCompanyUserAccountByCompanyId(int companyAuthId);
    Task<UserAccount> GetUserIncluded(int userId);
    Task<UserAccount> GetUserAsync(string userNameOrEmail);
    Task<UserAccount> GetUserAccountByEmailAsync(string email);
    Task<UserAccount> GetUserAccountByUserIdAsync(int id);
    Task<ApiResponse<bool>> IsUserExistCheckByEmailAsync(string emailParam);
    Task<bool> IsAccountLockedOut(string email);
    Task<ApiResponse<IdentityResult>> ManualAccountLockedOut(AccountLockedOutManualDto emailConfirmManual);
    // Task ValidateUserAccountAsync(UserAccount userAccount);
    Task<ApiResponse<bool>> UpdateUserAccountAuthAsync(UserAccountDto userAccount, int id);

}

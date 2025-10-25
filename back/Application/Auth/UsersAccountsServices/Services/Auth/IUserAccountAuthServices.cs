using Domain.Entities.Authentication;

namespace Application.Auth.UsersAccountsServices;

public interface IUserAccountAuthServices
{
    // Task<UserAccount> GetUserAccountByIdAsync(int id);
    Task<List<CompanyUserAccount>> GetCompanyUserAccountByCompanyId(int companyAuthId);
    Task<UserAccount> GetUserIncluded(int userId);
    Task<UserAccount> GetUserAsync(string userNameOrEmail);
    Task<UserAccount> GetUserAccountByEmailAsync(string email);
    Task<UserAccount> GetUserAccountByUserIdAsync(int id);
    // Task<ApiResponse<IdentityResult>> IsUserExistCheckByEmailAsync(string emailParam);
    Task ValidateUserAccountAsync(UserAccount userAccount);

}

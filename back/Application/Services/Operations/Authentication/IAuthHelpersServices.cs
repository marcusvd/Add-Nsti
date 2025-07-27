using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Application.Services.Operations.Authentication.Dtos;
using System.Security.Claims;
using Authentication;


namespace Application.Services.Operations.Authentication
{
    public interface IAuthHelpersServices
    {
        void ObjIsNull(object obj);
        Task<bool> NameIsDuplicate(string userName);
        Task<bool> EmailIsDuplicate(string email);
        Task<bool> IsLockedOutAsync(UserAccount userAccount);
        Task<bool> EmailIsNotConfirmedAsync(UserAccount userAccount);
        void EmailAlreadyConfirmed(UserAccount userAccount);
        Task<bool> CheckPasswordAsync(UserAccount userAccount, string user);
        Task<bool> GetTwoFactorEnabledAsync(UserAccount userAccount);
        Task<IList<string>> GetValidTwoFactorProvidersAsync(UserAccount userAccount);
        Task<string> GenerateTwoFactorTokenAsync(UserAccount userAccount, string provider);
        Task<List<UserAccount>> FindAllUsersAsync();
        Task<UserAccount> FindUserByEmailAsync(string email);
        Task<UserAccount> FindUserByNameAsync(string name);
        Task<UserAccount> FindUserByNameAllIncludedAsync(string name);
        Task<UserAccount> FindUserByIdAsync(int id);
        Task<UserAccount> FindUserByNameOrEmailAsync(string userNameOrEmail);
        Task<bool> VerifyTwoFactorTokenAsync(UserAccount userAccount, string email, T2FactorDto t2Factor);
        Task<bool> RegisterUserAsync(UserAccount user, string password);
        UserAccount User(string userName, string email, string companyName);
        UserAccountDto UserAccountToUserAccountDto(UserAccount user);
        Task<IdentityResult> UserUpdateAsync(UserAccount user);
        Task<string> UrlEmailConfirm(UserAccount userAccount, string controller, string action);
        Task<bool> ConfirmingEmail(UserAccount userAccount, ConfirmEmailDto confirmEmail);
        Task<bool> ResetPasswordAsync(ResetPasswordDto resetPassword);
        Task<string> UrlPasswordReset(UserAccount userAccount, string controller, string action);
        Task<string> GeneratePasswordResetTokenAsync(UserAccount userAccount);
        //ROLES
      //  Task<IdentityResult> CreateRole(RoleDto role);
        Task<string> UpdateUserRoles(UpdateUserRoleDto model);
        Task<IList<string>> GetRoles(UserAccount user);

        //ClAIMS
        Task<List<Claim>> GetClaims(UserAccountDto user, Task<IList<string>> roles);

    }
}
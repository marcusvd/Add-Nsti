
using Authentication.Entities;
using Authentication.Operations.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Operations.Account;

public interface IAccountManagerServices
{
    Task<bool> IsUserExistCheckByEmailAsync(string email);
    Task<bool> ConfirmEmailAddressAsync(ConfirmEmail confirmEmail);
    Task<bool> ForgotPasswordAsync(ForgotPassword forgotPassword);
    Task<bool> ResetPasswordAsync(ResetPassword resetPassword);
    Task<string> UpdateUserRoles(UpdateUserRole role);
    Task<IList<string>> GetRoles(UserAccount userAccount);
    Task<IdentityResult> CreateRole(RoleDto roleDto);
}

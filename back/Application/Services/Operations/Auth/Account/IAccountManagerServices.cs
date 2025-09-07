
using Domain.Entities.Authentication;
using Application.Services.Operations.Auth.Dtos;
using Microsoft.AspNetCore.Identity;
using Application.Services.Operations.Auth.Account.dtos;

namespace Application.Services.Operations.Account;

public interface IAccountManagerServices
{
    Task<IdentityResult> IsUserExistCheckByEmailAsync(string email);
    Task<IdentityResult> ConfirmEmailAddressAsync(ConfirmEmail confirmEmail);
    Task<IdentityResult> ForgotPasswordAsync(ForgotPassword forgotPassword);
    Task<IdentityResult> ResetPasswordAsync(ResetPassword resetPassword);
    Task<IdentityResult> UpdateUserAccountAuthAsync(UserAccountAuthUpdateDto userAccount, int id);
    Task<IdentityResult> UpdateUserAccountProfileAsync(UserAccountProfileUpdateDto userAccount, int id);
    Task<IdentityResult> RequestEmailChangeAsync(RequestEmailChangeDto requestEmailChangeDto);
    Task<IdentityResult> ConfirmYourEmailChangeAsync(ConfirmEmailChangeDto confirmRequestEmailChange);
    Task<string> UpdateUserRoles(UpdateUserRole role);
    Task<IList<string>> GetRolesAsync(UserAccount userAccount);
    Task<IdentityResult> CreateRoleAsync(RoleDto roleDto);
}

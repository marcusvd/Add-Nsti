
using Domain.Entities.Authentication;
using Application.Services.Operations.Auth.Dtos;
using Microsoft.AspNetCore.Identity;
using Application.Services.Operations.Auth.Account.dtos;
using Application.Services.Operations.Profiles.Dtos;

namespace Application.Services.Operations.Account;

public interface IAccountManagerServices
{
    Task<IdentityResult> IsUserExistCheckByEmailAsync(string email);
    Task<IdentityResult> ConfirmEmailAddressAsync(ConfirmEmailDto confirmEmail);
    Task<IdentityResult> ForgotPasswordAsync(ForgotPasswordDto forgotPassword);
    Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto resetPassword);
    Task<IdentityResult> PasswordChangeAsync(PasswordChangeDto passwordChange);
    Task<bool> IsAccountLockedOut(string email);
    Task<bool> IsEmailConfirmedAsync(string email);
    Task<IdentityResult> ManualConfirmEmailAddress(EmailConfirmManualDto EmailConfirmManual);
    Task<IdentityResult> ManualAccountLockedOut(AccountLockedOutManualDto emailConfirmManual);
    Task<IdentityResult> UpdateUserAccountAuthAsync(UserAccountAuthUpdateDto userAccount, int id);
    Task<IdentityResult> UpdateUserAccountProfileAsync(UserProfileDto userAccount, int id);
    Task<IdentityResult> RequestEmailChangeAsync(RequestEmailChangeDto requestEmailChangeDto);
    Task<IdentityResult> ConfirmYourEmailChangeAsync(ConfirmEmailChangeDto confirmRequestEmailChange);
    Task<string> UpdateUserRoles(UpdateUserRoleDto role);
    Task<IList<string>> GetRolesAsync(UserAccount userAccount);
    Task<IdentityResult> CreateRoleAsync(RoleDto roleDto);
}

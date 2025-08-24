
using Domain.Entities.Authentication;
using Application.Services.Operations.Auth.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Operations.Account;

public interface IAccountManagerServices
{
    Task<bool> IsUserExistCheckByEmailAsync(string email);
    Task<bool> ConfirmEmailAddressAsync(ConfirmEmail confirmEmail);
    Task<bool> ForgotPasswordAsync(ForgotPassword forgotPassword);
    Task<bool> ResetPasswordAsync(ResetPassword resetPassword);
    Task<string> UpdateUserRoles(UpdateUserRole role);
    Task<IList<string>> GetRolesAsync(UserAccount userAccount);
    Task<IdentityResult> CreateRoleAsync(RoleDto roleDto);
}

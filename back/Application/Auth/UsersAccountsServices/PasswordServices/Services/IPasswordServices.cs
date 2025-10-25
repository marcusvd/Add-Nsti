using AApplication.Auth.UsersAccountsServices.PasswordServices.Dtos;
using Application.Auth.Dtos;
using Application.Shared.Dtos;
using Domain.Entities.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Application.Auth.UsersAccountsServices.PasswordServices.Services;

public interface IPasswordServices
{
    Task<ApiResponse<string>> ForgotPasswordAsync(ForgotPasswordDto dto);
    Task<ApiResponse<IdentityResult>> ResetPasswordAsync(ResetPasswordDto resetPassword);
    Task<ApiResponse<IdentityResult>> PasswordSignInAsync(UserAccount userAccount, string password, bool isPersistent = true, bool lockoutOnFailure = true);
    Task<ApiResponse<IdentityResult>> CheckPasswordAsync(UserAccount userAccount, string password);
    Task<ApiResponse<IdentityResult>> PasswordChangeAsync(PasswordChangeDto passwordChange);
    Task<ApiResponse<bool>> IsPasswordExpiresAsync(int userId);
    Task<ApiResponse<IdentityResult>> MarkPasswordExpireAsync(PasswordWillExpiresDto passwordWillExpires);
    Task<ApiResponse<IdentityResult>> ManualAccountLockedOut(AccountLockedOutManualDto emailConfirmManual);
    Task<ApiResponse<IdentityResult>> SetStaticPassword(ResetStaticPasswordDto reset);
}

using Application.Auth.UsersAccountsServices.EmailUsrAccountServices.dtos;
using Application.Shared.Dtos;
using Domain.Entities.Authentication;
using Microsoft.AspNetCore.Identity;


namespace Application.Auth.UsersAccountsServices.EmailUsrAccountServices.Services;

public interface IEmailUserAccountServices
{
    Task<ApiResponse<IdentityResult>> ConfirmEmailAddressAsync(ConfirmEmailDto dto);
    Task<ApiResponse<IdentityResult>> ManualConfirmEmailAddress(EmailConfirmManualDto dto);
    Task<ApiResponse<string>> RequestEmailChangeAsync(RequestEmailChangeDto dto);
    Task<ApiResponse<IdentityResult>> ConfirmYourEmailChangeAsync(ConfirmEmailChangeDto dto);
    // Task<ApiResponse<string>> ResendConfirmEmailAsync(string emailParam);
    Task<ApiResponse<string>> SendConfirmEmailAsync(bool registerResult, UserAccount userAccount);

    Task<ApiResponse<UserToken>> FirstEmailConfirmationAsync(UserToken userToken);

    Task<ApiResponse<UserToken>> FirstEmailConfirmationCheckTokenAsync(ConfirmEmailDto dto);
    Task<bool> IsEmailConfirmedAsync(string email);
    Task ValidateUserAccountAsync(UserAccount userAccount);
   
}
using Application.Auth.TwoFactorAuthentication.Dtos;
using Application.Shared.Dtos;
using Domain.Entities.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Application.Auth.TwoFactorAuthentication;

public interface ITwoFactorAuthenticationServices
{
    Task<ApiResponse<UserToken>> TwoFactorVerifyAsync(VerifyTwoFactorRequestDto request);
    Task<ApiResponse<EnableAuthenticatorResponseDto>> EnableAuthenticatorAsync(ToggleAuthenticatorRequestDto request);
    Task<ApiResponse<TwoFactorStatusDto>> GetTwoFactorStatusAsync(int userId);
    Task<ApiResponse<AuthenticatorSetupResponseDto>> GetAuthenticatorSetup();
    Task<IdentityResult> OnOff2FaCodeViaEmailAsync(OnOff2FaCodeViaEmailDto request);
    Task<bool> HandleTwoFactorAuthenticationAsync(UserAccount userAccount);
}
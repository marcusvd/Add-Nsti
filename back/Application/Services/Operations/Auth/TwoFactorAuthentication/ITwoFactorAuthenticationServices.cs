using Application.Services.Operations.Auth.Dtos;
using Domain.Entities.Authentication;
using Microsoft.AspNetCore.Identity;


namespace Authentication.Operations.TwoFactorAuthentication;

public interface ITwoFactorAuthenticationServices
{
    Task<TwoFactorStatusViewModel> GetTwoFactorStatus(int userId);
    Task<IdentityResult> TwoFactorToggleAsync(TwoFactorToggleViewModel toggleTwoFactor);
    Task<bool> HandleTwoFactorAuthenticationAsync(UserAccount userAccount);
    

}
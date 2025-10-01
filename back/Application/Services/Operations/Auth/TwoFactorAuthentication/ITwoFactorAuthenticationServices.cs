using Application.Services.Operations.Auth.Account.dtos;
using Application.Services.Operations.Auth.Dtos;
using Domain.Entities.Authentication;
using Microsoft.AspNetCore.Identity;


namespace Authentication.Operations.TwoFactorAuthentication;

public interface ITwoFactorAuthenticationServices
{
    Task<TwoFactorStatusViewModel> GetTwoFactorStatus(int userId);
    Task<IdentityResult> OnOff2FaCodeViaEmailAsync(OnOff2FaCodeViaEmailViewModel request);
    Task<bool> HandleTwoFactorAuthenticationAsync(UserAccount userAccount);
    

}
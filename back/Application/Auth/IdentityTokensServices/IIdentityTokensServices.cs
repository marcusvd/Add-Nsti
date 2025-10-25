
using Domain.Entities.Authentication;

namespace Application.Auth.IdentityTokensServices;

public interface IIdentityTokensServices
{
    Task<string> GenerateUrlTokenEmailConfirmation(UserAccount userAccount, string action, string controller);
    Task<string> GenerateUrlTokenEmailChange(UserAccount userAccount, string action, string controller, string newEmail);
    Task<string> GenerateUrlTokenPasswordReset(UserAccount userAccount, string action, string controller);
    Task<string> GenerateTwoFactorTokenAsync(UserAccount userAccount);
}
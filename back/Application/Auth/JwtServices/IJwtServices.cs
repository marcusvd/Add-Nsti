using System.Security.Claims;
using Domain.Entities.Authentication;
using Microsoft.Extensions.Configuration;

namespace Application.Auth.JwtServices;

public interface IJwtServices
{
    Task<UserToken> FirstRegisterEmailValidation(string email);
    Task<UserToken> GenerateUserToken(List<Claim> claims, UserAccount user, IList<string> roles, string time);
    Task<UserToken> CreateAuthenticationResponseAsync(UserAccount userAccount);
    Task<UserToken> CreateTwoFactorResponse(UserAccount userAccount);
    ClaimsPrincipal ValidateJwtToken(string token);
    UserToken InvalidUserToken();
}
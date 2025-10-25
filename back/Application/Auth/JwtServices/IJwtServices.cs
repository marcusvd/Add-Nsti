using System.Security.Claims;
using Domain.Entities.Authentication;

namespace Application.Auth.JwtServices;

public interface IJwtServices
{
    Task<UserToken> GenerateUserToken(List<Claim> claims, UserAccount user, IList<string> roles, string time);
    Task<UserToken> CreateAuthenticationResponseAsync(UserAccount userAccount);
    Task<UserToken> CreateTwoFactorResponse(UserAccount userAccount);
    UserToken InvalidUserToken();
}

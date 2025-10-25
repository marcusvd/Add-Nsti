using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Domain.Entities.Authentication;
using Microsoft.AspNetCore.Identity;
using Application.Auth.Roles.Services;

namespace Application.Auth.JwtServices;

public class JwtServices : IJwtServices
{
    private readonly IConfiguration _configuration;
    private readonly IConfigurationSection _jwtSettings;
    private readonly IRolesServices _rolesServices;

    public JwtServices(
        IConfiguration configuration,
        IRolesServices rolesServices
    )
    {
        _configuration = configuration;
        _jwtSettings = _configuration.GetSection("JwtSettings");
        _rolesServices = rolesServices;
    }

    public async Task<UserToken> GenerateUserToken(List<Claim> claims, UserAccount user, IList<string> roles, string time)
    {

        DateTime loginExpiresHours = DateTime.Now.AddHours(Double.Parse(_jwtSettings["loginExpiresHours"]!));
        DateTime _2faExpiresMinutes = DateTime.Now.AddMinutes(Double.Parse(_jwtSettings["_2faExpiresMinutes"]!));

        DateTime timeToExpire = time.ToLower() == "2fa" ? _2faExpiresMinutes : loginExpiresHours;

        var tokenOptions = new JwtSecurityToken(
            claims: claims,
            issuer: _jwtSettings["validIssuer"],
            audience: _jwtSettings["validAudience"],
            expires: timeToExpire,
            signingCredentials: GetSigningCredentials()
        );
        var result = await Task.FromResult(userToken(user, roles, tokenOptions, timeToExpire));
        return result;
    }

    private SigningCredentials GetSigningCredentials()
    {

        var key = Encoding.UTF8.GetBytes(_jwtSettings["secretKey"]!);

        var secret = new SymmetricSecurityKey(key);

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }
    private UserToken userToken(UserAccount user, IList<string> roles, JwtSecurityToken tokenOptions, DateTime expiresDateTime)
    {
        return new UserToken()
        {
            Authenticated = true,
            Expiration = expiresDateTime,
            Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions),
            UserName = user.UserName!,
            Email = user.Email,
            Id = user.Id,
            BusinessId = user.BusinessAuthId,
            Roles = roles,
            Action = ""
        };

    }

    public UserToken InvalidUserToken() => new UserToken()
    {
        Authenticated = false,
        Expiration = DateTime.MinValue,
        Token = "Invalid token",

    };

    public async Task<UserToken> CreateAuthenticationResponseAsync(UserAccount userAccount)
    {
        var claimsList = await BuildUserClaims(userAccount);
        var roles = await _rolesServices.GetRolesAsync(userAccount);
        var token = await GenerateUserToken(claimsList.Claims.ToList(), userAccount, roles, "login");
        return token;
    }

    public async Task<UserToken> CreateTwoFactorResponse(UserAccount userAccount)
    {
        var claimsList = await BuildUserClaims(userAccount);
        var token = await GenerateUserToken(claimsList.Claims.ToList(), userAccount, new List<string>() { "PENDING_AUTH_2FA" }, "2fa");
        token.Action = "TwoFactor";
        return token;
    }

    private protected async Task<ClaimsPrincipal> BuildUserClaims(UserAccount userAccount)
    {
        var getRoles = await _rolesServices.GetRolesAsync(userAccount);

        var claims = new ClaimsIdentity(IdentityConstants.TwoFactorUserIdScheme);

        claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, userAccount.Id.ToString()));
        claims.AddClaim(new Claim("amr", "Email"));
        foreach (var role in getRoles)
            claims.AddClaim(new Claim(ClaimTypes.Role, role));

        return new ClaimsPrincipal(claims);
    }


}

using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Authentication.Entities;

namespace Authentication;

    public class JwtHandler
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;

        public JwtHandler(
            IConfiguration configuration
        )
        {
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection("JwtSettings");
        }

        public SigningCredentials GetSigningCredentials()
        {

            var key = Encoding.UTF8.GetBytes(_jwtSettings["secretKey"]!);

            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        public async Task<UserToken> GenerateUserToken(List<Claim> claims, UserAccount user)
        {
           
            DateTime expiresDateTime = DateTime.Now.AddHours(Double.Parse(_jwtSettings["expiresHours"]!));

            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtSettings["im_Issuer"],
                audience: _jwtSettings["im_Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(Double.Parse(_jwtSettings["expiresHours"]!)),
                signingCredentials: GetSigningCredentials()
            );

            var userToken = new UserToken()
            {
                Authenticated = true,
                Expiration = expiresDateTime,
                Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions),
                UserName = user.UserName!,
                Email = user.Email,
                Id = user.Id,
                CompanyUserAccounts = user.CompanyUserAccounts,
                Action = ""
            };

            return userToken;
        }
}

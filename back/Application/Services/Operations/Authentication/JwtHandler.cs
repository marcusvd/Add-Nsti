using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System;
using Domain.Entities.Authentication;
using System.Threading.Tasks;
using Application.Services.Operations.Authentication.Dtos;

namespace Application.Services.Operations.Authentication
{
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

            var key = Encoding.UTF8.GetBytes(_jwtSettings["secretKey"]);

            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        public async Task<UserToken> GenerateUserToken(Task<List<Claim>> claims, MyUserDto user)
        {
            
            DateTime expiresDateTime = DateTime.Now.AddHours(Double.Parse(_jwtSettings["expiresHours"]));

            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtSettings["sonny_Issuer"],
                audience: _jwtSettings["sonny_Audience"],
                claims: await claims,
                expires: DateTime.Now.AddHours(Double.Parse(_jwtSettings["expiresHours"])),
                signingCredentials: GetSigningCredentials()
            );

            var userToken = new UserToken()
            {
                Authenticated = true,
                Expiration = expiresDateTime,
                Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions),
                UserName = user.UserName,
                Id = user.Id,
                CompanyId = user.CompanyId,
                Action = ""
            };

            return userToken;
        }





    }
}

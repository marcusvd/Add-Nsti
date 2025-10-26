using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Application.Shared.Dtos;
using Domain.Entities.Authentication;

namespace Application.Auth.JwtServices.Extensions;

public static class JwtExtensionMethods
{
    public static void AddJwtSettings(this IServiceCollection services, IConfiguration Configuration)
    {
        var jwtSettings = Configuration.GetSection("JwtSettings");

        services.AddAuthentication(x =>
             {
                 x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                 x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                 //
                 x.DefaultScheme = IdentityConstants.ApplicationScheme;
                 x.DefaultSignInScheme = IdentityConstants.ExternalScheme;
             }).AddJwtBearer(x =>
             {
                 x.RequireHttpsMetadata = false;
                 x.SaveToken = false;
                 x.TokenValidationParameters = new TokenValidationParameters()
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = jwtSettings["validIssuer"],
                     ValidAudience = jwtSettings["validAudience"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["secretKey"])),
                     //ClockSkew = TimeSpan.Zero
                 };
             });
    }
   
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.Authentication;
using Microsoft.AspNetCore.Identity;
using Application.Services.Operations.Authentication.Dtos;

namespace Application.Services.Operations.Authentication
{
    public interface IAuthServices
    {
        // Task<UserToken> RegisterUser(MyUserDto user);
        Task<bool> RetryConfirmEmailGenerateNewToken(RetryConfirmPasswordDto retryConfirmPassword);
        // Task<UserToken> Login(MyUserDto user);
        Task<bool> ForgotPassword(ForgotPasswordDto forgotPassword);
        ResetPasswordDto ResetPassword(string token, string email);
        Task<bool> ResetPasswordAsync(ResetPasswordDto resetPassword);
        Task<bool> ConfirmEmailAddress(ConfirmEmailDto confirmEmail);
        Task<UserToken> TwoFactor(T2FactorDto t2Factor);

        // ROLES
       // Task<IdentityResult> CreateRole(RoleDto role);
        Task<string> UpdateUserRoles(UpdateUserRoleDto model);
        Task<IList<string>> GetRoles(MyUser user);


    }
}
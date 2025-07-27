using System.Collections.Generic;
using System.Threading.Tasks;
using Authentication;
using Application.Services.Operations.Authentication.Dtos;

namespace Application.Services.Operations.Authentication
{
    public interface IAuthServices
    {
        // Task<UserToken> RegisterUser(UserAccountDto user);
        Task<bool> RetryConfirmEmailGenerateNewToken(RetryConfirmPasswordDto retryConfirmPassword);
        // Task<UserToken> Login(UserAccountDto user);
        Task<bool> ForgotPassword(ForgotPasswordDto forgotPassword);
        ResetPasswordDto ResetPassword(string token, string email);
        Task<bool> ResetPasswordAsync(ResetPasswordDto resetPassword);
        Task<bool> ConfirmEmailAddress(ConfirmEmailDto confirmEmail);
        Task<UserToken> TwoFactor(T2FactorDto t2Factor);

        // ROLES
       // Task<IdentityResult> CreateRole(RoleDto role);
        Task<string> UpdateUserRoles(UpdateUserRoleDto model);
        Task<IList<string>> GetRoles(UserAccount user);


    }
}
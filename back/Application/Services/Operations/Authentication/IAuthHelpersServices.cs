using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.Authentication;
using Microsoft.AspNetCore.Identity;
using Application.Services.Operations.Authentication.Dtos;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Services.Operations.Authentication
{
    public interface IAuthHelpersServices
    {
        void ObjIsNull(object obj);
        Task<bool> NameIsDuplicate(string userName);
        Task<bool> EmailIsDuplicate(string email);
        Task<bool> IsLockedOutAsync(MyUser myUser);
        Task<bool> EmailIsNotConfirmedAsync(MyUser myUser);
        void EmailAlreadyConfirmed(MyUser myUser);
        Task<bool> CheckPasswordAsync(MyUser myUser, string user);
        Task<bool> GetTwoFactorEnabledAsync(MyUser myUser);
        Task<IList<string>> GetValidTwoFactorProvidersAsync(MyUser myUser);
        Task<string> GenerateTwoFactorTokenAsync(MyUser myUser, string provider);
        Task<List<MyUser>> FindAllUsersAsync();
        Task<MyUser> FindUserByEmailAsync(string email);
        Task<MyUser> FindUserByNameAsync(string name);
        Task<MyUser> FindUserByNameAllIncludedAsync(string name);
        Task<MyUser> FindUserByIdAsync(int id);
        Task<MyUser> FindUserByNameOrEmailAsync(string userNameOrEmail);
        Task<bool> VerifyTwoFactorTokenAsync(MyUser myUser, string email, T2FactorDto t2Factor);
        Task<bool> RegisterUserAsync(MyUser user, string password);
        MyUser User(string userName, string email, string companyName);
        MyUserDto MyUserToMyUserDto(MyUser user);
        Task<IdentityResult> UserUpdateAsync(MyUser user);
        Task<string> UrlEmailConfirm(MyUser myUser, string controller, string action);
        Task<bool> ConfirmingEmail(MyUser myUser, ConfirmEmailDto confirmEmail);
        Task<bool> ResetPasswordAsync(ResetPasswordDto resetPassword);
        Task<string> UrlPasswordReset(MyUser myUser, string controller, string action);
        Task<string> GeneratePasswordResetTokenAsync(MyUser myUser);
        //ROLES
      //  Task<IdentityResult> CreateRole(RoleDto role);
        Task<string> UpdateUserRoles(UpdateUserRoleDto model);
        Task<IList<string>> GetRoles(MyUser user);

        //ClAIMS
        Task<List<Claim>> GetClaims(MyUserDto user, Task<IList<string>> roles);

    }
}
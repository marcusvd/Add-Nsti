using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Application.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using Application.Services.Operations.Authentication.Dtos;
using Domain.Entities.Companies;
using Application.Services.Shared.Dtos.Mappers;


namespace Application.Services.Operations.Authentication
{

    public class AuthHelpersServices : IAuthHelpersServices
    {
        private readonly UserManager<UserAccount> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ICommonObjectMapper _mapper;
        private readonly IUrlHelper _url;
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;
        private readonly EmailServer _email;
        public AuthHelpersServices(
            UserManager<UserAccount> userManager,
            IUrlHelper url,
            RoleManager<Role> roleManager,
            ICommonObjectMapper mapper,
            IConfiguration configuration,
            EmailServer email
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _url = url;
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection("JwtSettings");
            _email = email;

        }
        public void ObjIsNull(object obj)
        {
            if (obj == null) throw new AuthServicesException(AuthErrorsMessagesException.ObjectIsNull);
        }
        public async Task<bool> NameIsDuplicate(string userName)
        {
            var userAccount = await _userManager.FindByNameAsync(userName);

            if (userAccount != null) throw new AuthServicesException(AuthErrorsMessagesException.UserNameAlreadyRegisterd);

            return false;
        }
        public async Task<bool> EmailIsDuplicate(string email)
        {
            var userAccount = await _userManager.FindByEmailAsync(email);

            if (userAccount != null) throw new AuthServicesException(AuthErrorsMessagesException.EmailAlreadyRegisterd);

            return false;
        }
        public async Task<bool> IsLockedOutAsync(UserAccount userAccount)
        {
            var result = !await _userManager.IsLockedOutAsync(userAccount);

            if (!result)
            {

                _email.Send(To: userAccount.Email, Subject: "Sonny conta bloqueada.", Body: "O número de dez tentativas de login foi esgotado e a conta foi bloqueada por atingir dez tentativas com senhas incorretas. Sugerimos troque sua senha. " + "Link para troca  de senha.");
                throw new AuthServicesException(AuthErrorsMessagesException.UserIsLocked);
            }
            return result;
        }
        public async Task<bool> EmailIsNotConfirmedAsync(UserAccount userAccount)
        {
            if (!await _userManager.IsEmailConfirmedAsync(userAccount))
                return false;

            return true;
        }

        public void EmailAlreadyConfirmed(UserAccount userAccount)
        {
            if (userAccount.EmailConfirmed)
                throw new AuthServicesException(AuthErrorsMessagesException.IsEmailConfirmed);
        }
        public async Task<bool> CheckPasswordAsync(UserAccount userAccount, string password)
        {
            var result = await _userManager.CheckPasswordAsync(userAccount, password);

            if (result)
            {
                await _userManager.ResetAccessFailedCountAsync(userAccount);
                return true;
            }
            else
            {
                await _userManager.AccessFailedAsync(userAccount);
                throw new AuthServicesException(AuthErrorsMessagesException.InvalidUserNameOrPassword);
            }
        }
        public async Task<bool> GetTwoFactorEnabledAsync(UserAccount userAccount)
        {
            return await _userManager.GetTwoFactorEnabledAsync(userAccount);
        }
        public async Task<IList<string>> GetValidTwoFactorProvidersAsync(UserAccount userAccount)
        {
            return await _userManager.GetValidTwoFactorProvidersAsync(userAccount);
        }
        public async Task<string> GenerateTwoFactorTokenAsync(UserAccount userAccount, string provider)
        {
            if (provider == null) throw new AuthServicesException(AuthErrorsMessagesException.TokenGenerationProvider);

            return await _userManager.GenerateTwoFactorTokenAsync(userAccount, provider);
        }
        public async Task<UserAccount> UpdateUserAsync(int id, UserAccount user)
        {
            if (id != user.Id) throw new AuthServicesException(AuthErrorsMessagesException.ErrorIdUpdateUserAccount);

            var userAccount = await _userManager.FindByIdAsync(id.ToString());

            if (userAccount == null) throw new AuthServicesException(AuthErrorsMessagesException.UserAccountNotFound);

            userAccount = user;

            await _userManager.UpdateAsync(userAccount);


            return userAccount;
        }
        public async Task<UserAccount> FindUserByEmailAsync(string email)
        {
            var userAccount = await _userManager.FindByEmailAsync(email);

            if (userAccount == null) throw new AuthServicesException(AuthErrorsMessagesException.UserAccountNotFound);

            return userAccount;
        }
        public async Task<List<UserAccount>> FindAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            if (users == null) throw new AuthServicesException(AuthErrorsMessagesException.UserAccountNotFound);

            return users;
        }
        public async Task<UserAccount> FindUserByNameAsync(string name)
        {
            try
            {
                if (name == null) throw new AuthServicesException(AuthErrorsMessagesException.ObjectIsNull);

                var userAccount = await _userManager.Users.SingleAsync(x => x.UserName == name);

                return userAccount;
            }
            catch (InvalidOperationException ex)
            {
                throw new AuthServicesException($"{AuthErrorsMessagesException.InvalidUserNameOrPassword} | {ex}");
            }


        }
        public async Task<UserAccount> FindUserByNameAllIncludedAsync(string name)
        {
            try
            {
                if (name == null) throw new AuthServicesException(AuthErrorsMessagesException.ObjectIsNull);

                var userAccount = await _userManager.Users
                 .SingleAsync(x => x.UserName == name);

                return userAccount;
            }
            catch (InvalidOperationException ex)
            {
                throw new AuthServicesException($"{AuthErrorsMessagesException.InvalidUserNameOrPassword} | {ex}");
            }
        }
        public async Task<UserAccount> FindUserByIdAsync(int id)
        {
            var userAccount = await _userManager.FindByIdAsync(id.ToString());

            if (userAccount == null) throw new AuthServicesException(AuthErrorsMessagesException.UserAccountNotFound);

            return userAccount;
        }
        public async Task<UserAccount> FindUserByNameOrEmailAsync(string userNameOrEmail)
        {

            var userAccount = await _userManager.FindByEmailAsync(userNameOrEmail) ?? await _userManager.FindByNameAsync(userNameOrEmail);

            if (userAccount == null) throw new AuthServicesException(AuthErrorsMessagesException.UserAccountNotFound);

            return userAccount;
        }
        public async Task<bool> VerifyTwoFactorTokenAsync(UserAccount userAccount, string email, T2FactorDto t2Factor)
        {
            var result = await _userManager.VerifyTwoFactorTokenAsync(userAccount, email, t2Factor.Token);

            if (!result) throw new AuthServicesException(AuthErrorsMessagesException.ExpiredTokenOrInvalid);

            return result;
        }
        public async Task<bool> RegisterUserAsync(UserAccount user, string password)
        {
            var register = await _userManager.CreateAsync(user, password);

            if (!register.Succeeded) throw new AuthServicesException(AuthErrorsMessagesException.ErrorWhenRegisterUserAccount);

            return register.Succeeded;
        }
        public UserAccount User(string email, string userName = "Incompleto", string companyName = "Incompleto")
        {

            var company = new Company(companyName);

            var userAccount = new UserAccount()
            {
                UserName = email,
                Email = email,
                // Company = company
            };

            return userAccount;
        }
        public async Task<IdentityResult> UserUpdateAsync(UserAccount user)
        {
            var userAccount = await _userManager.FindByIdAsync(user.Id.ToString());

            if (userAccount == null) throw new AuthServicesException(AuthErrorsMessagesException.UserAccountNotFound);
            if (userAccount.Id != user.Id) throw new AuthServicesException(AuthErrorsMessagesException.ErrorIdUpdateUserAccount);

            var userUpdated = await _userManager.UpdateAsync(user);
            //    var userUpdatePasswork = await _userManager.ChangePasswordAsync .UpdateAsync(user);

            if (!userUpdated.Succeeded) throw new AuthServicesException(AuthErrorsMessagesException.ErrorWhenTryUpdateUserAccount);

            return userUpdated;
        }
        public UserAccountDto UserAccountToUserAccountDto(UserAccount user)
        {
            var userAccountDto = _mapper.UserAccountMapper(user);
            return userAccountDto;
        }

        public async Task<string> UrlEmailConfirm(UserAccount userAccount, string controller, string action)
        {

            var urlConfirmMail = _url.Action(action, controller, new
            {
                token = await _userManager.GenerateEmailConfirmationTokenAsync(userAccount),
                email = userAccount.Email
            });

            if (urlConfirmMail == null) throw new AuthServicesException(AuthErrorsMessagesException.ErrorWhenGenerateEmailLink);

            return urlConfirmMail.Replace("api/auth/ConfirmEmailAddress", "");
        }
        // public async Task<string> TokenToChangePassDirect(UserAccount userAccount, string controller, string action)
        // {

        //     var urlConfirmMail = _url.Action(action, controller, new
        //     {
        //         token = await _userManager.GenerateEmailConfirmationTokenAsync(userAccount),
        //     });

        //     if (urlConfirmMail == null) throw new AuthServicesException(AuthErrorsMessagesException.ErrorWhenGenerateEmailLink);

        //     return urlConfirmMail;
        // }
        public async Task<bool> ConfirmingEmail(UserAccount userAccount, ConfirmEmailDto confirmEmail)
        {
            var result = await _userManager.ConfirmEmailAsync(userAccount, confirmEmail.Token);

            return result.Succeeded;

        }
        public async Task<bool> ResetPasswordAsync(ResetPasswordDto resetPassword)
        {
            if (resetPassword == null) throw new AuthServicesException(AuthErrorsMessagesException.ObjectIsNull);

            var userAccount = await _userManager.FindByEmailAsync(resetPassword.Email);

            IdentityResult identityResult = await _userManager.ResetPasswordAsync(userAccount, resetPassword.Token, resetPassword.Password);

            if (!identityResult.Succeeded) throw new AuthServicesException($"{AuthErrorsMessagesException.ResetPassword} - {identityResult}");

            return identityResult.Succeeded;
        }
        public async Task<string> UrlPasswordReset(UserAccount userAccount, string controller, string action)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(userAccount);

            var urlReset = _url.Action(action, controller, new { token = token, email = userAccount.Email }).Remove(0, 15);

            if (urlReset == null) throw new AuthServicesException(AuthErrorsMessagesException.ErrorWhenGenerateEmailLink);

            return urlReset;
        }
        public async Task<string> GeneratePasswordResetTokenAsync(UserAccount userAccount)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(userAccount);

            return token;
        }

        //ROLES
        // public async Task<IdentityResult> CreateRole(RoleDto role)
        // {
        //     var roleDtoToRoleEntity = _iMapper.Map<Role>(role);

        //     var result = await _roleManager.CreateAsync(roleDtoToRoleEntity);

        //     return result;
        // }
        public async Task<string> UpdateUserRoles(UpdateUserRoleDto model)
        {
            var userAccount = await _userManager.FindByNameAsync(model.UserName);

            if (userAccount == null) throw new AuthServicesException(AuthErrorsMessagesException.ObjectIsNull);

            if (model.Delete)
            {
                await _userManager.RemoveFromRoleAsync(userAccount, model.Role);
                return "Role removed";
            }
            else
            {
                await _userManager.AddToRoleAsync(userAccount, model.Role);
                return "Role Added";
            }

            throw new AuthServicesException(AuthErrorsMessagesException.UnknownError);

        }
        public async Task<IList<string>> GetRoles(UserAccount user)
        {
            var role = await _userManager.GetRolesAsync(user);
            return role;
        }

        //ClAIMS

        public async Task<List<Claim>> GetClaims(UserAccountDto user, Task<IList<string>> roles)
        {
           // var userToUserDto = _iMapper.Map<UserAccount>(user);

            var getRoles = await roles;

            var claims = new List<Claim>
            {
              new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
              new Claim(ClaimTypes.Name, user.UserName),
            };

            foreach (var role in getRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

    }
}
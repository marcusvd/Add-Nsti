using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Entities.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Application.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using Application.Services.Operations.Authentication.Dtos;
using Domain.Entities.Main.Companies;
using Application.Services.Shared.Dtos.Mappers;

namespace Application.Services.Operations.Authentication
{

    public class AuthHelpersServices : IAuthHelpersServices
    {
        private readonly UserManager<MyUser> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ICommonObjectMapper _mapper;
        private readonly IUrlHelper _url;
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;
        private readonly EmailServer _email;
        public AuthHelpersServices(
            UserManager<MyUser> userManager,
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
            var myUser = await _userManager.FindByNameAsync(userName);

            if (myUser != null) throw new AuthServicesException(AuthErrorsMessagesException.UserNameAlreadyRegisterd);

            return false;
        }
        public async Task<bool> EmailIsDuplicate(string email)
        {
            var myUser = await _userManager.FindByEmailAsync(email);

            if (myUser != null) throw new AuthServicesException(AuthErrorsMessagesException.EmailAlreadyRegisterd);

            return false;
        }
        public async Task<bool> IsLockedOutAsync(MyUser myUser)
        {
            var result = !await _userManager.IsLockedOutAsync(myUser);

            if (!result)
            {

                _email.Send(To: myUser.Email, Subject: "Sonny conta bloqueada.", Body: "O número de dez tentativas de login foi esgotado e a conta foi bloqueada por atingir dez tentativas com senhas incorretas. Sugerimos troque sua senha. " + "Link para troca  de senha.");
                throw new AuthServicesException(AuthErrorsMessagesException.UserIsLocked);
            }
            return result;
        }
        public async Task<bool> EmailIsNotConfirmedAsync(MyUser myUser)
        {
            if (!await _userManager.IsEmailConfirmedAsync(myUser))
                return false;

            return true;
        }

        public void EmailAlreadyConfirmed(MyUser myUser)
        {
            if (myUser.EmailConfirmed)
                throw new AuthServicesException(AuthErrorsMessagesException.IsEmailConfirmed);
        }
        public async Task<bool> CheckPasswordAsync(MyUser myUser, string password)
        {
            var result = await _userManager.CheckPasswordAsync(myUser, password);

            if (result)
            {
                await _userManager.ResetAccessFailedCountAsync(myUser);
                return true;
            }
            else
            {
                await _userManager.AccessFailedAsync(myUser);
                throw new AuthServicesException(AuthErrorsMessagesException.InvalidUserNameOrPassword);
            }
        }
        public async Task<bool> GetTwoFactorEnabledAsync(MyUser myUser)
        {
            return await _userManager.GetTwoFactorEnabledAsync(myUser);
        }
        public async Task<IList<string>> GetValidTwoFactorProvidersAsync(MyUser myUser)
        {
            return await _userManager.GetValidTwoFactorProvidersAsync(myUser);
        }
        public async Task<string> GenerateTwoFactorTokenAsync(MyUser myUser, string provider)
        {
            if (provider == null) throw new AuthServicesException(AuthErrorsMessagesException.TokenGenerationProvider);

            return await _userManager.GenerateTwoFactorTokenAsync(myUser, provider);
        }
        public async Task<MyUser> UpdateUserAsync(int id, MyUser user)
        {
            if (id != user.Id) throw new AuthServicesException(AuthErrorsMessagesException.ErrorIdUpdateUserAccount);

            var myUser = await _userManager.FindByIdAsync(id.ToString());

            if (myUser == null) throw new AuthServicesException(AuthErrorsMessagesException.UserAccountNotFound);

            myUser = user;

            await _userManager.UpdateAsync(myUser);


            return myUser;
        }
        public async Task<MyUser> FindUserByEmailAsync(string email)
        {
            var myUser = await _userManager.FindByEmailAsync(email);

            if (myUser == null) throw new AuthServicesException(AuthErrorsMessagesException.UserAccountNotFound);

            return myUser;
        }
        public async Task<List<MyUser>> FindAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            if (users == null) throw new AuthServicesException(AuthErrorsMessagesException.UserAccountNotFound);

            return users;
        }
        public async Task<MyUser> FindUserByNameAsync(string name)
        {
            try
            {
                if (name == null) throw new AuthServicesException(AuthErrorsMessagesException.ObjectIsNull);

                var myUser = await _userManager.Users.Include(x => x.Company).SingleAsync(x => x.UserName == name);

                return myUser;
            }
            catch (InvalidOperationException ex)
            {
                throw new AuthServicesException($"{AuthErrorsMessagesException.InvalidUserNameOrPassword} | {ex}");
            }


        }
        public async Task<MyUser> FindUserByNameAllIncludedAsync(string name)
        {
            try
            {
                if (name == null) throw new AuthServicesException(AuthErrorsMessagesException.ObjectIsNull);

                var myUser = await _userManager.Users
                .Include(x => x.Company)
                .Include(x => x.Address)
                .Include(x => x.Contact)
                .ThenInclude(x => x.SocialMedias)
                .SingleAsync(x => x.UserName == name);

                return myUser;
            }
            catch (InvalidOperationException ex)
            {
                throw new AuthServicesException($"{AuthErrorsMessagesException.InvalidUserNameOrPassword} | {ex}");
            }
        }
        public async Task<MyUser> FindUserByIdAsync(int id)
        {
            var myUser = await _userManager.FindByIdAsync(id.ToString());

            if (myUser == null) throw new AuthServicesException(AuthErrorsMessagesException.UserAccountNotFound);

            return myUser;
        }
        public async Task<MyUser> FindUserByNameOrEmailAsync(string userNameOrEmail)
        {

            var myUser = await _userManager.FindByEmailAsync(userNameOrEmail) ?? await _userManager.FindByNameAsync(userNameOrEmail);

            if (myUser == null) throw new AuthServicesException(AuthErrorsMessagesException.UserAccountNotFound);

            return myUser;
        }
        public async Task<bool> VerifyTwoFactorTokenAsync(MyUser myUser, string email, T2FactorDto t2Factor)
        {
            var result = await _userManager.VerifyTwoFactorTokenAsync(myUser, email, t2Factor.Token);

            if (!result) throw new AuthServicesException(AuthErrorsMessagesException.ExpiredTokenOrInvalid);

            return result;
        }
        public async Task<bool> RegisterUserAsync(MyUser user, string password)
        {
            var register = await _userManager.CreateAsync(user, password);

            if (!register.Succeeded) throw new AuthServicesException(AuthErrorsMessagesException.ErrorWhenRegisterUserAccount);

            return register.Succeeded;
        }
        public MyUser User(string email, string userName = "Incompleto", string companyName = "Incompleto")
        {

            var company = new Company(companyName);

            var myUser = new MyUser()
            {
                UserName = email,
                Email = email,
                Company = company
            };

            return myUser;
        }
        public async Task<IdentityResult> UserUpdateAsync(MyUser user)
        {
            var myUser = await _userManager.FindByIdAsync(user.Id.ToString());

            if (myUser == null) throw new AuthServicesException(AuthErrorsMessagesException.UserAccountNotFound);
            if (myUser.Id != user.Id) throw new AuthServicesException(AuthErrorsMessagesException.ErrorIdUpdateUserAccount);

            var userUpdated = await _userManager.UpdateAsync(user);
            //    var userUpdatePasswork = await _userManager.ChangePasswordAsync .UpdateAsync(user);

            if (!userUpdated.Succeeded) throw new AuthServicesException(AuthErrorsMessagesException.ErrorWhenTryUpdateUserAccount);

            return userUpdated;
        }
        public MyUserDto MyUserToMyUserDto(MyUser user)
        {
            var myUserDto = _mapper.MyUserMapper(user);
            return myUserDto;
        }

        public async Task<string> UrlEmailConfirm(MyUser myUser, string controller, string action)
        {

            var urlConfirmMail = _url.Action(action, controller, new
            {
                token = await _userManager.GenerateEmailConfirmationTokenAsync(myUser),
                email = myUser.Email
            });

            if (urlConfirmMail == null) throw new AuthServicesException(AuthErrorsMessagesException.ErrorWhenGenerateEmailLink);

            return urlConfirmMail.Replace("api/auth/ConfirmEmailAddress", "");
        }
        // public async Task<string> TokenToChangePassDirect(MyUser myUser, string controller, string action)
        // {

        //     var urlConfirmMail = _url.Action(action, controller, new
        //     {
        //         token = await _userManager.GenerateEmailConfirmationTokenAsync(myUser),
        //     });

        //     if (urlConfirmMail == null) throw new AuthServicesException(AuthErrorsMessagesException.ErrorWhenGenerateEmailLink);

        //     return urlConfirmMail;
        // }
        public async Task<bool> ConfirmingEmail(MyUser myUser, ConfirmEmailDto confirmEmail)
        {
            var result = await _userManager.ConfirmEmailAsync(myUser, confirmEmail.Token);

            return result.Succeeded;

        }
        public async Task<bool> ResetPasswordAsync(ResetPasswordDto resetPassword)
        {
            if (resetPassword == null) throw new AuthServicesException(AuthErrorsMessagesException.ObjectIsNull);

            var myUser = await _userManager.FindByEmailAsync(resetPassword.Email);

            IdentityResult identityResult = await _userManager.ResetPasswordAsync(myUser, resetPassword.Token, resetPassword.Password);

            if (!identityResult.Succeeded) throw new AuthServicesException($"{AuthErrorsMessagesException.ResetPassword} - {identityResult}");

            return identityResult.Succeeded;
        }
        public async Task<string> UrlPasswordReset(MyUser myUser, string controller, string action)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(myUser);

            var urlReset = _url.Action(action, controller, new { token = token, email = myUser.Email }).Remove(0, 15);

            if (urlReset == null) throw new AuthServicesException(AuthErrorsMessagesException.ErrorWhenGenerateEmailLink);

            return urlReset;
        }
        public async Task<string> GeneratePasswordResetTokenAsync(MyUser myUser)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(myUser);

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
            var myUser = await _userManager.FindByNameAsync(model.UserName);

            if (myUser == null) throw new AuthServicesException(AuthErrorsMessagesException.ObjectIsNull);

            if (model.Delete)
            {
                await _userManager.RemoveFromRoleAsync(myUser, model.Role);
                return "Role removed";
            }
            else
            {
                await _userManager.AddToRoleAsync(myUser, model.Role);
                return "Role Added";
            }

            throw new AuthServicesException(AuthErrorsMessagesException.UnknownError);

        }
        public async Task<IList<string>> GetRoles(MyUser user)
        {
            var role = await _userManager.GetRolesAsync(user);
            return role;
        }

        //ClAIMS

        public async Task<List<Claim>> GetClaims(MyUserDto user, Task<IList<string>> roles)
        {
           // var userToUserDto = _iMapper.Map<MyUser>(user);

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
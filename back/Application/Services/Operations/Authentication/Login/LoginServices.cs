using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Services.Operations.Authentication;
using Application.Services.Operations.Authentication.Dtos;
using Application.Services.Shared.Dtos.Mappers;
using Domain.Entities.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Operations.Authentication.Login
{
    public class LoginServices : ILoginServices
    {

        private UserManager<MyUser> _userManager;
        private readonly EmailServer _email;
        private readonly JwtHandler _jwtHandler;
        private readonly ICommonObjectMapper _mapper;
        public LoginServices(
              UserManager<MyUser> userManager,
              EmailServer email,
              JwtHandler jwtHandler,
               ICommonObjectMapper mapper

          )
        {
            _userManager = userManager;
            _email = email;
            _jwtHandler = jwtHandler;
            _mapper = mapper;
        }

        public async Task<UserToken> Login(MyUserDto user)
        {
            if (user == null) throw new Exception(GlobalErrorsMessagesException.ObjIsNull);

            var myUser = await FindUserByNameOrEmailAsync(user.UserName);

            if (myUser == null) throw new AuthServicesException(AuthErrorsMessagesException.UserAccountNotFound);

            if (!await IsLockedOutAsync(myUser))
            {
                if (!await EmailIsNotConfirmedAsync(myUser))
                    throw new AuthServicesException(AuthErrorsMessagesException.EmailIsNotConfirmed);

                if (await CheckPasswordAsync(myUser, user.Password))
                {
                    if (await IsEnabledTwoFactorAsync(myUser))
                    {
                        var returnUserToken = await _jwtHandler.GenerateUserToken(GetClaims(user, _userManager.GetRolesAsync(myUser)), user);
                        returnUserToken.Action = "TwoFactor";
                        return returnUserToken;
                    }

                    return await _jwtHandler.GenerateUserToken(GetClaims(user, _userManager.GetRolesAsync(myUser)), _mapper.MyUserMapper(myUser));
                }
                else
                    throw new AuthServicesException(AuthErrorsMessagesException.InvalidUserNameOrPassword);
            }
            else
                throw new AuthServicesException(AuthErrorsMessagesException.UserIsLocked);

        }
        private async Task<bool> IsEnabledTwoFactorAsync(MyUser myUser)
        {
            if (await _userManager.GetTwoFactorEnabledAsync(myUser))
            {
                var validator = await _userManager.GetValidTwoFactorProvidersAsync(myUser);

                if (validator.Contains("Email"))
                {
                    var token = await _userManager.GenerateTwoFactorTokenAsync(myUser, "Email");
                    _email.Send(To: myUser.Email, Subject: "SONNY: Autenticação de dois fatores", Body: "Código: Autenticação de dois fatores: " + token);

                    return true;
                }
            }
            return false;
        }
        private async Task<MyUser> FindUserByNameOrEmailAsync(string userNameOrEmail)
        {
            var myUser = await _userManager.FindByEmailAsync(userNameOrEmail) ?? await _userManager.FindByNameAsync(userNameOrEmail);
            return myUser;
        }
        private async Task<bool> IsLockedOutAsync(MyUser myUser)
        {
            var result = await _userManager.IsLockedOutAsync(myUser);
            if (result)
            {
                _email.Send(To: myUser.Email, Subject: "Sonny conta bloqueada.", Body: "O número de dez tentativas de login foi esgotado e a conta foi bloqueada por atingir dez tentativas com senhas incorretas. Sugerimos troque sua senha. " + "Link para troca  de senha.");
                return result;
            }
            return result;
        }
        private async Task<bool> EmailIsNotConfirmedAsync(MyUser myUser)
        {
            if (!await _userManager.IsEmailConfirmedAsync(myUser))
                return false;

            return true;
        }
        private async Task<bool> CheckPasswordAsync(MyUser myUser, string password)
        {
            var result = await _userManager.CheckPasswordAsync(myUser, password);

            if (result)
            {
                await _userManager.ResetAccessFailedCountAsync(myUser);
                return result;
            }
            else
            {
                await _userManager.AccessFailedAsync(myUser);
                return result;
            }
        }
        public async Task<List<Claim>> GetClaims(MyUserDto user, Task<IList<string>> roles)
        {
            //var userToUserDto = _mapper.MyUserMapper(user);

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

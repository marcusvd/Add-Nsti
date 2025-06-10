using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Services.Operations.Authentication;
using Application.Services.Operations.Authentication.Dtos;
using Domain.Entities.Authentication;
using Domain.Entities.Main.Companies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Operations.Authentication.Register
{
    public class RegisterServices : IRegisterServices
    {

        private UserManager<MyUser> _userManager;
        private readonly EmailServer _email;
        private readonly JwtHandler _jwtHandler;
        private readonly IUrlHelper _url;
        public RegisterServices(
              UserManager<MyUser> userManager,
              EmailServer email,
              JwtHandler jwtHandler,
              IUrlHelper url

          )
        {
            _userManager = userManager;
            _email = email;
            _jwtHandler = jwtHandler;
            _url = url;
        }

        public async Task<UserToken> Register(MyUserDto user)
        {
            if (user == null) throw new Exception(GlobalErrorsMessagesException.ObjIsNull);

            if (await NameIsDuplicate(user.UserName)) throw new AuthServicesException(AuthErrorsMessagesException.UserNameAlreadyRegisterd);

            if (await EmailIsDuplicate(user.Email)) throw new AuthServicesException(AuthErrorsMessagesException.EmailAlreadyRegisterd);

            var myUser = User(user.Email, user.UserName, user.Company.Name);
            //remove this.
            myUser.EmailConfirmed = true;

            if ((await _userManager.CreateAsync(myUser, user.Password)).Succeeded)
            {
                string urlToken = await UrlEmailConfirm(myUser, "auth", "ConfirmEmailAddress");

                if (urlToken == null) throw new AuthServicesException(AuthErrorsMessagesException.ErrorWhenGenerateEmailLink);

                // _email.Send(To: myUser.Email, Subject: "Sonny - Link para confirmação de e-mail", Body: "http://localhost:4200/confirm-email" + urlToken.Replace("api/auth/ConfirmEmailAddress", ""));
                _email.Send(To: myUser.Email, Subject: "Sonny - Link para confirmação de e-mail", Body: "http://sonnyapp.intra/confirm-email" + urlToken.Replace("api/auth/ConfirmEmailAddress", ""));
            }
            else
                throw new AuthServicesException(AuthErrorsMessagesException.ErrorWhenRegisterUserAccount);

            return await _jwtHandler.GenerateUserToken(GetClaims(user, _userManager.GetRolesAsync(myUser)), user);
        }
        private async Task<bool> NameIsDuplicate(string userName)
        {
            var myUser = await _userManager.FindByNameAsync(userName);

            if (myUser != null)
                return true;

            return false;
        }
        private async Task<bool> EmailIsDuplicate(string email)
        {
            var myUser = await _userManager.FindByEmailAsync(email);

            if (myUser != null)
                return true;

            return false;
        }
        private MyUser User(string email, string userName = "Incompleto", string companyName = "Incompleto")
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
        private async Task<bool> RegisterUserAsync(MyUser user, string password)
        {
            var register = await _userManager.CreateAsync(user, password);

            return register.Succeeded;
        }
        private async Task<List<Claim>> GetClaims(MyUserDto user, Task<IList<string>> roles)
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
        private async Task<string> UrlEmailConfirm(MyUser myUser, string controller, string action)
        {
            var urlConfirmMail = _url.Action(action, controller, new
            {
                token = await _userManager.GenerateEmailConfirmationTokenAsync(myUser),
                email = myUser.Email
            });

            return urlConfirmMail;
        }
    }
}

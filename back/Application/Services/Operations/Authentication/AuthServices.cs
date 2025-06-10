using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Exceptions;
using Domain.Entities.Authentication;
using Application.Services.Operations.Authentication.Dtos;

namespace Application.Services.Operations.Authentication
{
    public class AuthServices : IAuthServices
    {

        private readonly IAuthHelpersServices _iAuthHelpersServices;
        private readonly JwtHandler _jwtHandler;
        private readonly EmailServer _email;
        private readonly Email _email2;
        public AuthServices(
        IAuthHelpersServices iAuthHelpersServices,
         EmailServer email,
         Email email2,
        JwtHandler jwtHandler
        )
        {
            _iAuthHelpersServices = iAuthHelpersServices;
            _email = email;
            _email2 = email2;
            _jwtHandler = jwtHandler;
        }
      
        public async Task<bool> RetryConfirmEmailGenerateNewToken(RetryConfirmPasswordDto retryConfirmPassword)
        {

            _iAuthHelpersServices.ObjIsNull(retryConfirmPassword);

            var myUser = await _iAuthHelpersServices.FindUserByEmailAsync(retryConfirmPassword.Email);

            _iAuthHelpersServices.EmailAlreadyConfirmed(myUser);

            string urlToken = await _iAuthHelpersServices.UrlEmailConfirm(myUser, "auth", "ConfirmEmailAddress");

            _email.Send(To: myUser.Email, Subject: "Sonny - Link para confirmação de e-mail", Body: "http://sonnyapp.intra/confirm-email" + urlToken.Replace("api/auth/ConfirmEmailAddress", ""));
            // _email.Send(To: myUser.Email, Subject: "Sonny - Link para confirmação de e-mail", Body: "http://localhost:4200/confirm-email" + urlToken.Replace("api/auth/ConfirmEmailAddress", ""));

            return true;
        }


        public async Task<bool> ForgotPassword(ForgotPasswordDto forgotPassword)
        {
            _iAuthHelpersServices.ObjIsNull(forgotPassword);

            var myUser = await _iAuthHelpersServices.FindUserByEmailAsync(forgotPassword.Email);

            string urlToken = await _iAuthHelpersServices.UrlPasswordReset(myUser, "auth", "Reset");

            _email.Send(To: myUser.Email, Subject: "Sonny - Link para reset de senha.", Body: "http://sonnyapp.intra/reset-password" + urlToken.Replace("api/auth/ConfirmEmailAddress", ""));

            return true;
        }
        public ResetPasswordDto ResetPassword(string token, string email)
        {
            return new ResetPasswordDto { Token = token, Email = email };
        }
        public async Task<bool> ResetPasswordAsync(ResetPasswordDto resetPassword)
        {
            return await _iAuthHelpersServices.ResetPasswordAsync(resetPassword);
        }
        public async Task<bool> ConfirmEmailAddress(ConfirmEmailDto confirmEmail)
        {
            var myUser = await _iAuthHelpersServices.FindUserByEmailAsync(confirmEmail.Email);

            if (myUser.EmailConfirmed)
                throw new AuthServicesException("Email já foi confirmado.");

            return await _iAuthHelpersServices.ConfirmingEmail(myUser, confirmEmail);
        }
        public async Task<UserToken> TwoFactor(T2FactorDto t2Factor)
        {
            var myUser = await _iAuthHelpersServices.FindUserByNameAsync(t2Factor.UserName);

            await _iAuthHelpersServices.VerifyTwoFactorTokenAsync(myUser, "Email", t2Factor);

            return await _jwtHandler.GenerateUserToken(
                                     _iAuthHelpersServices.GetClaims(_iAuthHelpersServices.MyUserToMyUserDto(myUser),
                                     _iAuthHelpersServices.GetRoles(myUser)),
                                     _iAuthHelpersServices.MyUserToMyUserDto(myUser));
        }
        // public async Task<IdentityResult> CreateRole(RoleDto role)
        // {
        //     _iAuthHelpersServices.ObjIsNull(role);

        //     return await _iAuthHelpersServices.CreateRole(role);

        // }
        public async Task<string> UpdateUserRoles(UpdateUserRoleDto role)
        {
            _iAuthHelpersServices.ObjIsNull(role);

            return await _iAuthHelpersServices.UpdateUserRoles(role);

        }
        public async Task<IList<string>> GetRoles(MyUser user)
        {
            _iAuthHelpersServices.ObjIsNull(user);

            return await _iAuthHelpersServices.GetRoles(user);
        }
    }
}
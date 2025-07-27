using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Services.Operations.Authentication;
using Application.Services.Operations.Authentication.Dtos;
using Application.Exceptions;
using Application.Services.Operations.Authentication.Login;
using Application.Services.Operations.Authentication.Register;

namespace Api.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/{controller}")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _iAuthServices;
        private readonly ILoginServices _iLoginServices;
        private readonly IRegisterServices _iRegisterServices;

        public AuthController(
            IAuthServices iAuthServices,
            ILoginServices iLoginServices,
            IRegisterServices iRegisterServices
            )
        {
            _iAuthServices = iAuthServices;
            _iLoginServices = iLoginServices;
            _iRegisterServices = iRegisterServices;
        }

        [HttpPost("RegisterAsync")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto user)
        {
            var result = await _iRegisterServices.RegisterAsync(user);

            if (!result.Authenticated)
                throw new Exception("Erro na tentativa de criar o usuário.");

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserAccountDto user)
        {
            var login = await _iLoginServices.Login(user);

            if (!login.Authenticated)
               throw new Exception("Erro na tentativa de criar o usuário.");

            return Ok(login);
        }

        [HttpGet]
        public ResetPasswordDto Reset([FromBody] string token, string email)
        {
            ResetPasswordDto result = _iAuthServices.ResetPassword(token, email);
            return result;
        }

        [HttpPost("Reset")]
        public async Task<IActionResult> Reset([FromBody] ResetPasswordDto resetPassword)
        {
            return Ok(await _iAuthServices.ResetPasswordAsync(resetPassword));
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPassword)
        {
            return Ok(await _iAuthServices.ForgotPassword(forgotPassword));
        }

        [HttpPost("RetryConfirmEmailGenerateNewToken")]
        public async Task<IActionResult> RetryConfirmEmailGenerateNewToken([FromBody] RetryConfirmPasswordDto retryConfirmPassword)
        {
            if (!await _iAuthServices.RetryConfirmEmailGenerateNewToken(retryConfirmPassword)) throw new GlobalServicesException(GlobalErrorsMessagesException.IsObjNull);

            return Ok(true);
        }

        [HttpPost("ConfirmEmailAddress")]
        public async Task<IActionResult> ConfirmEmailAddress([FromBody] ConfirmEmailDto confirmEmail)
        {
            if (confirmEmail == null) throw new GlobalServicesException(GlobalErrorsMessagesException.IsObjNull);

            if (!await _iAuthServices.ConfirmEmailAddress(confirmEmail)) throw new GlobalServicesException(GlobalErrorsMessagesException.IsObjNull);

            return Ok(true);
        }

        [HttpPost("TwoFactor")]
        public async Task<IActionResult> TwoFactor([FromBody] T2FactorDto t2Factor)
        {
            var result = await _iAuthServices.TwoFactor(t2Factor);

            return Ok(result);
        }

        [HttpPut("UpdateUserRole")]
        public async Task<IActionResult> UpdateUserRole([FromBody] UpdateUserRoleDto model)
        {
            var result = await _iAuthServices.UpdateUserRoles(model);
            return Ok(result);
        }





    }
}

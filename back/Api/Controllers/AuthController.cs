using Application.Services.Operations.Account;
using Application.Services.Operations.Auth.Login;
using Application.Services.Operations.Auth.Register;
using Domain.Entities.Authentication;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/{controller}")]
    public class AuthController : ControllerBase
    {

        private readonly ILoginServices _iLoginServices;
        private readonly IFirstRegisterBusinessServices _iRegisterServices;
        private readonly IAccountManagerServices _iAccountManagerServices;

        public AuthController(

            ILoginServices iLoginServices,
            IFirstRegisterBusinessServices iRegisterServices,
            IAccountManagerServices iAccountManagerServices
            )
        {
            _iLoginServices = iLoginServices;
            _iRegisterServices = iRegisterServices;
            _iAccountManagerServices = iAccountManagerServices;
        }

        [HttpPost("RegisterAsync")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel user)
        {
            var result = await _iRegisterServices.RegisterAsync(user);

            return Ok(result);
        }

        [HttpPost("LoginAsync")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel user)
        {
            var result = await _iLoginServices.LoginAsync(user);

            return Ok(result);
        }

        [HttpPost("ConfirmEmailAddress")]
        public async Task<IActionResult> ConfirmEmailAddressAsync([FromBody] ConfirmEmail confirmEmail)
        {
            var result = await _iAccountManagerServices.ConfirmEmailAddressAsync(confirmEmail);

            return Ok(result);
        }
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPassword forgotPassword)
        {
            return Ok(await _iAccountManagerServices.ForgotPasswordAsync(forgotPassword));
        }

        [HttpPost("ResetPasswordAsync")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPassword resetPassword)
        {
            return Ok(await _iAccountManagerServices.ResetPasswordAsync(resetPassword));
        }

        [HttpGet("IsUserExistCheckByEmailAsync/{email}")]
        public async Task<IActionResult> IsUserExistCheckByEmailAsync(string email)
        {
            var result = await _iAccountManagerServices.IsUserExistCheckByEmailAsync(email);

            return Ok(result);
        }


    }
}

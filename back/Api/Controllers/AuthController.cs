using Authentication.Entities;
using Authentication.Operations.Account;
using Authentication.Operations.Login;
using Authentication.Operations.Register;
// using Authentication.Operations.UrlGenerator;
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
        private readonly IRegisterServices _iRegisterServices;
        // private readonly IUrlGenerator _iUrlGenerator;
        private readonly IAccountManagerServices _iAccountManagerServices;

        public AuthController(

            ILoginServices iLoginServices,
            IRegisterServices iRegisterServices,
            // // IUrlGenerator iUrlGenerator,
            IAccountManagerServices iAccountManagerServices
            )
        {
            _iLoginServices = iLoginServices;
            _iRegisterServices = iRegisterServices;
            // // _iUrlGenerator = iUrlGenerator;
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
        public async Task<IActionResult> ConfirmEmailAddress([FromBody] ConfirmEmail confirmEmail)
        {
            var result = await _iAccountManagerServices.ConfirmEmailAddress(confirmEmail);

            return Ok(result);
        }
         [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPassword forgotPassword)
        {
            return Ok(await _iAccountManagerServices.ForgotPassword(forgotPassword));
        }

        [HttpGet("IsUserExistCheckByEmail/{email}")]
        public async Task<IActionResult> IsUserExistCheckByEmail(string email)
        {
            var result = await _iAccountManagerServices.IsUserExistCheckByEmail(email);

            return Ok(result);
        }


    }
}

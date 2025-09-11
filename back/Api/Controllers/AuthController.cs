using Application.Services.Helpers.ServicesLauncher;
using Application.Services.Operations.Auth.Account.dtos;
using Application.Services.Operations.Auth.Dtos;
using Application.Services.Operations.Profiles.Dtos;
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
        private readonly IServiceLaucherService _ServiceLaucherService;

        public AuthController(
             IServiceLaucherService ServiceLaucherService
            )
        {
            _ServiceLaucherService = ServiceLaucherService;
        }

        [HttpPost("RegisterAsync")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModelDto user)
        {
            var result = await _ServiceLaucherService.RegisterServices.RegisterAsync(user);

            return Ok(result);
        }

        [HttpPost("LoginAsync")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModelDto user)
        {
            var result = await _ServiceLaucherService.LoginServices.LoginAsync(user);

            return Ok(result);
        }

        [HttpPost("ConfirmEmailAddress")]
        public async Task<IActionResult> ConfirmEmailAddressAsync([FromBody] ConfirmEmailDto confirmEmail)
        {
            var result = await _ServiceLaucherService.AccountManagerServices.ConfirmEmailAddressAsync(confirmEmail);

            return Ok(result);
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordDto forgotPassword)
        {
            return Ok(await _ServiceLaucherService.AccountManagerServices.ForgotPasswordAsync(forgotPassword));
        }

        [HttpPost("RequestEmailChange")]
        public async Task<IActionResult> RequestEmailChangeAsync([FromBody] RequestEmailChangeDto requestEmailChangeDto)
        {
            return Ok(await _ServiceLaucherService.AccountManagerServices.RequestEmailChangeAsync(requestEmailChangeDto));
        }

        [HttpPut("UpdateUserAccountAuthAsync/{id:min(1)}")]
        public async Task<IActionResult> UpdateUserAccountAuthAsync([FromBody] UserAccountAuthUpdateDto userAccountUpdate, int id)
        {
            return Ok(await _ServiceLaucherService.AccountManagerServices.UpdateUserAccountAuthAsync(userAccountUpdate, id));
        }

        [HttpPut("UpdateUserAccountProfileAsync/{id:min(1)}")]
        public async Task<IActionResult> UpdateUserAccountProfileAsync([FromBody] UserProfileDto userAccountUpdate, int id)
        {
            return Ok(await _ServiceLaucherService.AccountManagerServices.UpdateUserAccountProfileAsync(userAccountUpdate, id));
        }

        [HttpPost("ConfirmRequestEmailChange")]
        public async Task<IActionResult> ConfirmRequestEmailChange([FromBody] ConfirmEmailChangeDto confirmRequestEmailChange)
        {
            return Ok(await _ServiceLaucherService.AccountManagerServices.ConfirmYourEmailChangeAsync(confirmRequestEmailChange));
        }

        [HttpPost("ResetPasswordAsync")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordDto resetPassword)
        {
            return Ok(await _ServiceLaucherService.AccountManagerServices.ResetPasswordAsync(resetPassword));
        }
      
        [HttpGet("IsUserExistCheckByEmailAsync/{email}")]
        public async Task<IActionResult> IsUserExistCheckByEmailAsync(string email)
        {
            var result = await _ServiceLaucherService.AccountManagerServices.IsUserExistCheckByEmailAsync(email);

            return Ok(result);
        }


    }
}

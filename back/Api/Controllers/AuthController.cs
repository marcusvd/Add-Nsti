using System.Security.Claims;
using Application.Services.Helpers.ServicesLauncher;
using Application.Services.Operations.Auth.Account.dtos;
using Application.Services.Operations.Auth.Dtos;
using Application.Services.Operations.Profiles.Dtos;
using Domain.Entities.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

            var properties = new AuthenticationProperties { IsPersistent = true };

            var getUser = await _ServiceLaucherService.LoginServices.GetUser(user.Email);

            var result2 = await _ServiceLaucherService.LoginServices.BuildUserClaims(getUser);

            await HttpContext.SignInAsync(IdentityConstants.TwoFactorUserIdScheme, result2, properties);

            result = result;


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

        // [HttpPost("twofactorverify")]
        // public async Task<IActionResult> twofactorverify(TwoFactorCheckDto twoFactorCheck)
        // {
        //     return Ok(await _ServiceLaucherService.AccountManagerServices.TwoFactorVerifyAsync(twoFactorCheck.Email, twoFactorCheck.Token));
        // }
        // [HttpPost("test")]
        // public async Task<IActionResult> test(TwoFactorCheckDto twoFactorCheck)
        // {
        //     //          private protected async Task<bool> HandleTwoFactorAuthenticationAsync(UserAccount userAccount)
        //     // {
        //     //     if (!await _GENERIC_REPO.UsersManager.GetTwoFactorEnabledAsync(userAccount))
        //     //         return false;

        //     //     var validProviders = await _GENERIC_REPO.UsersManager.GetValidTwoFactorProvidersAsync(userAccount);

        //     //     if (!validProviders.Contains("Email"))
        //     //         return false;

        //     //     // var token = await _GENERIC_REPO.UsersManager.GenerateTwoFactorTokenAsync(userAccount, "Email");
        //     //     // var token = await GenerateTwoFactorTokenAsync(userAccount, "twofactorverify", "auth");
        //     //     var token = await _GENERIC_REPO.UsersManager.GenerateTwoFactorTokenAsync(userAccount, "Email");

        //     //     string linkToken = $"http://localhost:4200/two-factor-check/{token}/{userAccount.Email}";

        //     //     await SendTwoFactorTokenAsync(userAccount, linkToken);

        //     await HttpContext.SignInAsync(IdentityConstants.TwoFactorUserIdScheme, Store2FA(userAccount.Id, "Email"));


        // }

        // private ClaimsPrincipal Store2FA(int id, string provider)
        // {
        //     var identity = new ClaimsIdentity(new List<Claim>
        // {
        //     new Claim("sub", id.ToString()),
        //     new Claim("amr", provider)

        // }, IdentityConstants.TwoFactorUserIdScheme);


        //     return new ClaimsPrincipal(identity);
        // }
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

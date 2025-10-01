using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


using Application.Services.Helpers.ServicesLauncher;
using Application.Services.Operations.Auth.Account.dtos;
using Application.Services.Operations.Auth.Dtos;
using Application.Services.Operations.Profiles.Dtos;


namespace Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/{controller}")]
public class AuthController : ControllerBase
{
    private readonly IServiceLaucherService _ServiceLaucherService;
    public AuthController(IServiceLaucherService ServiceLaucherService) { _ServiceLaucherService = ServiceLaucherService; }



    [HttpPost("RegisterAsync")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterModelDto user) => Ok(await _ServiceLaucherService.RegisterServices.RegisterAsync(user));

    [HttpPost("LoginAsync")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginModelDto user) => Ok(await _ServiceLaucherService.LoginServices.LoginAsync(user));

    [HttpPost("ConfirmEmailAddress")]
    public async Task<IActionResult> ConfirmEmailAddressAsync([FromBody] ConfirmEmailDto confirmEmail) => Ok(await _ServiceLaucherService.AccountManagerServices.ConfirmEmailAddressAsync(confirmEmail));

    [HttpPost("ResendConfirmEmailAsync")]
    public async Task<IActionResult> ResendConfirmEmailAsync([FromBody] ResendConfirmEmailViewModel request) => Ok(await _ServiceLaucherService.AccountManagerServices.ResendConfirmEmailAsync(request.Email));

    [HttpPost("LogoutAsync")]
    [Authorize]
    public async Task<IActionResult> LogoutAsync() => Ok(await _ServiceLaucherService.LoginServices.LogoutAsync());

    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordDto forgotPassword) => Ok(await _ServiceLaucherService.AccountManagerServices.ForgotPasswordAsync(forgotPassword));

    [HttpPost("RequestEmailChange")]
    public async Task<IActionResult> RequestEmailChangeAsync([FromBody] RequestEmailChangeDto requestEmailChangeDto) => Ok(await _ServiceLaucherService.AccountManagerServices.RequestEmailChangeAsync(requestEmailChangeDto));

    [HttpPut("UpdateUserAccountAuthAsync/{id:min(1)}")]
    public async Task<IActionResult> UpdateUserAccountAuthAsync([FromBody] UserAccountAuthUpdateDto userAccountUpdate, int id) => Ok(await _ServiceLaucherService.AccountManagerServices.UpdateUserAccountAuthAsync(userAccountUpdate, id));

    [HttpPut("UpdateUserAccountProfileAsync/{id:min(1)}")]
    public async Task<IActionResult> UpdateUserAccountProfileAsync([FromBody] UserProfileDto userAccountUpdate, int id) => Ok(await _ServiceLaucherService.AccountManagerServices.UpdateUserAccountProfileAsync(userAccountUpdate, id));

    [HttpPost("ConfirmRequestEmailChange")]
    public async Task<IActionResult> ConfirmRequestEmailChange([FromBody] ConfirmEmailChangeDto confirmRequestEmailChange) => Ok(await _ServiceLaucherService.AccountManagerServices.ConfirmYourEmailChangeAsync(confirmRequestEmailChange));

    [HttpPost("ResetPasswordAsync")]
    public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordDto resetPassword) => Ok(await _ServiceLaucherService.AccountManagerServices.ResetPasswordAsync(resetPassword));

    [HttpGet("IsUserExistCheckByEmailAsync/{email}")]
    public async Task<IActionResult> IsUserExistCheckByEmailAsync(string email) => Ok(await _ServiceLaucherService.AccountManagerServices.IsUserExistCheckByEmailAsync(email));
}

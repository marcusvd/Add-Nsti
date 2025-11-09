using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using Application.Helpers.ServicesLauncher;

using AApplication.Auth.UsersAccountsServices.PasswordServices.Dtos;

namespace Api.Controllers;

[ApiController]
[Route("api/{controller}")]
[Authorize(Roles = "SYSADMIN")]
public class _PasswordsController : ControllerBase
{
    private readonly IServiceLaucherService _ServiceLaucherService;
    public _PasswordsController(IServiceLaucherService ServiceLaucherService) { _ServiceLaucherService = ServiceLaucherService; }
    [HttpPost("PasswordChangeAsync")]
    public async Task<IActionResult> PasswordChangeAsync([FromBody] PasswordChangeDto passwordChange) =>
    Ok(await _ServiceLaucherService.PasswordServices.PasswordChangeAsync(passwordChange));
    [HttpPost("MarkPasswordExpireAsync")]
    public async Task<IActionResult> MarkPasswordExpireAsync([FromBody] PasswordWillExpiresDto passwordWillExpires) =>
         Ok(await _ServiceLaucherService.PasswordServices.MarkPasswordExpireAsync(passwordWillExpires));
    [HttpPut("StaticPasswordDefined")]
    public async Task<IActionResult> StaticPasswordDefined([FromBody] ResetStaticPasswordDto reset) =>
        Ok(await _ServiceLaucherService.PasswordServices.SetStaticPassword(reset));

    [HttpGet("IsPasswordExpiresAsync/{userId:min(1)}")]
    public async Task<IActionResult> IsPasswordExpiresAsync(int userId) =>
        Ok(await _ServiceLaucherService.PasswordServices.IsPasswordExpiresAsync(userId));
        

    [HttpPost("ForgotPassword")]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordDto forgotPassword) =>
       Ok(await _ServiceLaucherService.PasswordServices.ForgotPasswordAsync(forgotPassword));

    [HttpPost("ResetPasswordAsync")]
    public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordDto resetPassword) => Ok(await _ServiceLaucherService.PasswordServices.ResetPasswordAsync(resetPassword));




}
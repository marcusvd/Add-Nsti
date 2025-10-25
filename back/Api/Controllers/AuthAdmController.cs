using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using Application.Helpers.ServicesLauncher;
using  Application.Shared.Dtos;
using Application.Auth.Dtos;

namespace Api.Controllers;

[ApiController]
[Route("api/{controller}")]
[Authorize(Roles = "SYSADMIN")]
public class AuthAdmController : ControllerBase
{
    private readonly IServiceLaucherService _ServiceLaucherService;
    public AuthAdmController(IServiceLaucherService ServiceLaucherService) { _ServiceLaucherService = ServiceLaucherService; }
 

    // [HttpPost("PasswordChangeAsync")]
    // // // // // public async Task<IActionResult> PasswordChangeAsync([FromBody] PasswordChangeDto passwordChange) => Ok(await _ServiceLaucherService.AccountManagerServices.PasswordChangeAsync(passwordChange));

    [HttpGet("GetAccountStatus/{email}")]
    public async Task<IActionResult> GetAccountStatus(string email)
    {
        var emailConfirmed = await _ServiceLaucherService.AccountManagerServices.IsEmailConfirmedAsync(email);
        var accountLockedOut = await _ServiceLaucherService.AccountManagerServices.IsAccountLockedOut(email);

        AccountStatusDto result = new() { IsEmailConfirmed = emailConfirmed, IsAccountLockedOut = accountLockedOut };

        return Ok(result);
    }

    [HttpPut("ManualConfirmEmailAddress")]
    public async Task<IActionResult> ManualConfirmEmailAddress([FromBody] EmailConfirmManualDto emailConfirmManual) => Ok(await _ServiceLaucherService.AccountManagerServices.ManualConfirmEmailAddress(emailConfirmManual));

    [HttpPut("ManualAccountLockedOut")]
    public async Task<IActionResult> ManualAccountLockedOut([FromBody] AccountLockedOutManualDto AccountLockedOutManual) => Ok(await _ServiceLaucherService.AccountManagerServices.ManualAccountLockedOut(AccountLockedOutManual));

    // [HttpPut("MarkPasswordExpireAsync")]
    // // // // // public async Task<IActionResult> MarkPasswordExpireAsync([FromBody] PasswordWillExpiresDto passwordWillExpires) => Ok(await _ServiceLaucherService.AccountManagerServices.MarkPasswordExpireAsync(passwordWillExpires));

    // [HttpPut("StaticPasswordDefined")]
    // // // public async Task<IActionResult> StaticPasswordDefined([FromBody] ResetStaticPasswordDto reset) => Ok(await _ServiceLaucherService.AccountManagerServices.StaticPasswordDefined(reset));

    // [HttpGet("IsPasswordExpiresAsync/{userId}")]
    // // public async Task<IActionResult> IsPasswordExpiresAsync(int userId) => Ok(await _ServiceLaucherService.AccountManagerServices.IsPasswordExpiresAsync(userId));

    [HttpPost("TimedAccessControlStartEndPostAsync")]
    public async Task<IActionResult> AcesseTimeIntervalSimpleAsync(TimedAccessControlStartEndPostDto timedAccessControl) => Ok(await _ServiceLaucherService.AccountManagerServices.TimedAccessControlStartEndUpdateAsync(timedAccessControl));

    [HttpGet("GetTimedAccessControlAsync/{userId:min(1)}")]
    public async Task<IActionResult> GetTimedAccessControlAsync(int userId) => Ok(await _ServiceLaucherService.AccountManagerServices.GetTimedAccessControlAsync(userId));
}
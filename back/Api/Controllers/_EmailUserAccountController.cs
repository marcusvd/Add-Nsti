using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Application.Helpers.ServicesLauncher;
using Application.Auth.UsersAccountsServices.EmailUsrAccountServices.dtos;

namespace Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/{controller}")]
public class _EmailUserAccountController : ControllerBase
{
    private readonly IServiceLaucherService _ServiceLaucherService;
    public _EmailUserAccountController(IServiceLaucherService ServiceLaucherService) { _ServiceLaucherService = ServiceLaucherService; }


    [HttpPost("FirstEmailConfirmationCheckTokenAsync")]
    public async Task<IActionResult> FirstEmailConfirmationCheckTokenAsync([FromBody] ConfirmEmailDto confirmEmail) => Ok(await _ServiceLaucherService.EmailUserAccountServices.FirstEmailConfirmationCheckTokenAsync(confirmEmail));

    [HttpPost("ConfirmEmailAddress")]
    public async Task<IActionResult> ConfirmEmailAddressAsync([FromBody] ConfirmEmailDto confirmEmail) => Ok(await _ServiceLaucherService.EmailUserAccountServices.ConfirmEmailAddressAsync(confirmEmail));

    // [HttpPost("ResendConfirmEmailAsync")]
    // public async Task<IActionResult> ResendConfirmEmailAsync([FromBody] ResendConfirmEmailViewModel request) => Ok(await _ServiceLaucherService.EmailUserAccountServices.SendConfirmEmailAsync(request.Email));

    [HttpPost("RequestEmailChange")]
    public async Task<IActionResult> RequestEmailChangeAsync([FromBody] RequestEmailChangeDto requestEmailChangeDto) => Ok(await _ServiceLaucherService.EmailUserAccountServices.RequestEmailChangeAsync(requestEmailChangeDto));

    [HttpPost("ConfirmRequestEmailChange")]
    public async Task<IActionResult> ConfirmRequestEmailChange([FromBody] ConfirmEmailChangeDto confirmRequestEmailChange) => Ok(await _ServiceLaucherService.EmailUserAccountServices.ConfirmYourEmailChangeAsync(confirmRequestEmailChange));

    [HttpPut("ManualConfirmEmailAddress")]
    public async Task<IActionResult> ManualConfirmEmailAddress([FromBody] EmailConfirmManualDto emailConfirmManual) => Ok(await _ServiceLaucherService.EmailUserAccountServices.ManualConfirmEmailAddress(emailConfirmManual));

}

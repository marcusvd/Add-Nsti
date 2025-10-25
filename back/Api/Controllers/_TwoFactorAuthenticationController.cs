using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Application.Helpers.ServicesLauncher;
using Application.Auth.TwoFactorAuthentication.Dtos;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "SYSADMIN, PENDING_AUTH_2FA")]
public class _TwoFactorAuthenticationController : ControllerBase
{

    private readonly IServiceLaucherService _serviceLaucherService;
    public _TwoFactorAuthenticationController(IServiceLaucherService serviceLaucherService)
    {
        _serviceLaucherService = serviceLaucherService;
    }

    [HttpPost("TwoFactorVerifyAsync")]
    public async Task<IActionResult> TwoFactorVerifyAsync([FromBody] VerifyTwoFactorRequestDto request)
    {
        return Ok(await _serviceLaucherService.TwoFactorAuthenticationServices.TwoFactorVerifyAsync(request));
    }

    [HttpPost("EnableAuthenticator")]
    [Authorize]
    public async Task<IActionResult> EnableAuthenticator([FromBody] ToggleAuthenticatorRequestDto request)
    {
        return Ok(await _serviceLaucherService.TwoFactorAuthenticationServices.EnableAuthenticatorAsync(request));
    }

    [HttpGet("GetAuthenticatorSetup")]
    [Authorize]
    public async Task<IActionResult> GetAuthenticatorSetup()
    {
        return Ok(await _serviceLaucherService.TwoFactorAuthenticationServices.GetAuthenticatorSetup());
    }

    [HttpPut("OnOff2FaCodeViaEmailAsync")]
    public async Task<IActionResult> OnOff2FaCodeViaEmailAsync([FromBody] OnOff2FaCodeViaEmailDto request)
    {
        return Ok(await _serviceLaucherService.TwoFactorAuthenticationServices.OnOff2FaCodeViaEmailAsync(request));
    }

}


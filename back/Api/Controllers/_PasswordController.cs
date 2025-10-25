using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using Application.Helpers.ServicesLauncher;

using AApplication.Auth.UsersAccountsServices.PasswordServices.Dtos;

namespace Api.Controllers;

[ApiController]
[Route("api/{controller}")]
[Authorize(Roles = "SYSADMIN")]
public class PasswordController : ControllerBase
{
    private readonly IServiceLaucherService _ServiceLaucherService;
    public PasswordController(IServiceLaucherService ServiceLaucherService) { _ServiceLaucherService = ServiceLaucherService; }
    public async Task<IActionResult> PasswordChangeAsync([FromBody] PasswordChangeDto passwordChange) =>
    Ok(await _ServiceLaucherService.PasswordServices.PasswordChangeAsync(passwordChange));

    public async Task<IActionResult> MarkPasswordExpireAsync([FromBody] PasswordWillExpiresDto passwordWillExpires) =>
     Ok(await _ServiceLaucherService.PasswordServices.MarkPasswordExpireAsync(passwordWillExpires));

    public async Task<IActionResult> StaticPasswordDefined([FromBody] ResetStaticPasswordDto reset) =>
    Ok(await _ServiceLaucherService.PasswordServices.SetStaticPassword(reset));
    
    public async Task<IActionResult> IsPasswordExpiresAsync(int userId) =>
    Ok(await _ServiceLaucherService.PasswordServices.IsPasswordExpiresAsync(userId));

}
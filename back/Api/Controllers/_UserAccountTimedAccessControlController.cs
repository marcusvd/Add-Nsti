using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using Application.Helpers.ServicesLauncher;
using Application.Auth.UsersAccountsServices.TimedAccessCtrlServices.Dtos;

namespace Api.Controllers;

[ApiController]
[Route("api/{controller}")]
[Authorize(Roles = "SYSADMIN")]
public class _UserAccountTimedAccessControlController : ControllerBase
{
    private readonly IServiceLaucherService _ServiceLaucherService;
    public _UserAccountTimedAccessControlController(IServiceLaucherService ServiceLaucherService) { _ServiceLaucherService = ServiceLaucherService; }

    [HttpPut("TimedAccessControlStartEndAsync/{id}")]
    public async Task<IActionResult> TimedAccessControlStartEndAsync(TimedAccessControlStartEndPostDto timedAccessControl) => Ok(await _ServiceLaucherService.UserAccountTimedAccessControlServices.TimedAccessControlStartEndUpdateAsync(timedAccessControl));

    [HttpGet("GetTimedAccessControlAsync/{userId:min(1)}")]
    public async Task<IActionResult> GetTimedAccessControlAsync(int userId) => Ok(await _ServiceLaucherService.UserAccountTimedAccessControlServices.GetTimedAccessControlAsync(userId));
}
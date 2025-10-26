using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Application.Helpers.ServicesLauncher;
using Application.Auth.Login.Dtos;

namespace Api.Controllers;

[ApiController]
[Authorize]
[Route("api/{controller}")]
public class _LoginController : ControllerBase
{
    private readonly IServiceLaucherService _ServiceLaucherService;
    public _LoginController(IServiceLaucherService ServiceLaucherService) { _ServiceLaucherService = ServiceLaucherService; }

    [HttpPost("LoginAsync")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginAsync([FromBody] LoginModelDto user) => Ok(await _ServiceLaucherService.LoginServices.LoginAsync(user));
 
    [HttpPost("LogoutAsync")]
    [AllowAnonymous]
    public async Task<IActionResult> LogoutAsync() => Ok(await _ServiceLaucherService.LoginServices.LogoutAsync());
 
}

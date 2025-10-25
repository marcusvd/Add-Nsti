using Microsoft.AspNetCore.Authorization;


using Domain.Entities.Authentication;
using Microsoft.AspNetCore.Mvc;

using Application.Helpers.ServicesLauncher;
using Application.Auth.Roles.Dtos;

namespace Api.Controllers;

[ApiController]
[Route("api/{controller}")]
[Authorize(Roles = "SYSADMIN")]
public class _RolesController : ControllerBase
{
    private readonly IServiceLaucherService _ServiceLaucherService;
    public _RolesController(IServiceLaucherService ServiceLaucherService) { _ServiceLaucherService = ServiceLaucherService; }

    [HttpPut("UpdateUserRole")]
    public async Task<IActionResult> UpdateUserRole([FromBody] UpdateUserRoleDto[] roles) => Ok(await _ServiceLaucherService.RolesServices.UpdateUserRoles(roles));

    [HttpGet("GetRoles")]
    public async Task<IActionResult> GetRolesAsync([FromBody] UserAccount userAccount) => Ok(await _ServiceLaucherService.RolesServices.GetRolesAsync(userAccount));

    [HttpPost("CreateRole")]
    public async Task<IActionResult> CreateRoleAsync([FromBody] RoleDto roleDto) => Ok(await _ServiceLaucherService.RolesServices.CreateRoleAsync(roleDto));

   
}
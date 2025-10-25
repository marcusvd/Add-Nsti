using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Application.Helpers.ServicesLauncher;
using  Application.Shared.Dtos;
using Application.Auth.Dtos;
using Application.Auth.Register.Dtos;


namespace Api.Controllers;

[ApiController]
[Authorize(Roles = "SYSADMIN")]
[Route("api/{controller}")]
public class _UserAccountsController : ControllerBase
{
    private readonly IServiceLaucherService _ServiceLaucherService;
    public _UserAccountsController(IServiceLaucherService ServiceLaucherService) { _ServiceLaucherService = ServiceLaucherService; }

    [HttpGet("GetUsersByCompanyIdAsync/{companyAuthId:min(1)}")]
    public async Task<IActionResult> GetUsersByCompanyIdAsync(int companyAuthId) => Ok(await _ServiceLaucherService.UserAccountServices.GetUsersByCompanyIdAsync(companyAuthId));

    [HttpGet("GetUserByIdFullAsync/{id:min(1)}")]
    public async Task<IActionResult> GetUserByIdFullAsync(int id) => Ok(await _ServiceLaucherService.UserAccountServices.GetUserByIdFullAsync(id));

    [HttpPut("AddUserAccountAsync/{companyId:min(1)}")]
    public async Task<IActionResult> AddUserAccountAsync([FromBody] AddUserExistingCompanyDto user, int companyId) => Ok(await _ServiceLaucherService.RegisterUserAccountServices.AddUserExistingCompanyAsync(user, companyId));

}

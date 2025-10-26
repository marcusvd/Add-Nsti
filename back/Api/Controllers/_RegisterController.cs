using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Application.Helpers.ServicesLauncher;
using Application.Auth.Login.Dtos;
using Application.Auth.Register.Dtos.FirstRegister;
using Application.Auth.Register.Dtos;

namespace Api.Controllers;

[ApiController]
[Authorize]
[Route("api/{controller}")]
public class _RegisterController : ControllerBase
{
    private readonly IServiceLaucherService _ServiceLaucherService;
    public _RegisterController(IServiceLaucherService ServiceLaucherService) { _ServiceLaucherService = ServiceLaucherService; }

    [HttpPost("RegisterAsync")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterModelDto user) => Ok(await _ServiceLaucherService.RegisterServices.RegisterAsync(user));

    [HttpPut("AddUserAccountAsync/{companyId:min(1)}")]
    public async Task<IActionResult> AddUserAccountAsync([FromBody] AddUserExistingCompanyDto user, int companyId) => Ok(await _ServiceLaucherService.RegisterUserAccountServices.AddUserExistingCompanyAsync(user, companyId));
}

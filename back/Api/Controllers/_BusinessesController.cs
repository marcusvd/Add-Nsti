using Microsoft.AspNetCore.Authorization;


using Microsoft.AspNetCore.Mvc;
using Application.Helpers.ServicesLauncher;


namespace Api.Controllers;

[ApiController]
[Route("api/{controller}")]
[Authorize(Roles = "SYSADMIN")]
public class _BusinessesController : ControllerBase
{
    private readonly IServiceLaucherService _ServiceLaucherService;
    public _BusinessesController(IServiceLaucherService ServiceLaucherService) { _ServiceLaucherService = ServiceLaucherService; }

    [HttpGet("GetBusinessFullAsync/{id:min(1)}")]
    public async Task<IActionResult> GetBusinessFullAsync(int id) => Ok(await _ServiceLaucherService.BusinessesAuthServices.GetBusinessFullAsync(id));

    [HttpGet("GetBusinessAsync/{id:min(1)}")]
    public async Task<IActionResult> GetBusinessAsync(int id) => Ok(await _ServiceLaucherService.BusinessesAuthServices.GetBusinessAsync(id));

    
}
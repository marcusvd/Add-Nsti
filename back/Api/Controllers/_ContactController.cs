using Application.Helpers.ServicesLauncher;
using Application.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/{controller}")]
 [Authorize(Roles = "SYSADMIN")]
public class _ContactController : ControllerBase
{
    private readonly IServiceLaucherService _ServiceLaucherService;
    public _ContactController(IServiceLaucherService ServiceLaucherService)
    { _ServiceLaucherService = ServiceLaucherService;}

    [HttpPut("UpdateContactAsync/{id:min(1)}")]
   
    public async Task<IActionResult> UpdateContactAsync([FromBody] ContactDto Contact, int id) => Ok(await _ServiceLaucherService.ContactServices.UpdateAsync(Contact, id));

}
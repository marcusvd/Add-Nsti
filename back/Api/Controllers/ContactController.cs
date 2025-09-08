using Application.Services.Helpers.ServicesLauncher;
using Application.Services.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/{controller}")]
public class ContactController : ControllerBase
{
    private readonly IServiceLaucherService _ServiceLaucherService;
    public ContactController(IServiceLaucherService ServiceLaucherService)
    { _ServiceLaucherService = ServiceLaucherService;}

    [HttpPut("UpdateContactAsync/{id:min(1)}")]
    [Authorize(Roles = "SYSADMIN")]
    public async Task<IActionResult> UpdateContactAsync([FromBody] ContactDto Contact, int id) => Ok(await _ServiceLaucherService.ContactServices.UpdateAsync(Contact, id));

}
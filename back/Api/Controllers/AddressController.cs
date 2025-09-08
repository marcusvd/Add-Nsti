using Application.Services.Helpers.ServicesLauncher;
using Application.Services.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/{controller}")]
public class AddressController : ControllerBase
{
    private readonly IServiceLaucherService _ServiceLaucherService;
    public AddressController(IServiceLaucherService ServiceLaucherService)
    { _ServiceLaucherService = ServiceLaucherService;}

    [HttpPut("UpdateAddressAsync/{id:min(1)}")]
    [Authorize(Roles = "SYSADMIN")]
    public async Task<IActionResult> UpdateAddressAsync([FromBody] AddressDto address, int id) => Ok(await _ServiceLaucherService.AddressServices.UpdateAsync(address, id));

}
using Application.Helpers.ServicesLauncher;
using Application.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/{controller}")]
 [Authorize(Roles = "SYSADMIN")]
public class _AddressController : ControllerBase
{
    private readonly IServiceLaucherService _ServiceLaucherService;
    public _AddressController(IServiceLaucherService ServiceLaucherService)
    { _ServiceLaucherService = ServiceLaucherService;}

    [HttpPut("UpdateAddressAsync/{id:min(1)}")]
    public async Task<IActionResult> UpdateAddressAsync([FromBody] AddressDto address, int id) => Ok(await _ServiceLaucherService.AddressServices.UpdateAsync(address, id));

}
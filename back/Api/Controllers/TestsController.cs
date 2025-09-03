using Authentication.Helpers;
using Domain.Entities.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImApi.Controllers;

[ApiController]
[Route("api/{controller}")]
[AllowAnonymous]
public class TestControlController : ControllerBase
{

    private readonly AuthGenericValidatorServices _genericValidatorServices;
    public TestControlController(

        AuthGenericValidatorServices genericValidatorServices
    )
    {
        _genericValidatorServices = genericValidatorServices;

    }

    [HttpGet("Tests")]
    public async Task<IActionResult> Tests()
    {
        var result = (CompanyAuth)_genericValidatorServices.ReplaceNullObj<CompanyAuth>();

        return Ok(result);
    }

}

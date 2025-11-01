using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Application.Helpers.ServicesLauncher;
using Application.Auth.Register.Dtos.FirstRegister;
using Application.Auth.Register.Dtos;
using Application.Helpers.Tools.CpfCnpj;
using Application.Helpers.Tools.Cnpj;
using Application.Helpers.Tools.ZipCode;

namespace Api.Controllers;

[ApiController]
[Route("api/{controller}")]
public class _RegisterController : ControllerBase
{
    private readonly IServiceLaucherService _ServiceLaucherService;
    public _RegisterController(IServiceLaucherService ServiceLaucherService) { _ServiceLaucherService = ServiceLaucherService; }

    [AllowAnonymous]
    [HttpPost("FirstConfirmEmailRegisterAsync")]
    public async Task<IActionResult> FirstConfirmEmailRegisterAsync([FromBody] FirstConfirmEmailRegisterDto dto) =>
    Ok(await _ServiceLaucherService.RegisterServices.FirstEmailConfirmation(dto.Email));
    
    
    [HttpPost("RegisterAsync")]
    public async Task<IActionResult> RegisterAsync(
        [FromBody] RegisterModelDto request,
        [FromServices]ICpfCnpjGetDataServices cpfCnpjGetDataServices,
        [FromServices]IZipCodeGetDataServices zipCodeGetDataServices,
        [FromServices]IPhoneNumberValidateServices phoneNumberValidateServices
        ) => Ok(await _ServiceLaucherService.RegisterServices.RegisterAsync(request,cpfCnpjGetDataServices,zipCodeGetDataServices,phoneNumberValidateServices));

    [Authorize]
    [HttpPut("AddUserAccountAsync/{companyId:min(1)}")]
    public async Task<IActionResult> AddUserAccountAsync([FromBody] AddUserExistingCompanyDto user, int companyId) => Ok(await _ServiceLaucherService.RegisterUserAccountServices.AddUserExistingCompanyAsync(user, companyId));
}

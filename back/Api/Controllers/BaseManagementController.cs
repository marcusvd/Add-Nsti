using Authentication.Operations.AuthAdm;
using Application.Services.Operations.Account;
using Application.Services.Operations.Auth.Login;
using Application.Services.Operations.Auth.Register;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities.Authentication;
using Microsoft.AspNetCore.Authorization;
using Application.Services.Operations.Auth.Dtos;


namespace Api.Controllers;

[ApiController]
[Route("api/{controller}")]
public class BaseManagementController : ControllerBase
{

    private readonly ILoginServices _iLoginServices;
    private readonly IRegisterServices _iRegisterServices;
    private readonly IAccountManagerServices _iAccountManagerServices;
    private readonly IAuthAdmServices _authAdmServices;

    public BaseManagementController(

        ILoginServices iLoginServices,
        IRegisterServices iRegisterServices,
        IAccountManagerServices iAccountManagerServices,
        IAuthAdmServices authAdmServices
        )
    {
        _iLoginServices = iLoginServices;
        _iRegisterServices = iRegisterServices;
        _iAccountManagerServices = iAccountManagerServices;
        _authAdmServices = authAdmServices;
    }


    [HttpPut("UpdateBusinessAddCompanyAsync/{id:min(1)}")]
    [Authorize(Roles = "SYSADMIN")]
    public async Task<IActionResult> UpdateBusinessAddCompanyAsync([FromBody] BusinessAuthUpdateAddCompanyDto businessAuthUpdateAddCompanyDto, int id)
    {
        return Ok(await _authAdmServices.UpdateBusinessAuthAndProfileAsync(businessAuthUpdateAddCompanyDto, id));
    }

}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Application.Helpers.ServicesLauncher;

using Application.CompaniesServices.Dtos;
using Application.BusinessesServices.Dtos.UpdateBusinessAddNewCompany;
using Application.Auth.Login.Dtos;

namespace Api.Controllers;

[ApiController]
[Route("api/{controller}")]
public class _CompaniesController : ControllerBase
{
    private readonly IServiceLaucherService _ServiceLaucherService;
    public _CompaniesController(IServiceLaucherService ServiceLaucherService) { _ServiceLaucherService = ServiceLaucherService; }


    [HttpPut("AddCompanyAsync/{businessId:min(1)}")]
    [Authorize(Roles = "SYSADMIN")]
    public async Task<IActionResult> AddCompanyAsync([FromBody] PushCompanyDto pushCompany, int businessId) => Ok(await _ServiceLaucherService.CompanyServices.AddCompanyAsync(pushCompany, businessId));

    [HttpPut("UpdateCompany_Auth_Profile")]
    [Authorize(Roles = "SYSADMIN")]
    public async Task<IActionResult> UpdateCompany_Auth_Profile([FromBody] Update_Auth_ProfileDto updateCompany_Auth_Profile) => Ok(await _ServiceLaucherService.CompanyServices.UpdateCompany_Auth_Profile(updateCompany_Auth_Profile));

    [HttpGet("GetCompaniesUserMemberAsync")]
    [Authorize]
    public async Task<IActionResult> GetCompaniesUserMemberAsync([FromBody] LoginModelDto user) => Ok(await _ServiceLaucherService.LoginServices.LoginAsync(user));

    [HttpGet("GetCompanyAuthAsync/{id:min(1)}")]
    public async Task<IActionResult> GetCompanyAuthAsync(int id) => Ok(await _ServiceLaucherService.CompanyAuthServices.GetCompanyAuthAsync(id));

    [HttpGet("GetCompanyAuthFullAsync/{id:min(1)}")]
    public async Task<IActionResult> GetCompanyAuthFullAsync(int id) => Ok(await _ServiceLaucherService.CompanyAuthServices.GetCompanyAuthFullAsync(id));

    [HttpGet("GetCompanyProfileAsync/{companyAuthId}")]
    public async Task<IActionResult> GetCompanyProfileAsync(string companyAuthId) => Ok(await _ServiceLaucherService.CompanyProfileServices.GetCompanyProfileFullAsync(companyAuthId));
    
    [HttpGet("GetCompaniesByUserIdAsync/{userId}")]
    public async Task<IActionResult> GetCompaniesByUserIdAsync(int userId) => Ok(await _ServiceLaucherService.CompanyAuthServices.GetCompaniesByUserIdAsync(userId));
   
    [HttpGet("GetAmountCompaniesByUserIdAsync/{userId}")]
    public async Task<IActionResult> GetAmountCompaniesByUserIdAsync(int userId) => Ok(await _ServiceLaucherService.CompanyAuthServices.GetAmountCompaniesByUserIdAsync(userId));
}

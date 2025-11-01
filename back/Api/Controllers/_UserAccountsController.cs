using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Application.Helpers.ServicesLauncher;
using Application.Shared.Dtos;
using Application.Auth.Dtos;
using Application.Auth.Register.Dtos;
using Application.UsersAccountsServices.Dtos;
using Application.Auth.UsersAccountsServices.Dtos;


namespace Api.Controllers;

[ApiController]
[Authorize(Roles = "SYSADMIN")]
[Route("api/{controller}")]
public class _UserAccountsController : ControllerBase
{
    private readonly IServiceLaucherService _ServiceLaucherService;
    public _UserAccountsController(IServiceLaucherService ServiceLaucherService) { _ServiceLaucherService = ServiceLaucherService; }

    [HttpGet("GetUsersByCompanyIdAsync/{companyAuthId:min(1)}")]
    public async Task<IActionResult> GetUsersByCompanyIdAsync(int companyAuthId) => Ok(await _ServiceLaucherService.UserAccountServices.GetUsersByCompanyIdAsync(companyAuthId));

    [HttpGet("GetUserByIdFullAsync/{id:min(1)}")]
    public async Task<IActionResult> GetUserByIdFullAsync(int id) => Ok(await _ServiceLaucherService.UserAccountServices.GetUserByIdFullAsync(id));

    [HttpGet("GetAccountStatus/{email}")]
    public async Task<IActionResult> GetAccountStatus(string email)
    {
        var emailConfirmed = await _ServiceLaucherService.EmailUserAccountServices.IsEmailConfirmedAsync(email);

        var accountLockedOut = await _ServiceLaucherService.UserAccountAuthServices.IsAccountLockedOut(email);

        AccountStatusDto result = new() { IsEmailConfirmed = emailConfirmed, IsAccountLockedOut = accountLockedOut };

        return Ok(result);
    }
    [AllowAnonymous]
    [HttpGet("IsUserExistCheckByEmailAsync/{email}")]
    public async Task<IActionResult> IsUserExistCheckByEmailAsync(string email) => Ok(await _ServiceLaucherService.UserAccountAuthServices.IsUserExistCheckByEmailAsync(email));

    [HttpPut("UpdateUserAccountAuthAsync/{id:min(1)}")]
    public async Task<IActionResult> UpdateUserAccountAuthAsync([FromBody] UserAccountDto userAccountUpdate, int id) => Ok(await _ServiceLaucherService.UserAccountAuthServices.UpdateUserAccountAuthAsync(userAccountUpdate, id));

    [HttpPut("UpdateUserAccountProfileAsync/{id:min(1)}")]
    public async Task<IActionResult> UpdateUserAccountProfileAsync([FromBody] UserProfileDto userAccountUpdate, int id) => Ok(await _ServiceLaucherService.UserAccountProfileServices.UpdateUserAccountProfileAsync(userAccountUpdate, id));

    [HttpPut("ManualAccountLockedOut")]
    public async Task<IActionResult> ManualAccountLockedOut([FromBody] AccountLockedOutManualDto AccountLockedOutManual) => Ok(await _ServiceLaucherService.UserAccountAuthServices.ManualAccountLockedOut(AccountLockedOutManual));


}

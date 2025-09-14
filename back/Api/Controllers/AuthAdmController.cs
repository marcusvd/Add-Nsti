using Authentication.Operations.AuthAdm;
using Application.Services.Operations.Account;
using Domain.Entities.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Services.Operations.Auth.Dtos;
using Application.Services.Operations.Auth.CompanyAuthServices;
using Application.Services.Operations.Auth.Register;
using Application.Exceptions;
using Application.Services.Helpers.ServicesLauncher;



namespace Api.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    [Authorize(Roles = "SYSADMIN")]
    public class AuthAdmController : ControllerBase
    {
        private readonly IServiceLaucherService _ServiceLaucherService;
        // private readonly IAccountManagerServices _iAccountManagerServices;
        // private readonly IAuthAdmServices _authAdmServices;
        // private readonly ICompanyAuthServices _companyAuthServices;
        // private readonly IRegisterUserAccountServices _registerUserAccountServices;

        public AuthAdmController(
            IServiceLaucherService ServiceLaucherService
            // IAccountManagerServices iAccountManagerServices,
            // IAuthAdmServices authAdmServices,
            //  ICompanyAuthServices companyAuthServices,
            //  IRegisterUserAccountServices registerUserAccountServices
            )
        {
            _ServiceLaucherService = ServiceLaucherService;
            // _iAccountManagerServices = iAccountManagerServices;
            // _authAdmServices = authAdmServices;
            // _companyAuthServices = companyAuthServices;
            // _registerUserAccountServices = registerUserAccountServices;
        }


        [HttpGet("GetBusinessFullAsync/{id:min(1)}")]
        // [Authorize(Roles = "SYSADMIN")]
        // [AllowAnonymous]
        public async Task<IActionResult> GetBusinessFullAsync(int id)
        {
            return Ok(await _ServiceLaucherService.AuthAdmServices.GetBusinessFullAsync(id));

        }

        [HttpGet("GetBusinessAsync/{id:min(1)}")]
        // [Authorize(Roles = "SYSADMIN")]
        public async Task<IActionResult> GetBusinessAsync(int id)
        {
            return Ok(await _ServiceLaucherService.AuthAdmServices.GetBusinessAsync(id));

        }

        [HttpGet("GetCompanyAuthAsync/{id:min(1)}")]
        // [Authorize(Roles = "SYSADMIN")]
        public async Task<IActionResult> GetCompanyAuthAsync(int id)
        {
            return Ok(await _ServiceLaucherService.CompanyAuthServices.GetCompanyAuthAsync(id));

        }
        [HttpGet("GetCompanyAuthFullAsync/{id:min(1)}")]
        // [Authorize(Roles = "SYSADMIN")]
        public async Task<IActionResult> GetCompanyAuthFullAsync(int id)
        {
            return Ok(await _ServiceLaucherService.CompanyAuthServices.GetCompanyAuthFullAsync(id));

        }
        [HttpGet("GetUsersByCompanyIdAsync/{companyAuthId:min(1)}")]
        // [AllowAnonymous]
        // [Authorize(Roles = "SYSADMIN")]
        public async Task<IActionResult> GetUsersByCompanyIdAsync(int companyAuthId)
        {
            return Ok(await _ServiceLaucherService.CompanyAuthServices.GetUsersByCompanyIdAsync(companyAuthId));
        }
        [HttpGet("GetUserByIdFullAsync/{id:min(1)}")]
        // [AllowAnonymous]
        // [Authorize(Roles = "SYSADMIN")]
        public async Task<IActionResult> GetUserByIdFullAsync(int id)
        {

            return Ok(await _ServiceLaucherService.CompanyAuthServices.GetUserByIdFullAsync(id));
        }

        [HttpGet("GetCompanyProfileAsync/{companyAuthId}")]
        // [AllowAnonymous]
        // [Authorize(Roles = "SYSADMIN")]
        public async Task<IActionResult> GetCompanyProfileAsync(string companyAuthId)
        {
            if (string.IsNullOrWhiteSpace(companyAuthId)) throw new Exception(GlobalErrorsMessagesException.IvalidId);

            return Ok(await _ServiceLaucherService.CompanyAuthServices.GetCompanyProfileFullAsync(companyAuthId));
        }

        [HttpPut("UpdateUserRole")]
        // [Authorize(Roles = "SYSADMIN")]
        public async Task<IActionResult> UpdateUserRole([FromBody] UpdateUserRoleDto role)
        {
            return Ok(await _ServiceLaucherService.AccountManagerServices.UpdateUserRoles(role));

        }

        [HttpGet("GetRoles")]
        // [Authorize(Roles = "SYSADMIN")]
        public async Task<IActionResult> GetRolesAsync([FromBody] UserAccount userAccount)
        {
            return Ok(await _ServiceLaucherService.AccountManagerServices.GetRolesAsync(userAccount));
        }

        [HttpPost("CreateRole")]
        // [Authorize(Roles = "SYSADMIN")]
        public async Task<IActionResult> CreateRoleAsync([FromBody] RoleDto roleDto)
        {
            return Ok(await _ServiceLaucherService.AccountManagerServices.CreateRoleAsync(roleDto));
        }

        [HttpPut("AddUserAccountAsync/{companyId:min(1)}")]
        // [Authorize(Roles = "SYSADMIN")]
        public async Task<IActionResult> AddUserAccountAsync([FromBody] AddUserExistingCompanyDto user, int companyId)
        {
            return Ok(await _ServiceLaucherService.RegisterUserAccountServices.AddUserExistingCompanyAsync(user, companyId));

        }

        [HttpPost("PasswordChangeAsync")]
        public async Task<IActionResult> PasswordChangeAsync([FromBody] PasswordChangeDto passwordChange)
        {
            return Ok(await _ServiceLaucherService.AccountManagerServices.PasswordChangeAsync(passwordChange));
        }

        [HttpGet("GetAccountStatus/{email}")]
        // [Authorize(Roles = "SYSADMIN")]
        public async Task<IActionResult> GetAccountStatus(string email)
        {
            var emailConfirmed = await _ServiceLaucherService.AccountManagerServices.IsEmailConfirmedAsync(email);
            var accountLockedOut = await _ServiceLaucherService.AccountManagerServices.IsAccountLockedOut(email);

            AccountStatusDto result = new() { IsEmailConfirmed = emailConfirmed, IsAccountLockedOut = accountLockedOut };

            return Ok(result);
        }

        [HttpPut("ManualConfirmEmailAddress")]
        public async Task<IActionResult> ManualConfirmEmailAddress([FromBody] EmailConfirmManualDto emailConfirmManual)
        {
            var result = await _ServiceLaucherService.AccountManagerServices.ManualConfirmEmailAddress(emailConfirmManual);

            return Ok(result);
        }

        [HttpPut("ManualAccountLockedOut")]
        public async Task<IActionResult> ManualAccountLockedOut([FromBody] AccountLockedOutManualDto AccountLockedOutManual)
        {
            var result = await _ServiceLaucherService.AccountManagerServices.ManualAccountLockedOut(AccountLockedOutManual);

            return Ok(result);
        }

        [HttpPut("MarkPasswordExpireAsync")]
        public async Task<IActionResult> MarkPasswordExpireAsync([FromBody] PasswordWillExpiresDto passwordWillExpires)
        {
            var result = await _ServiceLaucherService.AccountManagerServices.MarkPasswordExpireAsync(passwordWillExpires);

            return Ok(result);
        }

        [HttpPut("StaticPasswordDefined")]
        public async Task<IActionResult> StaticPasswordDefined([FromBody] ResetStaticPasswordDto reset)
        {
            var result = await _ServiceLaucherService.AccountManagerServices.StaticPasswordDefined(reset);

            return Ok(result);
        }

        [HttpGet("IsPasswordExpiresAsync/{userId}")]
        public async Task<IActionResult> IsPasswordExpiresAsync(int userId)
        {
            var result = await _ServiceLaucherService.AccountManagerServices.IsPasswordExpiresAsync(userId);

            return Ok(result);
        }
        // [HttpGet("AcesseTimeIntervalStartEndAsync")]
        // public async Task<IActionResult> AcesseTimeIntervalSimpleAsync()
        // {
        //     var result = await _ServiceLaucherService.AccountManagerServices.IsPasswordExpiresAsync(userId);

        //     return Ok(result);
        // }


    }
}

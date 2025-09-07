using Authentication.Operations.AuthAdm;
using Application.Services.Operations.Account;
using Domain.Entities.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Services.Operations.Auth.Dtos;
using Application.Services.Operations.Auth.CompanyAuthServices;
using Application.Services.Operations.Auth.Register;
using Application.Exceptions;



namespace Api.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class AuthAdmController : ControllerBase
    {
        private readonly IAccountManagerServices _iAccountManagerServices;
        private readonly IAuthAdmServices _authAdmServices;
        private readonly ICompanyAuthServices _companyAuthServices;
        private readonly IRegisterUserAccountServices _registerUserAccountServices;

        public AuthAdmController(
            IAccountManagerServices iAccountManagerServices,
            IAuthAdmServices authAdmServices,
             ICompanyAuthServices companyAuthServices,
             IRegisterUserAccountServices registerUserAccountServices
            )
        {
            _iAccountManagerServices = iAccountManagerServices;
            _authAdmServices = authAdmServices;
            _companyAuthServices = companyAuthServices;
            _registerUserAccountServices = registerUserAccountServices;
        }


        [HttpGet("GetBusinessFullAsync/{id:min(1)}")]
        [Authorize(Roles = "SYSADMIN")]
        // [AllowAnonymous]
        public async Task<IActionResult> GetBusinessFullAsync(int id)
        {
            return Ok(await _authAdmServices.GetBusinessFullAsync(id));

        }

        [HttpGet("GetBusinessAsync/{id:min(1)}")]
        [Authorize(Roles = "SYSADMIN")]
        public async Task<IActionResult> GetBusinessAsync(int id)
        {
            return Ok(await _authAdmServices.GetBusinessAsync(id));

        }

        [HttpGet("GetCompanyAuthAsync/{id:min(1)}")]
        [Authorize(Roles = "SYSADMIN")]
        public async Task<IActionResult> GetCompanyAuthAsync(int id)
        {
            return Ok(await _companyAuthServices.GetCompanyAuthAsync(id));

        }
        [HttpGet("GetCompanyAuthFullAsync/{id:min(1)}")]
        [Authorize(Roles = "SYSADMIN")]
        public async Task<IActionResult> GetCompanyAuthFullAsync(int id)
        {
            return Ok(await _companyAuthServices.GetCompanyAuthFullAsync(id));

        }

        [HttpGet("GetCompanyProfileAsync/{companyAuthId}")]
        // [AllowAnonymous]
        [Authorize(Roles = "SYSADMIN")]
        public async Task<IActionResult> GetCompanyProfileAsync(string companyAuthId)
        {
            if (string.IsNullOrWhiteSpace(companyAuthId)) throw new Exception(GlobalErrorsMessagesException.IvalidId);

            return Ok(await _companyAuthServices.GetCompanyProfileFullAsync(companyAuthId));
        }

        [HttpPut("UpdateUserRole")]
        [Authorize(Roles = "SYSADMIN")]
        public async Task<IActionResult> UpdateUserRole([FromBody] UpdateUserRole role)
        {
            return Ok(await _iAccountManagerServices.UpdateUserRoles(role));

        }

        [HttpGet("GetRoles")]
        [Authorize(Roles = "SYSADMIN")]
        public async Task<IActionResult> GetRolesAsync([FromBody] UserAccount userAccount)
        {
            return Ok(await _iAccountManagerServices.GetRolesAsync(userAccount));
        }

        [HttpPost("CreateRole")]
        [Authorize(Roles = "SYSADMIN")]
        public async Task<IActionResult> CreateRoleAsync([FromBody] RoleDto roleDto)
        {
            return Ok(await _iAccountManagerServices.CreateRoleAsync(roleDto));
        }

        [HttpPut("AddUserAccountAsync/{companyId:min(1)}")]
        [Authorize(Roles = "SYSADMIN")]
        public async Task<IActionResult> AddUserAccountAsync([FromBody] AddUserExistingCompanyDto user, int companyId)
        {
            return Ok(await _registerUserAccountServices.AddUserExistingCompanyAsync(user, companyId));

        }
        
        
        [HttpGet("GetUsersByCompanyIdAsync/{companyAuthId:min(1)}")]
        // [AllowAnonymous]
        [Authorize(Roles = "SYSADMIN")]
        public async Task<IActionResult> GetUsersByCompanyIdAsync(int companyAuthId)
        {
            return Ok(await _companyAuthServices.GetUsersByCompanyIdAsync(companyAuthId));
        }
        [HttpGet("GetUserByIdFullAsync/{id:min(1)}")]
        [AllowAnonymous]
        // [Authorize(Roles = "SYSADMIN")]
        public async Task<IActionResult> GetUserByIdFullAsync(int id)
        {

            return Ok(await _companyAuthServices.GetUserByIdFullAsync(id));
        }



    }
}

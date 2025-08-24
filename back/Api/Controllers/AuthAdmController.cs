using Authentication.Operations.AuthAdm;
using Application.Services.Operations.Account;
using Domain.Entities.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Services.Operations.Auth.Dtos;
using Application.Services.Operations.Auth.CompanyAuthServices;


namespace Api.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class AuthAdmController : ControllerBase
    {

        // private readonly ILoginServices _iLoginServices;
        // private readonly IRegisterServices _iRegisterServices;
        private readonly IAccountManagerServices _iAccountManagerServices;
        private readonly IAuthAdmServices _authAdmServices;
        private readonly ICompanyAuthServices _companyAuthServices;

        public AuthAdmController(

            // ILoginServices iLoginServices,
            // IRegisterServices iRegisterServices,
            IAccountManagerServices iAccountManagerServices,
            IAuthAdmServices authAdmServices,
             ICompanyAuthServices companyAuthServices
            )
        {
            // _iLoginServices = iLoginServices;
            // _iRegisterServices = iRegisterServices;
            _iAccountManagerServices = iAccountManagerServices;
            _authAdmServices = authAdmServices;
            _companyAuthServices = companyAuthServices;
        }


        [HttpGet("GetBusinessFullAsync/{id:min(1)}")]
        [Authorize(Roles = "SYSADMIN")]
        public async Task<IActionResult> GetBusinessFullAsync(int id)
        {
            return Ok(await _authAdmServices.BusinessAsync(id));

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

    

    }
}

using Authentication.Operations.AuthAdm;
using Authentication.Operations.Account;
using Authentication.Operations.Login;
using Authentication.Operations.Register;
// using Authentication.Operations.UrlGenerator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Authentication.Entities;


namespace Api.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/{controller}")]
    public class AuthAdmController : ControllerBase
    {

        private readonly ILoginServices _iLoginServices;
        private readonly IRegisterServices _iRegisterServices;
        private readonly IAccountManagerServices _iAccountManagerServices;
        private readonly IAuthAdmServices _authAdmServices;

        public AuthAdmController(

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


        [HttpGet("GetBusinessFullAsync/{id}")]
        public async Task<IActionResult> GetBusinessFullAsync(int id)
        {
            var result = await _authAdmServices.BusinessAsync(id);

            return Ok(result);
        }

        [HttpPut("UpdateUserRole")]
        public async Task<IActionResult> UpdateUserRole([FromBody] UpdateUserRole role)
        {
            var result = await _iAccountManagerServices.UpdateUserRoles(role);
            return Ok(result);
        }


    }
}

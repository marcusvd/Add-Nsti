// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Application.Services.Operations.Authentication.Dtos;
// using Application.Services.Operations.Authentication.Login;
// using Application.Services.Operations.Authentication.Register;

// namespace Api.Controllers
// {
//     [ApiController]
//     [AllowAnonymous]
//     [Route("api/{controller}")]
//     public class AuthController : ControllerBase
//     {
 
//         private readonly ILoginServices _iLoginServices;
//         private readonly IRegisterServices _iRegisterServices;

//         public AuthController(
         
//             ILoginServices iLoginServices,
//             IRegisterServices iRegisterServices
//             )
//         {
//             _iLoginServices = iLoginServices;
//             _iRegisterServices = iRegisterServices;
//         }

//         [HttpPost("RegisterAsync")]
//         public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto user)
//         {
//             var result = await _iRegisterServices.RegisterAsync(user);

//             return Ok(result);
//         }

//         [HttpPost("login")]
//         public async Task<IActionResult> Login([FromBody] LoginDto user)
//         {
//             var result = await _iLoginServices.Login(user);
          
//             return Ok(result);
//         }
//     }
// }

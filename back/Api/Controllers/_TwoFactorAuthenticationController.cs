using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Application.Services.Helpers.ServicesLauncher;
using Application.Services.Operations.Auth.Dtos;
using Domain.Entities.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// namespace Api.Controllers;

// [ApiController]
// [Route("api/{controller}")]
// public class _TwoFactorAuthenticationController : ControllerBase
// {
//     private readonly IServiceLaucherService _ServiceLaucherService;
//     private readonly UserManager<UserAccount> _userManager;
//     private readonly UserClaimsPrincipalFactory<UserAccount> _userClaimsPrincipalFactory;
//     public _TwoFactorAuthenticationController(
//         IServiceLaucherService ServiceLaucherService, UserManager<UserAccount> userManager,
//         UserClaimsPrincipalFactory<UserAccount> userClaimsPrincipalFactory
//         )
//     {
//         _ServiceLaucherService = ServiceLaucherService;
//         _userManager = userManager;
//         _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
//     }



///test
// [HttpPut("TwoFactorToggleAsync")]
// public async Task<IActionResult> TwoFactorToggleAsync([FromBody] TwoFactorToggleViewModel toggleTwoFactor)
// {
//     var result = await _ServiceLaucherService.TwoFactorAuthenticationServices.TwoFactorToggleAsync(toggleTwoFactor);

//     return Ok(result);
// }

// [HttpGet("GetTwoFactorStatus/{userId:min(1)}")]
// public async Task<IActionResult> GetTwoFactorStatus(int userId)
// {
//     var result = await _ServiceLaucherService.TwoFactorAuthenticationServices.GetTwoFactorStatus(userId);

//     return Ok(result);
// }

//test


// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.WebUtilities;
// using System.Text;
// using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authorization;
using UnitOfWork.Persistence.Operations;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class _TwoFactorAuthenticationController : ControllerBase
{
    private readonly UserManager<UserAccount> _userManager;
    private readonly SignInManager<UserAccount> _signInManager;
    private readonly ILogger<_TwoFactorAuthenticationController> _logger;
    private readonly IAuthServicesInjection _AUTH_SERVICES_INJECTION;
    public _TwoFactorAuthenticationController(
        UserManager<UserAccount> userManager,
        SignInManager<UserAccount> signInManager,
        ILogger<_TwoFactorAuthenticationController> logger,
        IAuthServicesInjection AUTH_SERVICES_INJECTION
        )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
        _AUTH_SERVICES_INJECTION = AUTH_SERVICES_INJECTION;
    }

    // POST: api/Account/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {


        var result = await _signInManager.PasswordSignInAsync(
            request.Email, request.Password, request.RememberMe, lockoutOnFailure: true);

        if (result.Succeeded)
        {
            _logger.LogInformation("Usuário logado: {Email}", request.Email);

            var user = await _userManager.FindByEmailAsync(request.Email);
            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new ApiResponse<LoginResponse>
            {
                Success = true,
                Message = "Login realizado com sucesso",
                Data = new LoginResponse
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Roles = roles,
                    RequiresTwoFactor = false
                }
            });
        }

        if (result.RequiresTwoFactor)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            var providers = await _userManager.GetValidTwoFactorProvidersAsync(user);

            // Gera token e envia via email (exemplo com email)
            var token = await _userManager.GenerateTwoFactorTokenAsync(user, providers.First());

            // Envia o token por email
            // await _emailSender.SendTwoFactorTokenAsync(user.Email, token);

            return Ok(new ApiResponse<LoginResponse>
            {
                Success = true,
                Message = "Verificação em duas etapas necessária",
                Data = new LoginResponse
                {
                    UserId = user.Id,
                    Email = user.Email,
                    RequiresTwoFactor = true,
                    TwoFactorProviders = providers
                }
            });
        }

        if (result.IsLockedOut)
        {
            _logger.LogWarning("Conta bloqueada: {Email}", request.Email);
            return Unauthorized(new ApiResponse<string>
            {
                Success = false,
                Message = "Conta temporariamente bloqueada"
            });
        }

        return Unauthorized(new ApiResponse<string>
        {
            Success = false,
            Message = "Email ou senha inválidos"
        });
    }

    // POST: api/Account/verify-two-factor
    [HttpPost("twofactorverify")]
    public async Task<IActionResult> twofactorverify([FromBody] VerifyTwoFactorRequestViewModel request)
    {
        var userAccount = await _userManager.FindByEmailAsync(request.Email);
        if (userAccount == null)
        {
            return BadRequest(new ApiResponse<string>
            {
                Success = false,
                Message = "Usuário não encontrado"
            });
        }


        var appAuthToken = await _userManager.VerifyTwoFactorTokenAsync(
           userAccount, _userManager.Options.Tokens.AuthenticatorTokenProvider, request.Token);

        var emailAuthToken = await _userManager.VerifyTwoFactorTokenAsync(
            userAccount, request.Provider, request.Token);


        if (!emailAuthToken && !appAuthToken)
        {
            return BadRequest(new ApiResponse<string>
            {
                Success = false,
                Message = "Token inválido"
            });
        }

        // Realiza o login com 2FA
       ;
        // var result = await _signInManager.TwoFactorSignInAsync(
        //     request.Provider, request.Token, request.RememberMe, rememberClient: false);


        if (await _userManager.IsLockedOutAsync(userAccount))
        {
            return Unauthorized(new ApiResponse<string>
            {
                Success = false,
                Message = "Conta bloqueada"
            });
        }


        await _signInManager.SignInAsync(userAccount, request.RememberMe);
        _logger.LogInformation("Login com 2FA realizado: {Email}", userAccount.Email);

        var roles = await _userManager.GetRolesAsync(userAccount);



        return Ok(new ApiResponse<UserToken>
        {
            Success = true,
            Message = "Login realizado com sucesso",
            Data = await CreateAuthenticationResponseAsync(userAccount)
        });

    }

    private protected async Task<UserToken> CreateAuthenticationResponseAsync(UserAccount userAccount)
    {
        var claimsList = await BClaims(userAccount);
        var roles = await _userManager.GetRolesAsync(userAccount);
        var token = await _AUTH_SERVICES_INJECTION.JwtHandler.GenerateUserToken(claimsList.Claims.ToList(), userAccount, roles);
        return token;
    }

    private async Task<ClaimsPrincipal> BClaims(UserAccount userAccount)
    {
        var getRoles = await _userManager.GetRolesAsync(userAccount);

        var claims = new ClaimsIdentity(IdentityConstants.TwoFactorUserIdScheme);



        claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, userAccount.Id.ToString()));
        claims.AddClaim(new Claim("amr", "Email"));
        // identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId));
        //  new Claim(ClaimTypes.NameIdentifier, userAccount.Id.ToString()),
        //   new Claim(ClaimTypes.Name, userAccount.UserName!),
        // new Claim(ClaimTypes.Name, userAccount.Email!),


        foreach (var role in getRoles)
            claims.AddClaim(new Claim(ClaimTypes.Role, role));


        return new ClaimsPrincipal(claims);
    }

    //todo

    // POST: api/Account/enable-authenticator
    [HttpPost("EnableAuthenticator")]
    [Authorize] // Requer autenticação
    public async Task<IActionResult> EnableAuthenticator([FromBody] ToggleAuthenticatorRequestViewModel request)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized(new ApiResponse<string>
            {
                Success = false,
                Message = "Usuário não autenticado"
            });
        }

        var verificationCode = request.Code.Replace(" ", "").Replace("-", "");
        var isValid = await _userManager.VerifyTwoFactorTokenAsync(
            user, _userManager.Options.Tokens.AuthenticatorTokenProvider, verificationCode);

        if (!isValid)
        {
            return BadRequest(new ApiResponse<string>
            {
                Success = false,
                Message = "Código inválido"
            });
        }
        

        await _userManager.SetTwoFactorEnabledAsync(user, request.Enabled);
        _logger.LogInformation("2FA habilitado para usuário: {UserId}", user.Id);

        // Gera códigos de recuperação
        var recoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);

        return Ok(new ApiResponse<EnableAuthenticatorResponse>
        {
            Success = true,
            Message = "Autenticador habilitado com sucesso",
            Data = new EnableAuthenticatorResponse
            {
                RecoveryCodes = recoveryCodes.ToArray(),
                IsTwoFactorEnabled = true
            }
        });
    }

    // GET: api/Account/authenticator-setup
    [HttpGet("GetAuthenticatorSetup")]
    [Authorize]
    public async Task<IActionResult> GetAuthenticatorSetup()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized();
        }

        var authenticatorKey = await _userManager.GetAuthenticatorKeyAsync(user);
        if (string.IsNullOrEmpty(authenticatorKey))
        {
            await _userManager.ResetAuthenticatorKeyAsync(user);
            authenticatorKey = await _userManager.GetAuthenticatorKeyAsync(user);
        }

        var model = new AuthenticatorSetupResponse
        {
            SharedKey = FormatKey(authenticatorKey),
            AuthenticatorUri = GenerateQrCodeUri(user.Email, authenticatorKey),
            IsTwoFactorEnabled = await _userManager.GetTwoFactorEnabledAsync(user)
        };

        return Ok(new ApiResponse<AuthenticatorSetupResponse>
        {
            Success = true,
            Data = model
        });
    }

    //todo


    // POST: api/Account/logout
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        _logger.LogInformation("Usuário deslogado");

        return Ok(new ApiResponse<string>
        {
            Success = true,
            Message = "Logout realizado com sucesso"
        });
    }

    // POST: api/Account/register
    [HttpPost("register")]
    // public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         return BadRequest(new ApiResponse<string>
    //         {
    //             Success = false,
    //             Message = "Dados inválidos"
    //         });
    //     }

    //     var user = new UserAccount { UserName = request.Email, Email = request.Email };
    //     var result = await _userManager.CreateAsync(user, request.Password);

    //     if (result.Succeeded)
    //     {
    //         _logger.LogInformation("Novo usuário registrado: {Email}", request.Email);

    //         // Opcional: enviar email de confirmação
    //         var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
    //         code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

    //         // await _emailSender.SendConfirmationEmailAsync(user.Email, code);

    //         return Ok(new ApiResponse<string>
    //         {
    //             Success = true,
    //             Message = "Usuário registrado com sucesso"
    //         });
    //     }

    //     return BadRequest(new ApiResponse<string>
    //     {
    //         Success = false,
    //         Message = "Erro ao registrar usuário",
    //         Errors = result.Errors.Select(e => e.Description)
    //     });
    // }

    private string FormatKey(string unformattedKey)
    {
        var result = new StringBuilder();
        int currentPosition = 0;
        while (currentPosition + 4 < unformattedKey.Length)
        {
            result.Append(unformattedKey.Substring(currentPosition, 4)).Append(" ");
            currentPosition += 4;
        }
        if (currentPosition < unformattedKey.Length)
        {
            result.Append(unformattedKey.Substring(currentPosition));
        }
        return result.ToString().ToLowerInvariant();
    }

    private string GenerateQrCodeUri(string email, string unformattedKey)
    {
        return string.Format(
            "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6",
            UrlEncoder.Default.Encode("SuaAplicacao"),
            UrlEncoder.Default.Encode(email),
            unformattedKey);
    }


}



// [HttpPost("twofactorverify")]
// public async Task<IActionResult> twofactorverify(RecoveryCodeRequestViewModel model)
// {

//     var result = await HttpContext.AuthenticateAsync(IdentityConstants.TwoFactorUserIdScheme);

//     if (!result.Succeeded)
//     {

//         return Ok(result);
//         // return Ok(IdentityResult.Failed(new IdentityError() { Description = "Seu token expirou ou token inválido" }));
//         // throw new AuthServicesException(AuthErrorsMessagesException.);
//         // return View();
//     }


//         var user = await _userManager.FindByIdAsync(result.Principal.FindFirstValue("sub"));
//         if (user != null)
//         {
//             var isValid = await _userManager.VerifyTwoFactorTokenAsync(
//                 user,
//                 result.Principal.FindFirstValue("amr"), model.RecoveryCode);

//             if (isValid)
//             {
//                 await HttpContext.SignOutAsync(IdentityConstants.TwoFactorUserIdScheme);


//                 var claimsPrincipal = await _userClaimsPrincipalFactory.CreateAsync(user);


//                 await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, claimsPrincipal);

//                 return RedirectToAction("About");
//             }


//             return Ok(IdentityResult.Failed(new IdentityError() { Description = "Invalid Token" }));
//         }

//     return Ok(IdentityResult.Failed(new IdentityError() { Description = "Invalid Request" }));
// }

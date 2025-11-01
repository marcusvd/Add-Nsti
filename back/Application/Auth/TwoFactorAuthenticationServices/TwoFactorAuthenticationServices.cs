using Domain.Entities.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Application.Helpers.Inject;
using Application.Auth.Extends;
using Application.EmailServices.Services;
using Application.Auth.JwtServices;
using Application.Auth.IdentityTokensServices;
using Application.Auth.UsersAccountsServices.EmailUsrAccountServices.dtos;
using Application.Shared.Dtos;
using Application.Auth.TwoFactorAuthentication.Dtos;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Text.Encodings.Web;
using Application.Auth.UsersAccountsServices.EmailUsrAccountServices.Services;
using Application.Auth.UsersAccountsServices.Services.Auth;

namespace Application.Auth.TwoFactorAuthentication;

public class TwoFactorAuthenticationServices : AuthenticationBase, ITwoFactorAuthenticationServices
{
    private readonly IUserAccountAuthServices _userAccountAuthServices;
    private readonly IIdentityTokensServices _identityTokensServices;
    private readonly UserManager<UserAccount> _userManager;
    private readonly SignInManager<UserAccount> _signInManager;
    private readonly IValidatorsInject _validatorsInject;
    private readonly ISmtpServices _smtpServices;
    private readonly IJwtServices _jwtServices;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IEmailUserAccountServices _emailUserAccountServices;

    public TwoFactorAuthenticationServices(
                      IUserAccountAuthServices userAccountAuthServices,
                      IIdentityTokensServices identityTokensServices,
                      UserManager<UserAccount> userManager,
                      SignInManager<UserAccount> signInManager,
                      IValidatorsInject validatorsInject,
                      ISmtpServices smtpServices,
                      IJwtServices jwtServices,
                      IHttpContextAccessor httpContextAccessor,
                      IEmailUserAccountServices emailUserAccountServices

      )
    {
        _userManager = userManager;
        _userAccountAuthServices = userAccountAuthServices;
        _signInManager = signInManager;
        _validatorsInject = validatorsInject;
        _smtpServices = smtpServices;
        _jwtServices = jwtServices;
        _identityTokensServices = identityTokensServices;
        _httpContextAccessor = httpContextAccessor;
        _emailUserAccountServices = emailUserAccountServices;

    }
    public async Task<ApiResponse<UserToken>> TwoFactorVerifyAsync(VerifyTwoFactorRequestDto request)
    {
        IsValidEmail(request.Email);

        var userAccount = await _userAccountAuthServices.GetUserAccountByEmailAsync(request.Email);

        await _emailUserAccountServices.ValidateUserAccountAsync(userAccount);

        var appAuthToken = await _userManager.VerifyTwoFactorTokenAsync(userAccount, _userManager.Options.Tokens.AuthenticatorTokenProvider, request.Code);//app Authenticator

        var emailAuthToken = await _userManager.VerifyTwoFactorTokenAsync(userAccount, request.Provider, request.Code);//sent by email.

        await _signInManager.SignInAsync(userAccount, request.RememberMe);

        return await ResponseTwoFactorVerifyAsync(!emailAuthToken && !appAuthToken, userAccount);
    }
    private async Task<ApiResponse<UserToken>> ResponseTwoFactorVerifyAsync(bool tokenResponse, UserAccount userAccount)
    {
        if (tokenResponse)
            return ApiResponse<UserToken>.Response(["Token inválido"], tokenResponse, "TwoFactorVerify", _jwtServices.InvalidUserToken());
        else
            return ApiResponse<UserToken>.Response([""], tokenResponse, "Login realizado com sucesso", await _jwtServices.CreateAuthenticationResponseAsync(userAccount));
    }

    public async Task<ApiResponse<EnableAuthenticatorResponseDto>> EnableAuthenticatorAsync(ToggleAuthenticatorRequestDto request)
    {
        var userPrincipalClims = _httpContextAccessor.HttpContext.User;

        var user = await _userManager.GetUserAsync(userPrincipalClims);

        var verificationCode = CodeHandling(request.Code);

        var isValid = await _userManager.VerifyTwoFactorTokenAsync(user, _userManager.Options.Tokens.AuthenticatorTokenProvider, verificationCode);

        bool result = !(user == null) && isValid;

        return await ResponseEnableAuthenticatorAsync(user, result, request.Enabled);


        // if (user == null || !isValid)
        // {
        //     return BadRequest(new ApiResponse<string>
        //     {
        //         Success = false,
        //         Message = "Código inválido"
        //     });
        // }



        // string onOff2FA = request.Enabled == true ? "habilitado" : "desabilitado";

        // _logger.LogInformation(@$"2FA {onOff2FA} para usuário: {user.Id}", user.Id);



        // return Ok(new ApiResponse<EnableAuthenticatorResponse>
        // {
        //     Success = true,
        //     Message = "Autenticador habilitado com sucesso",
        //     Data = new EnableAuthenticatorResponseDto
        //     {
        //         RecoveryCodes = recoveryCodes.ToArray(),
        //         IsTwoFactorEnabled = true
        //     }
        // });
    }
    private string CodeHandling(string code) => code.Replace(" ", "").Replace("-", "");
    private async Task<ApiResponse<EnableAuthenticatorResponseDto>> ResponseEnableAuthenticatorAsync(UserAccount userAccount, bool result, bool onOff)
    {
        if (result)
        {
            await _userManager.SetTwoFactorEnabledAsync(userAccount, onOff);

            var recoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(userAccount, 10) ?? [];

            var listOfCodes = ObjEnableAuthenticatorResponse(recoveryCodes);

            return ApiResponse<EnableAuthenticatorResponseDto>.Response([""], result, "Autenticador habilitado com sucesso", listOfCodes);
        }
        else
            return ApiResponse<EnableAuthenticatorResponseDto>.Response(["Código inválido / Usuário não autenticado"], result, "EnableAuthenticatorAsync", new EnableAuthenticatorResponseDto());
    }


    private EnableAuthenticatorResponseDto ObjEnableAuthenticatorResponse(IEnumerable<string> recoveryCodes)
    {
        return new EnableAuthenticatorResponseDto
        {
            RecoveryCodes = recoveryCodes.ToArray(),
            IsTwoFactorEnabled = true
        };

    }

    // private async Task<ApiResponse<EnableAuthenticatorResponseDto>> ResponseEnableAuthenticatorAsync(bool tokenResponse, IEnumerable<string> recoveryCodes)
    // {
    //     if (tokenResponse)
    //     {
    //         var response = new EnableAuthenticatorResponseDto
    //         {
    //             RecoveryCodes = recoveryCodes.ToArray(),
    //             IsTwoFactorEnabled = true
    //         };

    //         return ApiResponse<EnableAuthenticatorResponseDto>.Response([""], tokenResponse, "Autenticador habilitado com sucesso", response);
    //     }
    //     else
    //         return ApiResponse<EnableAuthenticatorResponseDto>.Response(["Código inválido / Usuário não autenticado"], tokenResponse, "EnableAuthenticatorAsync", new EnableAuthenticatorResponseDto());
    // }

    public async Task<ApiResponse<TwoFactorStatusDto>> GetTwoFactorStatusAsync(int userId)
    {
        ValidateUserId(userId);

        var userAccount = await _userAccountAuthServices.GetUserAccountByUserIdAsync(userId);

        _validatorsInject.GenericValidators.IsObjNull(userAccount);

        var is2FaEnable = await _userManager.GetTwoFactorEnabledAsync(userAccount);
        var hasAuthenticator = await _userManager.GetAuthenticatorKeyAsync(userAccount) != null;
        var recoveryCodesLeft = await _userManager.CountRecoveryCodesAsync(userAccount);

        var twoFactorStatus = new TwoFactorStatusDto()
        {
            IsEnabled = is2FaEnable,
            HasAuthenticator = hasAuthenticator,
            RecoveryCodesLeft = recoveryCodesLeft,
        };

        var result = twoFactorStatus != null;

        return ApiResponse<TwoFactorStatusDto>.Response([""], result, "Autenticador habilitado com sucesso", twoFactorStatus);

    }
    public async Task<ApiResponse<AuthenticatorSetupResponseDto>> GetAuthenticatorSetup()
    {

        var userPrincipalClims = _httpContextAccessor.HttpContext.User;

        var user = await _userManager.GetUserAsync(userPrincipalClims);

        await _emailUserAccountServices.ValidateUserAccountAsync(user);

        string authenticatorKey = await AuthenticatorKey(user);

        var result = await Setup2FAResponse(user, authenticatorKey);

        string onOff2FA = result.IsTwoFactorEnabled == true ? "habilitado" : "desabilitado";

        return ApiResponse<AuthenticatorSetupResponseDto>.Response([""], true, @$"Autenticador {onOff2FA} com sucesso", result);

    }
    private async Task<AuthenticatorSetupResponseDto> Setup2FAResponse(UserAccount userAccount, string authenticatorKey)
    {
        var authResponseSetup = new AuthenticatorSetupResponseDto
        {
            SharedKey = FormatKey(authenticatorKey),
            AuthenticatorUri = GenerateQrCodeUri(userAccount.Email, authenticatorKey),
            IsTwoFactorEnabled = await _userManager.GetTwoFactorEnabledAsync(userAccount)
        };

        return authResponseSetup;
    }
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
            UrlEncoder.Default.Encode("I.M Integrações"),
            UrlEncoder.Default.Encode(email),
            unformattedKey);
    }
    private async Task<string> AuthenticatorKey(UserAccount userAccount)
    {

        var authenticatorKey = await _userManager.GetAuthenticatorKeyAsync(userAccount) ?? "";

        if (string.IsNullOrEmpty(authenticatorKey))
        {
            await _userManager.ResetAuthenticatorKeyAsync(userAccount);
            authenticatorKey = await _userManager.GetAuthenticatorKeyAsync(userAccount) ?? "";
        }

        return authenticatorKey;
    }



    public async Task<IdentityResult> OnOff2FaCodeViaEmailAsync(OnOff2FaCodeViaEmailDto request)
    {

        IsValidEmail(request.Email);

        var userAccount = await _userAccountAuthServices.GetUserAccountByEmailAsync(request.Email);

        _validatorsInject.GenericValidators.IsObjNull(userAccount);

        await _emailUserAccountServices.ValidateUserAccountAsync(userAccount);

        userAccount = AssignValues(request.OnOff, userAccount);

        return await _userManager.UpdateAsync(userAccount);
    }

    private UserAccount AssignValues(bool onOff, UserAccount userAccount)
    {
        if (onOff)
            userAccount.Code2FaSendEmail = DateTime.MinValue;
        else
            userAccount.Code2FaSendEmail = DateTime.Now;

        return userAccount;
    }


    public async Task<bool> HandleTwoFactorAuthenticationAsync(UserAccount userAccount)
    {
        if (!await _userManager.GetTwoFactorEnabledAsync(userAccount))
            return false;

        var validProviders = await _userManager.GetValidTwoFactorProvidersAsync(userAccount);

        if (!validProviders.Contains("Email"))
            return false;

        await _signInManager.RememberTwoFactorClientAsync(userAccount);

        await TwoFactorAuthenticationToken(userAccount);

        return true;
    }
    private async Task TwoFactorAuthenticationToken(UserAccount userAccount)
    {

        if (userAccount.Code2FaSendEmail == DateTime.MinValue)
        {
            var genToken = await _identityTokensServices.GenerateTwoFactorTokenAsync(userAccount);

            var dataConfirmEmail = DataConfirmEmail.DataConfirmEmailMaker(userAccount, [genToken, "", "", "I.M: Autenticação de dois fatores."]);

            await _smtpServices.SendTokensEmailAsync(dataConfirmEmail, dataConfirmEmail.TwoFactorAuthentication());
        }

    }
    private ClaimsPrincipal Store2FA(int id, string provider)
    {
        var identity = new ClaimsIdentity(new List<Claim>
        {
            new Claim("sub", id.ToString()),
            new Claim("amr", provider)

        }, IdentityConstants.TwoFactorUserIdScheme);


        return new ClaimsPrincipal(identity);
    }




}

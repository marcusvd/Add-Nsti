using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


using Application.Exceptions;
using Authentication;
using Application.Services.Operations.Authentication.Dtos;
using Application.Services.Helpers;
using Application.Services.Operations.Authentication.Dtos.Mappers;
using Microsoft.Extensions.Logging;
using System;


namespace Application.Services.Operations.Authentication.Login;

public class LoginServices : AuthenticationBase, ILoginServices
{
    private UserManager<UserAccount> _userManager;
    private readonly ILogger<GenericValidatorServices> _logger;
    private readonly GenericValidatorServices _genericValidatorServices;
    private readonly EmailServer _emailService;
    private readonly JwtHandler _jwtHandler;
    private readonly IAuthenticationObjectMapperServices _mapper;
    public LoginServices(
          UserManager<UserAccount> userManager,
          ILogger<GenericValidatorServices> logger,
          EmailServer emailService,
          JwtHandler jwtHandler,
          IAuthenticationObjectMapperServices mapper,
          GenericValidatorServices genericValidatorServices
      ) : base(userManager)
    {
        _userManager = userManager;
        _logger = logger;
        _emailService = emailService;
        _jwtHandler = jwtHandler;
        _mapper = mapper;
        _genericValidatorServices = genericValidatorServices;
    }

    public async Task<UserToken> Login(LoginDto user)
    {
        _genericValidatorServices.IsObjNull(user);

        var userAccount = await FindUserAsync(user.UserName);

        if (userAccount == null)
        {
            _logger.LogWarning("Login attempt for non-existent user: {Username}", user.UserName);
            throw new AuthServicesException(AuthErrorsMessagesException.UserAccountNotFound);
        }

        await ValidateAccountStatusAsync(userAccount);

        if (!await IsPasswordValidAsync(userAccount, user.Password))
        {
            _logger.LogWarning("Invalid password attempt for user: {UserId}", userAccount.Id);
            throw new AuthServicesException(AuthErrorsMessagesException.InvalidUserNameOrPassword);
        }

        //  if (await IsEnabledTwoFactorAsync(userAccount))
        //             {
        //                 var returnUserToken = await _jwtHandler.GenerateUserToken(BuildUserClaims(userAccount), user);
        //                 returnUserToken.Action = "TwoFactor";
        //                 return returnUserToken;
        //             }

        //             return await _jwtHandler.GenerateUserToken(BuildUserClaims(userAccount), _mapper.UserAccountMapper(userAccount));
    }

    private async Task ValidateAccountStatusAsync(UserAccount userAccount)
    {
        if (await IsAccountLockedOutAsync(userAccount))
        {
            _logger.LogWarning("Account locked out: {UserId}", userAccount.Id);
            await NotifyAccountLockedAsync(userAccount);
            throw new AuthServicesException(AuthErrorsMessagesException.UserIsLocked);
        }

        if (!await IsEmailConfirmedAsync(userAccount))
        {
            _logger.LogWarning("Email not confirmed for user: {UserId}", userAccount.Id);
            throw new AuthServicesException(AuthErrorsMessagesException.EmailIsNotConfirmed);
        }
    }

    private async Task NotifyAccountLockedAsync(UserAccount userAccount)
    {
        try
        {
            await _emailService.SendAsync(To: userAccount.Email,
                       Subject: "Sonny conta bloqueada",
                       Body: "O número de dez tentativas de login foi esgotado e a conta foi bloqueada por atingir dez tentativas com senhas incorretas. Sugerimos troque sua senha.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send account locked notification to {Email}", userAccount.Email);
        }
    }


    private async Task<bool> IsEnabledTwoFactorAsync(UserAccount userAccount)
    {
        if (await _userManager.GetTwoFactorEnabledAsync(userAccount))
        {
            var validator = await _userManager.GetValidTwoFactorProvidersAsync(userAccount);

            if (validator.Contains("Email"))
            {
                var token = await _userManager.GenerateTwoFactorTokenAsync(userAccount, "Email");
                _email.Send(To: userAccount.Email, Subject: "SONNY: Autenticação de dois fatores", Body: "Código: Autenticação de dois fatores: " + token);

                return true;
            }
        }
        return false;
    }



    // private async Task<bool> IsLockedOutAsync(UserAccount userAccount)
    // {
    //     var result = await _userManager.IsLockedOutAsync(userAccount);
    //     if (result)
    //     {
    //         _email.Send(To: userAccount.Email, Subject: "Sonny conta bloqueada.", Body: "O número de dez tentativas de login foi esgotado e a conta foi bloqueada por atingir dez tentativas com senhas incorretas. Sugerimos troque sua senha. " + "Link para troca  de senha.");
    //         return result;
    //     }
    //     return result;
    // }
    // private async Task<bool> EmailIsNotConfirmedAsync(UserAccount userAccount)
    // {
    //     if (!await _userManager.IsEmailConfirmedAsync(userAccount))
    //         return false;

    //     return true;
    // }
    // private async Task<bool> CheckPasswordAsync(UserAccount userAccount, string password)
    // {
    //     var result = await _userManager.CheckPasswordAsync(userAccount, password);

    //     if (result)
    //     {
    //         await _userManager.ResetAccessFailedCountAsync(userAccount);
    //         return result;
    //     }
    //     else
    //     {
    //         await _userManager.AccessFailedAsync(userAccount);
    //         return result;
    //     }
    // }

}
using Microsoft.AspNetCore.Identity;

using Authentication.Entities;
using Authentication.Exceptions;
using Authentication.Helpers;
using Microsoft.Extensions.Logging;
using Authentication.Operations.CompanyUsrAcct;
using System.Linq;


namespace Authentication.Operations.Login;

public class LoginServices : AuthenticationBase, ILoginServices
{
    private UserManager<UserAccount> _userManager;
    private readonly IIdentityEntitiesManagerRepository _identityEntitiesManagerRepository;
    private readonly ILogger<AuthGenericValidatorServices> _logger;
    private readonly AuthGenericValidatorServices _genericValidatorServices;
    // private readonly EmailServer _emailService;
    private readonly JwtHandler _jwtHandler;
    // private readonly IAuthenticationObjectMapperServices _mapper;
    public LoginServices(
          UserManager<UserAccount> userManager,
          ILogger<AuthGenericValidatorServices> logger,
          IIdentityEntitiesManagerRepository identityEntitiesManagerRepository,
          //   EmailServer emailService,
          JwtHandler jwtHandler,
          //   IAuthenticationObjectMapperServices mapper,
          AuthGenericValidatorServices genericValidatorServices
      ) : base(userManager, jwtHandler)
    {
        _userManager = userManager;
        _logger = logger;
        // _emailService = emailService;
        _jwtHandler = jwtHandler;
        _identityEntitiesManagerRepository = identityEntitiesManagerRepository;
        // _mapper = mapper;
        _genericValidatorServices = genericValidatorServices;
    }

    public async Task<UserToken> LoginAsync(LoginModel user)
    {
        _genericValidatorServices.IsObjNull(user);

        // var userAccount = await FindUserAsync(user.Email);

        var userAccount = await _identityEntitiesManagerRepository.GetUserAccountFull(user.Email);

        // var companies = userAccount.CompanyUserAccounts.Any() ? userAccount.CompanyUserAccounts : [];

        if (userAccount == null)
        {
            _logger.LogWarning("Login attempt for non-existent user: {Username}", user.Email);
            throw new AuthServicesException(AuthErrorsMessagesException.UserAccountNotFound);
        }

        await ValidateAccountStatusAsync(userAccount);

        if (!await IsPasswordValidAsync(userAccount, user.Password))
        {
            _logger.LogWarning("Invalid password attempt for user: {UserId}", userAccount.Id);
            throw new AuthServicesException(AuthErrorsMessagesException.InvalidUserNameOrPassword);
        }

        if (await HandleTwoFactorAuthenticationAsync(userAccount))
        {
            _logger.LogInformation("2FA required for user: {UserId}", userAccount.Id);
            return await CreateTwoFactorResponse(userAccount);
        }

        _logger.LogInformation("Successful login for user: {UserId}", userAccount.Id);

        return await CreateAuthenticationResponseAsync(userAccount);
    }


    private async Task<bool> HandleTwoFactorAuthenticationAsync(UserAccount userAccount)
    {
        if (!await _userManager.GetTwoFactorEnabledAsync(userAccount))
            return false;

        var validProviders = await _userManager.GetValidTwoFactorProvidersAsync(userAccount);

        if (!validProviders.Contains("Email"))
            return false;

        var token = await _userManager.GenerateTwoFactorTokenAsync(userAccount, "Email");

        await SendTwoFactorTokenAsync(userAccount, token);

        return true;
    }

    private async Task SendTwoFactorTokenAsync(UserAccount userAccount, string token)
    {
        try
        {
            // await _emailService.SendAsync(To: userAccount.Email,
            //         Subject: "SONNY: Autenticação de dois fatores",
            //         Body: $"Código: Autenticação de dois fatores: {token}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send 2FA token to {Email}", userAccount.Email);
            throw new AuthServicesException(AuthErrorsMessagesException.TwoFactorTokenSendFailed);
        }
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
            // await _emailService.SendAsync(To: userAccount.Email,
            //            Subject: "Sonny conta bloqueada",
            //            Body: "O número de dez tentativas de login foi esgotado e a conta foi bloqueada por atingir dez tentativas com senhas incorretas. Sugerimos troque sua senha.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send account locked notification to {Email}", userAccount.Email);
        }
    }

}
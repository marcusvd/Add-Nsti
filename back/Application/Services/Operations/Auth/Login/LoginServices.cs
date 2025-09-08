using Microsoft.AspNetCore.Identity;

using Domain.Entities.Authentication;
using Authentication.Exceptions;
using Authentication.Helpers;
using Authentication.Jwt;
using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Mvc;
using UnitOfWork.Persistence.Operations;


namespace Application.Services.Operations.Auth.Login;

public class LoginServices : AuthenticationBase, ILoginServices
{
    // private UserManager<UserAccount> _userManager;
    private readonly ILogger<LoginServices> _logger;
     private readonly IUnitOfWork _GENERIC_REPO;

    
    public LoginServices(
        //   UserManager<UserAccount> userManager,
          ILogger<LoginServices> logger,
            IUnitOfWork GENERIC_REPO,
          JwtHandler jwtHandler,
          IUrlHelper url
      ) : base(jwtHandler, logger, url, GENERIC_REPO)
    {
        // _userManager = userManager;
        _logger = logger;
        _GENERIC_REPO = GENERIC_REPO;
    }
   
    public async Task<UserToken> LoginAsync(LoginModel user)
    {
        _GENERIC_REPO._GenericValidatorServices.IsObjNull(user);

        // var userAccount = await FindUserAsync(user.Email);

         var userAccount = await _GENERIC_REPO.UsersAccounts.GetUserAccountFull(user.Email);


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
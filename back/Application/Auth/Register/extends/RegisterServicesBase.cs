using UnitOfWork.Persistence.Operations;
using Application.Auth.Extends;
using Application.Auth.Register.Exceptions;
using Domain.Entities.Authentication;
using Application.Auth.IdentityTokensServices;
using Application.Auth.UsersAccountsServices.EmailUsrAccountServices.dtos;
using Application.EmailServices.Services;
using Microsoft.AspNetCore.Identity;


namespace Application.Auth.Register.Extends;

public abstract class RegisterServicesBase : AuthenticationBase, IRegisterServicesBase
{
    private readonly UserManager<UserAccount> _userManager;
    private readonly IIdentityTokensServices _identityTokensServices;
    private readonly ISmtpServices _emailServices;
    // private readonly IUnitOfWork _genericRepo;

    public RegisterServicesBase(
         //  IUnitOfWork genericRepo,
         UserManager<UserAccount> userManager,
         IIdentityTokensServices identityTokensServices,
         ISmtpServices emailServices
        )
    {
        _userManager = userManager;
        _identityTokensServices = identityTokensServices;
        _emailServices = emailServices;
        // _genericRepo = genericRepo;
    }


    public async Task ValidateUniqueUserCredentials(string userName, string email)
    {
        if (await IsUserNameDuplicate(userName))
        {
            // _logger.LogWarning("Duplicate username attempt: {UserName}", register.UserName);
            throw new RegisterServicesException(RegisterServicesMessagesException.UserNameAlreadyRegisterd);
        }

        if (await IsEmailDuplicate(email))
        {
            // _logger.LogWarning("Duplicate email attempt: {Email}", register.Email);
            throw new RegisterServicesException(RegisterServicesMessagesException.EmailAlreadyRegisterd);
        }
    }

    private protected async Task<bool> IsUserNameDuplicate(string userName)
    {
        var userAccount = await _userManager.FindByNameAsync(userName);

        return userAccount != null;
    }
    private protected async Task<bool> IsEmailDuplicate(string email)
    {
        var userAccount = await _userManager.FindByEmailAsync(email);

        return userAccount != null;
    }

    public async Task<bool> SendUrlTokenEmailConfirmation(bool registerResult, UserAccount userAccount)
    {
        if (registerResult)
        {
            var genToken = _identityTokensServices.GenerateUrlTokenEmailConfirmation(userAccount, "ConfirmEmailAddress", "auth");

            var dataConfirmEmail = DataConfirmEmail.DataConfirmEmailMaker(userAccount, [await genToken, "http://localhost:4200/confirm-email", "api/auth/ConfirmEmailAddress", "I.M - Link para confirmação de e-mail"]);

            await _emailServices.SendTokensEmailAsync(dataConfirmEmail, dataConfirmEmail.WelcomeMessage());

            return true;
        }

        return false;
    }

}
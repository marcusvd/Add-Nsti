using Domain.Entities.Authentication;
using UnitOfWork.Persistence.Operations;
using Microsoft.AspNetCore.Mvc;
using Authentication.Exceptions;
using Domain.Entities.Authentication.extends;
using Microsoft.AspNetCore.Identity;


namespace Application.Auth.IdentityTokensServices;

public class IdentityTokensServices : UserAccountBaseDb, IIdentityTokensServices
{

    private readonly UserManager<UserAccount> _userManager;
    private readonly IUrlHelper _urlHelper;

    public IdentityTokensServices(
          UserManager<UserAccount> userManager,
          IUrlHelper urlHelper
      )
    {
        _urlHelper = urlHelper;
        _userManager = userManager;
    }

    public async Task<string> GenerateUrlTokenEmailConfirmation(UserAccount userAccount, string action, string controller)
    {
        var urlConfirmMail = _urlHelper.Action(action, controller, new
        {
            token = await _userManager.GenerateEmailConfirmationTokenAsync(userAccount),
            email = userAccount.Email

        }) ?? throw new InvalidOperationException("Unable to generate email confirmation URL.");

        return urlConfirmMail;
    }

    public async Task<string> GenerateUrlTokenEmailChange(UserAccount userAccount, string action, string controller, string newEmail)
    {
        var urlConfirmMail = _urlHelper.Action(action, controller, new
        {
            token = await _userManager.GenerateChangeEmailTokenAsync(userAccount, newEmail),
            email = userAccount.Email,
            id = userAccount.Id

        }) ?? throw new InvalidOperationException("Unable to generate email confirmation URL.");

        return urlConfirmMail;
    }
    public async Task<string> GenerateUrlTokenPasswordReset(UserAccount userAccount, string action, string controller)
    {
        var token = await _userManager.GeneratePasswordResetTokenAsync(userAccount);

        var urlReset = _urlHelper.Action(action, controller, new { token, email = userAccount.Email, userName = userAccount.UserName }) ?? throw new AuthServicesException(AuthErrorsMessagesException.ErrorWhenGenerateEmailLink); ;

        return urlReset;
    }
    public async Task<string> GenerateTwoFactorTokenAsync(UserAccount userAccount)
    {
        var token = await _userManager.GenerateTwoFactorTokenAsync(userAccount, "Email");
        return token;
    }


}

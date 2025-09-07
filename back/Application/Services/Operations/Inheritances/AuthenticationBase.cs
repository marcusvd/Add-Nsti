using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Authentication.Jwt;
using Domain.Entities.Authentication;
using Application.Services.Operations.Auth.Dtos;
using Authentication.Exceptions;
using Microsoft.Extensions.Logging;
using Application.Services.Operations.Auth.Account.dtos;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Operations.Auth;

public class AuthenticationBase
{
    private UserManager<UserAccount> _userManager;
    private readonly JwtHandler _jwtHandler;
    private readonly ILogger _logger;
    private readonly IUrlHelper _url;
    public AuthenticationBase(
        UserManager<UserAccount> userManager,
         JwtHandler jwtHandler,
         ILogger logger,
         IUrlHelper url
        )
    {
        _userManager = userManager;
        _jwtHandler = jwtHandler;
        _logger = logger;
        _url = url;
    }
    private protected async Task<UserAccount> FindUserAsync(string userNameOrEmail)
    {
        return await _userManager.FindByEmailAsync(userNameOrEmail) ?? await _userManager.FindByNameAsync(userNameOrEmail) ?? new UserAccount() { UserProfileId = "Invalid", DisplayUserName = "Invalid" };
    }
    private protected async Task<UserAccount> FindUserByIdAsync(int id)
    {
        return await _userManager.FindByIdAsync(id.ToString()) ?? new UserAccount() { UserProfileId = "Invalid", DisplayUserName = "Invalid" };
    }
    private protected async Task<IdentityResult> IsUserExist(string email)
    {
        var userFound = await _userManager.FindByEmailAsync(email);

        return userFound != null ? IdentityResult.Success : IdentityResult.Failed([new IdentityError(){Description = "Usuário não encontrado."}]);
    }

    private protected async Task<bool> IsAccountLockedOutAsync(UserAccount userAccount)
    {
        return await _userManager.IsLockedOutAsync(userAccount);
    }

    private protected async Task<bool> IsEmailConfirmedAsync(UserAccount userAccount)
    {
        return await _userManager.IsEmailConfirmedAsync(userAccount);
    }

    private protected async Task<bool> IsPasswordValidAsync(UserAccount userAccount, string password)
    {
        var isValid = await _userManager.CheckPasswordAsync(userAccount, password);

        if (isValid)
        {
            await _userManager.ResetAccessFailedCountAsync(userAccount);
            return true;
        }
        else
        {
            await _userManager.AccessFailedAsync(userAccount);
            return false;
        }
    }

    private protected void ResultUserCreation(bool userAccount, bool userProfile, string userEmail, string errosMsg)
    {

        if (!userAccount || !userProfile)
        {
            _logger.LogError("User creation failed for {Email}. Errors: {Errors}", userEmail, string.Join(", ", errosMsg));

            throw new AuthServicesException(AuthErrorsMessagesException.ErrorWhenRegisterUserAccount);
        }
    }
    private protected async Task<List<Claim>> BuildUserClaims(UserAccount userAccount)
    {
        var getRoles = await _userManager.GetRolesAsync(userAccount);

        var claims = new List<Claim>
            {
              new Claim(ClaimTypes.NameIdentifier, userAccount.Id.ToString()),
            //   new Claim(ClaimTypes.Name, userAccount.UserName!),
              new Claim(ClaimTypes.Name, userAccount.Email!),
            };

        foreach (var role in getRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }

    private protected async Task<UserToken> CreateAuthenticationResponseAsync(UserAccount userAccount)
    {
        var claimsList = await BuildUserClaims(userAccount);
        var roles = await _userManager.GetRolesAsync(userAccount);
        var token = await _jwtHandler.GenerateUserToken(claimsList, userAccount, roles);

        return token;
    }

    private protected async Task<UserToken> CreateTwoFactorResponse(UserAccount userAccount)
    {
        var claimsList = await BuildUserClaims(userAccount);
        var token = await _jwtHandler.GenerateUserToken(claimsList, userAccount, []);
        token.Action = "TwoFactor";
        return token;
    }

    private protected RoleDto CreateRole(string role, string DisplayRole)
    {
        return new RoleDto
        {
            Name = role,
            DisplayRole = DisplayRole,
        };
    }

    private protected UpdateUserRole CreateUpdateUserRole(string userNameOrEmail, string role, string DisplayRole, bool delete)
    {
        return new UpdateUserRole
        {
            UserName = userNameOrEmail,
            Role = role,
            DisplayRole = DisplayRole,
            Delete = delete
        };
    }

    public async Task SendEmailConfirmationAsync(DataConfirmEmail dataConfirmEmail, string body)
    {
        try
        {
            //var confirmationUrl = await GenerateEmailUrl(dataConfirmEmail);

            if (string.IsNullOrEmpty(dataConfirmEmail.TokenConfirmationUrl))
            {
                _logger.LogError("Failed to generate email confirmation URL for {Email}", dataConfirmEmail.UserAccount.Email);
                throw new AuthServicesException(AuthErrorsMessagesException.ErrorWhenGenerateEmailLink);
            }

           // var formattedUrl = dataConfirmEmail.WelcomeMessage();
            // var formattedUrl = FormatEmailUrl(dataConfirmEmail.UrlFront, dataConfirmEmail.TokenConfirmationUrl, dataConfirmEmail.UrlBack, dataConfirmEmail.UserAccount);

            await SendAsync(To: dataConfirmEmail.UserAccount.Email, Subject: dataConfirmEmail.SubjectEmail, Body: body);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending confirmation email to {Email}", dataConfirmEmail.UserAccount.Email);
            throw;
        }
    }

    public async Task<string> GenerateUrlTokenEmailConfirmation(UserAccount userAccount, string action, string controller)
    {
        var urlConfirmMail = _url.Action(action, controller, new
        {
            token = await _userManager.GenerateEmailConfirmationTokenAsync(userAccount),
            email = userAccount.Email

        }) ?? throw new InvalidOperationException("Unable to generate email confirmation URL.");

        return urlConfirmMail;
    }
    public async Task<string> GenerateUrlTokenEmailChange(UserAccount userAccount, string action, string controller,string newEmail)
    {
        var urlConfirmMail = _url.Action(action, controller, new
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

        var urlReset = _url.Action(action,controller, new { token, email = userAccount.Email, userName = userAccount.UserName }) ?? throw new AuthServicesException(AuthErrorsMessagesException.ErrorWhenGenerateEmailLink); ;

        return urlReset;
    }



    public DataConfirmEmail DataConfirmEmailMaker(UserAccount user, string[] dataConfirmation)
    {
        return new DataConfirmEmail()
        {
            UserAccount = user,
            TokenConfirmationUrl = dataConfirmation[0],
            UrlFront = dataConfirmation[1],
            UrlBack = dataConfirmation[2],
            SubjectEmail = dataConfirmation[3]
        };
    }
    private static async Task SendAsync(string To = "register@nostopti.com.br", string From = "register@nostopti.com.br", string DisplayName = "Sonny System",
    string Subject = "Test Subject", string Body = "Test", string MailServer = "smtp.nostopti.com.br",
     int Port = 587, bool IsUseSsl = false, string UserName = "register@nostopti.com.br", string Password = "Nsti$2024")
    {
        var message = new MailMessage("register@nostopti.com.br", To, Subject, Body);
        SmtpClient SmtpClient = new SmtpClient(MailServer)
        {
            Port = 587,
            Credentials = new NetworkCredential(UserName, Password),
        };
        SmtpClient.SendCompleted += (s, e) =>
       {
           SmtpClient.Dispose();
           message.Dispose();
       };
        try
        {
            SmtpClient.SendAsync(message, null);

            await Task.CompletedTask;

        }
        catch (SmtpFailedRecipientException ex)
        {
            throw new Exception($"{ex}");
            // throw new EmailException($"{EmailErrosMessagesException.InvalidDomain} - {ex}");
        }
    }

}
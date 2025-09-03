using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Authentication.Jwt;
using Domain.Entities.Authentication;
using Application.Services.Operations.Auth.Dtos;

namespace Application.Services.Operations.Auth;

public class AuthenticationBase
{
    private UserManager<UserAccount> _userManager;
    private readonly JwtHandler _jwtHandler;
    public AuthenticationBase(
        UserManager<UserAccount> userManager,
         JwtHandler jwtHandler
        )
    {
        _userManager = userManager;
        _jwtHandler = jwtHandler;
    }
    private protected async Task<UserAccount> FindUserAsync(string userNameOrEmail)
    {
        var userFound = await _userManager.FindByEmailAsync(userNameOrEmail) ?? await _userManager.FindByNameAsync(userNameOrEmail);

        return userFound ?? throw new InvalidOperationException("User not found.");
    }
    private protected async Task<bool> IsUserExist(string email)
    {
        var userFound = await _userManager.FindByEmailAsync(email);

        return userFound != null;
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

    public static async Task SendAsync(string To = "register@nostopti.com.br", string From = "register@nostopti.com.br", string DisplayName = "Sonny System",
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
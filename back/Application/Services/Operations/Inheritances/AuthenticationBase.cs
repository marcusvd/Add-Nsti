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
using UnitOfWork.Persistence.Operations;

namespace Application.Services.Operations.Auth;

public class AuthenticationBase
{
    private readonly JwtHandler _jwtHandler;
    private readonly ILogger _logger;
    private readonly IUrlHelper _url;
    private readonly IUnitOfWork _GENERIC_REPO;
    public AuthenticationBase(
         JwtHandler jwtHandler,
         ILogger logger,
         IUrlHelper url,
         IUnitOfWork GENERIC_REPO
        )
    {
        _jwtHandler = jwtHandler;
        _logger = logger;
        _url = url;
        _GENERIC_REPO = GENERIC_REPO;
    }

    private protected DateTime DateTimeNow
    {

        get
        {
            DateTime now = DateTime.Now;
            return new DateTime(now.Year, now.Month, now.Day, now.Hour - 3, now.Minute, 0, 0);
        }
    }


    private protected async Task<UserAccount> FindUserAsync(string userNameOrEmail)
    {
        return await _GENERIC_REPO.UsersManager.FindByEmailAsync(userNameOrEmail) ?? await _GENERIC_REPO.UsersManager.FindByNameAsync(userNameOrEmail) ?? new UserAccount() { UserProfileId = "Invalid", DisplayUserName = "Invalid" };
    }
    private protected async Task<UserAccount> FindUserByIdAsync(int id)
    {
        return await _GENERIC_REPO.UsersManager.FindByIdAsync(id.ToString()) ?? new UserAccount() { UserProfileId = "Invalid", DisplayUserName = "Invalid" };
    }
    private protected async Task<IdentityResult> IsUserExist(string email)
    {
        var userFound = await _GENERIC_REPO.UsersManager.FindByEmailAsync(email);

        return userFound != null ? IdentityResult.Success : IdentityResult.Failed([new IdentityError() { Description = "Usuário não encontrado." }]);
    }
    private protected async Task<bool> IsAccountLockedOutAsync(UserAccount userAccount)
    {
        return await _GENERIC_REPO.UsersManager.IsLockedOutAsync(userAccount);
    }
    private protected async Task<bool> IsEmailConfirmedAsync(UserAccount userAccount)
    {
        return await _GENERIC_REPO.UsersManager.IsEmailConfirmedAsync(userAccount);
    }
    private protected async Task<bool> HandleTwoFactorAuthenticationAsync(UserAccount userAccount)
    {
        if (!await _GENERIC_REPO.UsersManager.GetTwoFactorEnabledAsync(userAccount))
            return false;

        var validProviders = await _GENERIC_REPO.UsersManager.GetValidTwoFactorProvidersAsync(userAccount);

        if (!validProviders.Contains("Email"))
            return false;

        var token = await _GENERIC_REPO.UsersManager.GenerateTwoFactorTokenAsync(userAccount, "Email");

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
    private protected async Task<IdentityResult> IsPasswordValidAsync(UserAccount userAccount, string password)
    {
        var isValid = await _GENERIC_REPO.UsersManager.CheckPasswordAsync(userAccount, password);

        if (isValid)
            return await _GENERIC_REPO.UsersManager.ResetAccessFailedCountAsync(userAccount);
        else
        {
            await _GENERIC_REPO.UsersManager.AccessFailedAsync(userAccount);
            return IdentityResult.Failed(new IdentityError() { Description = "User or password is invalid." });
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
        var getRoles = await _GENERIC_REPO.UsersManager.GetRolesAsync(userAccount);

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
        var roles = await _GENERIC_REPO.UsersManager.GetRolesAsync(userAccount);
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
    private protected UpdateUserRoleDto CreateUpdateUserRole(string userNameOrEmail, string role, string DisplayRole, bool delete)
    {
        return new UpdateUserRoleDto
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
            token = await _GENERIC_REPO.UsersManager.GenerateEmailConfirmationTokenAsync(userAccount),
            email = userAccount.Email

        }) ?? throw new InvalidOperationException("Unable to generate email confirmation URL.");

        return urlConfirmMail;
    }
    public async Task<string> GenerateUrlTokenEmailChange(UserAccount userAccount, string action, string controller, string newEmail)
    {
        var urlConfirmMail = _url.Action(action, controller, new
        {
            token = await _GENERIC_REPO.UsersManager.GenerateChangeEmailTokenAsync(userAccount, newEmail),
            email = userAccount.Email,
            id = userAccount.Id

        }) ?? throw new InvalidOperationException("Unable to generate email confirmation URL.");

        return urlConfirmMail;
    }
    public async Task<string> GenerateUrlTokenPasswordReset(UserAccount userAccount, string action, string controller)
    {
        var token = await _GENERIC_REPO.UsersManager.GeneratePasswordResetTokenAsync(userAccount);

        var urlReset = _url.Action(action, controller, new { token, email = userAccount.Email, userName = userAccount.UserName }) ?? throw new AuthServicesException(AuthErrorsMessagesException.ErrorWhenGenerateEmailLink); ;

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
    public async Task<string> UpdateUserRoles(UpdateUserRoleDto role)
    {
        var myUser = await FindUserAsync(role.UserName);

        if (role.Delete)
        {
            await _GENERIC_REPO.UsersManager.RemoveFromRoleAsync(myUser, role.Role);

            return "Role removed";
        }
        else
        {
            await _GENERIC_REPO.UsersManager.AddToRoleAsync(myUser, role.Role);
            return "Role Added";
        }
    }
    public async Task<IList<string>> GetRolesAsync(UserAccount userAccount) => await _GENERIC_REPO.UsersManager.GetRolesAsync(userAccount);
    public async Task<IdentityResult> CreateRoleAsync(RoleDto roleDto) => await _GENERIC_REPO.RolesManager.CreateAsync(new Role { Name = roleDto.Name, DisplayRole = roleDto.DisplayRole });
    public async Task<IdentityResult> IsUserExistCheckByEmailAsync(string email) => await IsUserExist(email);
    public async Task<IdentityResult> RequestEmailChangeAsync(RequestEmailChangeDto updateUserAccountEmailDto)
    {
        var userAccount = await FindUserAsync(updateUserAccountEmailDto.OldEmail);
        userAccount.Email = updateUserAccountEmailDto.NewEmail;

        if (userAccount == null)
            return IdentityResult.Failed(new IdentityError { Description = "Usuário não encontrado." });

        var genToken = GenerateUrlTokenEmailChange(userAccount, "ConfirmRequestEmailChange", "auth", updateUserAccountEmailDto.NewEmail);


        var dataConfirmEmail = DataConfirmEmailMaker(userAccount, [await genToken, "http://localhost:4200/confirm-request-email-change", "api/auth/ConfirmRequestEmailChange", "I.M - Link para confirmação mudança de email."]);
        // var dataConfirmEmail = DataConfirmEmailMaker(userAccount, [await genToken, "http://localhost:4200/request-email-change", "api/auth/RequestEmailChange", "I.M - Link para confirmação mudança de email."]);



        await SendEmailConfirmationAsync(dataConfirmEmail, dataConfirmEmail.EmailUpdated());

        return IdentityResult.Success;
    }
    public async Task<IdentityResult> ConfirmYourEmailChangeAsync(ConfirmEmailChangeDto confirmRequestEmailChange)
    {
        var userAccount = await FindUserByIdAsync(confirmRequestEmailChange.Id);
        if (userAccount == null)
            return IdentityResult.Failed(new IdentityError { Description = "Usuário não encontrado." });

        var result = await _GENERIC_REPO.UsersManager.ChangeEmailAsync(userAccount, confirmRequestEmailChange.Email, confirmRequestEmailChange.Token);
        if (result.Succeeded)
        {
            userAccount.UserName = confirmRequestEmailChange.Email;
            userAccount.Email = confirmRequestEmailChange.Email;
            await _GENERIC_REPO.UsersManager.UpdateAsync(userAccount);
        }

        return result;
    }
    public async Task<IdentityResult> ConfirmEmailAddressAsync(ConfirmEmailDto confirmEmail)
    {
        var userAccout = await FindUserAsync(confirmEmail.Email);

        var result = await _GENERIC_REPO.UsersManager.ConfirmEmailAsync(userAccout, confirmEmail.Token);

        return result;

    }
    public async Task<IdentityResult> ForgotPasswordAsync(ForgotPasswordDto forgotPassword)
    {
        var userAccount = await FindUserAsync(forgotPassword.Email);

        var genToken = GenerateUrlTokenPasswordReset(userAccount, "ForgotPassword", "auth");

        var dataConfirmEmail = DataConfirmEmailMaker(userAccount, [await genToken, "http://localhost:4200/password-reset", "api/auth/ForgotPassword", "I.M - Link para recadastramento de senha."]);

        await SendEmailConfirmationAsync(dataConfirmEmail, dataConfirmEmail.PasswordReset());

        return IdentityResult.Success;
    }
    public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto resetPassword)
    {
        var userAccount = await _GENERIC_REPO.UsersManager.FindByEmailAsync(resetPassword.Email) ?? throw new AuthServicesException(AuthErrorsMessagesException.ObjectIsNull);

        IdentityResult identityResult = await _GENERIC_REPO.UsersManager.ResetPasswordAsync(userAccount, resetPassword.Token, resetPassword.Password);

        if (!identityResult.Succeeded) throw new AuthServicesException($"{AuthErrorsMessagesException.ResetPassword} - {identityResult}");

        return identityResult;
    }

    public async Task<IdentityResult> PasswdChangeAsync(UserAccount user, string CurrentPwd, string NewPwd)
    {
        return await _GENERIC_REPO.UsersManager.ChangePasswordAsync(user, CurrentPwd, NewPwd);
    }

    private protected async Task ValidateUniqueUserCredentials(RegisterModelDto register)
    {
        if (await IsUserNameDuplicate(register.UserName))
        {
            _logger.LogWarning("Duplicate username attempt: {UserName}", register.UserName);
            throw new AuthServicesException(AuthErrorsMessagesException.UserNameAlreadyRegisterd);
        }

        if (await IsEmailDuplicate(register.Email))
        {
            _logger.LogWarning("Duplicate email attempt: {Email}", register.Email);
            throw new AuthServicesException(AuthErrorsMessagesException.EmailAlreadyRegisterd);
        }
    }
    private async Task<bool> IsUserNameDuplicate(string userName)
    {
        var userAccount = await _GENERIC_REPO.UsersManager.FindByNameAsync(userName);

        return userAccount != null;
    }
    private async Task<bool> IsEmailDuplicate(string email)
    {
        var userAccount = await _GENERIC_REPO.UsersManager.FindByEmailAsync(email);

        return userAccount != null;
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
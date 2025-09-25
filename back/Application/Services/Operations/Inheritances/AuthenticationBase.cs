using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Domain.Entities.Authentication;
using Application.Services.Operations.Auth.Dtos;
using Authentication.Exceptions;
using Microsoft.Extensions.Logging;
using Application.Services.Operations.Auth.Account.dtos;
using Microsoft.AspNetCore.Mvc;
using UnitOfWork.Persistence.Operations;
using Application.Exceptions;

namespace Application.Services.Operations.Auth;

public class AuthenticationBase
{
        private readonly IAuthServicesInjection _AUTH_SERVICES_INJECTION;

    public AuthenticationBase(
         IUnitOfWork GENERIC_REPO,
         IAuthServicesInjection AUTH_SERVICES_INJECTION
        )
    {
                _AUTH_SERVICES_INJECTION = AUTH_SERVICES_INJECTION;
    }

    private protected async Task<UserAccount> FindUserAsync(string userNameOrEmail)
    {
        return await _AUTH_SERVICES_INJECTION.UsersManager.FindByEmailAsync(userNameOrEmail) ?? await _AUTH_SERVICES_INJECTION.UsersManager.FindByNameAsync(userNameOrEmail) ?? throw new AuthServicesException(GlobalErrorsMessagesException.IsObjNull);
    }
    private protected async Task<UserAccount> FindUserByIdAsync(int id)
    {
        return await _AUTH_SERVICES_INJECTION.UsersManager.FindByIdAsync(id.ToString()) ?? throw new AuthServicesException(GlobalErrorsMessagesException.IsObjNull);
    }
    private protected async Task<IdentityResult> IsUserExist(string email)
    {
        var userFound = await _AUTH_SERVICES_INJECTION.UsersManager.FindByEmailAsync(email);

        return userFound != null ? IdentityResult.Success : IdentityResult.Failed([new IdentityError() { Description = "Usuário não encontrado." }]);
    }
    private protected async Task<bool> IsAccountLockedOutAsync(UserAccount userAccount)
    {
        return await _AUTH_SERVICES_INJECTION.UsersManager.IsLockedOutAsync(userAccount);
    }
    private protected async Task<bool> IsEmailConfirmedAsync(UserAccount userAccount)
    {
        return await _AUTH_SERVICES_INJECTION.UsersManager.IsEmailConfirmedAsync(userAccount);
    }

    private protected async Task<IdentityResult> IsPasswordValidAsync(UserAccount userAccount, string password)
    {
        var isValid = await _AUTH_SERVICES_INJECTION.UsersManager.CheckPasswordAsync(userAccount, password);

        if (isValid)
            return await _AUTH_SERVICES_INJECTION.UsersManager.ResetAccessFailedCountAsync(userAccount);
        else
        {
            await _AUTH_SERVICES_INJECTION.UsersManager.AccessFailedAsync(userAccount);
            return IdentityResult.Failed(new IdentityError() { Description = "User or password is invalid." });
        }
    }
    private protected void ResultUserCreation(bool userAccount, bool userProfile, string userEmail, string errosMsg)
    {
        if (!userAccount || !userProfile)
        {
            // _logger.LogError("User creation failed for {Email}. Errors: {Errors}", userEmail, string.Join(", ", errosMsg));

            throw new AuthServicesException(AuthErrorsMessagesException.ErrorWhenRegisterUserAccount);
        }
    }



    // private protected async Task<List<Claim>> BuildUserClaims(UserAccount userAccount)
    // {
    //     var getRoles = await _AUTH_SERVICES_INJECTION.UsersManager.GetRolesAsync(userAccount);

    //     var claims = new List<Claim>
    //     {

    //         new Claim("sub", userAccount.Id.ToString()),
    //         new Claim("amr", "Email")
    //         //  new Claim(ClaimTypes.NameIdentifier, userAccount.Id.ToString()),
    //         //   new Claim(ClaimTypes.Name, userAccount.UserName!),
    //         // new Claim(ClaimTypes.Name, userAccount.Email!),
    //     };

    //     foreach (var role in getRoles)
    //     {
    //         claims.Add(new Claim(ClaimTypes.Role, role));
    //     }

    //     return claims;
    // }




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


    private protected async Task<string> GenerateUrlTokenEmailConfirmation(UserAccount userAccount, string action, string controller)
    {
        var urlConfirmMail = _AUTH_SERVICES_INJECTION.UrlHelper.Action(action, controller, new
        {
            token = await _AUTH_SERVICES_INJECTION.UsersManager.GenerateEmailConfirmationTokenAsync(userAccount),
            email = userAccount.Email

        }) ?? throw new InvalidOperationException("Unable to generate email confirmation URL.");

        return urlConfirmMail;
    }
    private protected async Task<string> GenerateUrlTokenEmailChange(UserAccount userAccount, string action, string controller, string newEmail)
    {
        var urlConfirmMail = _AUTH_SERVICES_INJECTION.UrlHelper.Action(action, controller, new
        {
            token = await _AUTH_SERVICES_INJECTION.UsersManager.GenerateChangeEmailTokenAsync(userAccount, newEmail),
            email = userAccount.Email,
            id = userAccount.Id

        }) ?? throw new InvalidOperationException("Unable to generate email confirmation URL.");

        return urlConfirmMail;
    }
    // private protected async Task<string> GenerateTwoFactorTokenAsync(UserAccount userAccount, string action, string controller)
    // {
    //      var urlConfirmMail = _AUTH_SERVICES_INJECTION.UrlHelper.Action(action, controller, new
    //     {
    //         token = await _AUTH_SERVICES_INJECTION.UsersManager.GenerateTwoFactorTokenAsync(userAccount, "Email"),
    //         email = userAccount.Email

    //     }) ?? throw new InvalidOperationException("Unable to generate email Two Factor token URL.");

    //     return urlConfirmMail;
    // }
    private protected async Task<string> GenerateUrlTokenPasswordReset(UserAccount userAccount, string action, string controller)
    {
        var token = await _AUTH_SERVICES_INJECTION.UsersManager.GeneratePasswordResetTokenAsync(userAccount);

        var urlReset = _AUTH_SERVICES_INJECTION.UrlHelper.Action(action, controller, new { token, email = userAccount.Email, userName = userAccount.UserName }) ?? throw new AuthServicesException(AuthErrorsMessagesException.ErrorWhenGenerateEmailLink); ;

        return urlReset;
    }

    public async Task<string> UpdateUserRoles(UpdateUserRoleDto role)
    {
        var myUser = await FindUserAsync(role.UserName);

        if (role.Delete)
        {
            await _AUTH_SERVICES_INJECTION.UsersManager.RemoveFromRoleAsync(myUser, role.Role);

            return "Role removed";
        }
        else
        {
            await _AUTH_SERVICES_INJECTION.UsersManager.AddToRoleAsync(myUser, role.Role);
            return "Role Added";
        }
    }
    public async Task<IList<string>> GetRolesAsync(UserAccount userAccount) => await _AUTH_SERVICES_INJECTION.UsersManager.GetRolesAsync(userAccount);
    public async Task<IdentityResult> CreateRoleAsync(RoleDto roleDto) => await _AUTH_SERVICES_INJECTION.RolesManager.CreateAsync(new Role { Name = roleDto.Name, DisplayRole = roleDto.DisplayRole });
    public async Task<IdentityResult> IsUserExistCheckByEmailAsync(string email) => await IsUserExist(email);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="updateUserAccountEmailDto"></param>
    /// <returns></returns>
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

    public async Task SendEmailConfirmationAsync(DataConfirmEmail dataConfirmEmail, string body)
    {
        try
        {
            //var confirmationUrl = await GenerateEmailUrl(dataConfirmEmail);

            if (string.IsNullOrEmpty(dataConfirmEmail.TokenConfirmationUrl))
            {
                // _logger.LogError("Failed to generate email confirmation URL for {Email}", dataConfirmEmail.UserAccount.Email);
                throw new AuthServicesException(AuthErrorsMessagesException.ErrorWhenGenerateEmailLink);
            }

            // var formattedUrl = dataConfirmEmail.WelcomeMessage();
            // var formattedUrl = FormatEmailUrl(dataConfirmEmail.UrlFront, dataConfirmEmail.TokenConfirmationUrl, dataConfirmEmail.UrlBack, dataConfirmEmail.UserAccount);

            await SendAsync(To: dataConfirmEmail.UserAccount.Email, Subject: dataConfirmEmail.SubjectEmail, Body: body);
        }
        catch (Exception ex)
        {
            // _logger.LogError(ex, "Error sending confirmation email to {Email}", dataConfirmEmail.UserAccount.Email);
            throw;
        }
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
   
       private protected async Task<UserToken> CreateAuthenticationResponseAsync(UserAccount userAccount)
    {
        var claimsList = await BuildUserClaims(userAccount);
        var roles = await _AUTH_SERVICES_INJECTION.UsersManager.GetRolesAsync(userAccount);
        var token = await _AUTH_SERVICES_INJECTION.JwtHandler.GenerateUserToken(claimsList.Claims.ToList(), userAccount, roles);
        return token;
    }
    private protected async Task<UserToken> CreateTwoFactorResponse(UserAccount userAccount)
    {
        var claimsList = await BuildUserClaims(userAccount);
        var roles = await _AUTH_SERVICES_INJECTION.UsersManager.GetRolesAsync(userAccount);
        var token = await _AUTH_SERVICES_INJECTION.JwtHandler.GenerateUserToken(claimsList.Claims.ToList(), userAccount, roles);
        token.Action = "TwoFactor";
        return token;
    }
    // private protected async Task<List<Claim>> BuildUserClaims(UserAccount userAccount)
    // {
    //     var getRoles = await _AUTH_SERVICES_INJECTION.UsersManager.GetRolesAsync(userAccount);

    //     var claims = new List<Claim>
    //     {

    //         new Claim("sub", userAccount.Id.ToString()),
    //         new Claim("amr", "Email")
    //         //  new Claim(ClaimTypes.NameIdentifier, userAccount.Id.ToString()),
    //         //   new Claim(ClaimTypes.Name, userAccount.UserName!),
    //         // new Claim(ClaimTypes.Name, userAccount.Email!),
    //     };

    //     foreach (var role in getRoles)
    //         claims.Add(new Claim(ClaimTypes.Role, role));


    //     return claims;
    // }
    public async Task<ClaimsPrincipal> BuildUserClaims(UserAccount userAccount)
    {
        var getRoles = await _AUTH_SERVICES_INJECTION.UsersManager.GetRolesAsync(userAccount);

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

   
//    private ClaimsPrincipal StoreTwoFactorInfo(string userId, string provider)
// {
//     var identity = new ClaimsIdentity(IdentityConstants.TwoFactorUserIdScheme);
//     identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId));
//     identity.AddClaim(new Claim("amr", provider)); // authentication method reference
    
//     return new ClaimsPrincipal(identity);
// }
   
   
    /// <summary>
    /// 
    /// </summary>
    /// <param name="confirmRequestEmailChange"></param>
    /// <returns></returns>
    public async Task<IdentityResult> ConfirmYourEmailChangeAsync(ConfirmEmailChangeDto confirmRequestEmailChange)
    {
        var userAccount = await FindUserByIdAsync(confirmRequestEmailChange.Id);
        if (userAccount == null)
            return IdentityResult.Failed(new IdentityError { Description = "Usuário não encontrado." });

        var result = await _AUTH_SERVICES_INJECTION.UsersManager.ChangeEmailAsync(userAccount, confirmRequestEmailChange.Email, confirmRequestEmailChange.Token);
        if (result.Succeeded)
        {
            userAccount.UserName = confirmRequestEmailChange.Email;
            userAccount.Email = confirmRequestEmailChange.Email;
            await _AUTH_SERVICES_INJECTION.UsersManager.UpdateAsync(userAccount);
        }

        return result;
    }
    public async Task<IdentityResult> ConfirmEmailAddressAsync(ConfirmEmailDto confirmEmail)
    {
        var userAccout = await FindUserAsync(confirmEmail.Email);

        var result = await _AUTH_SERVICES_INJECTION.UsersManager.ConfirmEmailAsync(userAccout, confirmEmail.Token);

        return result;

    }
    public async Task<IdentityResult> ForgotPasswordAsync(ForgotPasswordDto forgotPassword)
    {
        var userAccount = await FindUserAsync(forgotPassword.Email);

        var genToken = await GenerateUrlTokenPasswordReset(userAccount, "ForgotPassword", "auth");

        var dataConfirmEmail = DataConfirmEmailMaker(userAccount, [genToken, "http://localhost:4200/password-reset", "api/auth/ForgotPassword", "I.M - Link para recadastramento de senha."]);

        await SendEmailConfirmationAsync(dataConfirmEmail, dataConfirmEmail.PasswordReset());

        return IdentityResult.Success;
    }
    public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto resetPassword)
    {
        var userAccount = await _AUTH_SERVICES_INJECTION.UsersManager.FindByEmailAsync(resetPassword.Email) ?? throw new AuthServicesException(AuthErrorsMessagesException.ObjectIsNull);

        IdentityResult identityResult = await _AUTH_SERVICES_INJECTION.UsersManager.ResetPasswordAsync(userAccount, resetPassword.Token, resetPassword.Password);

        if (identityResult.Succeeded)
        {
            userAccount.WillExpire = DateTime.MinValue;
            userAccount.LockoutEnd = DateTimeOffset.MinValue;
            userAccount.EmailConfirmed = true;
            await _AUTH_SERVICES_INJECTION.UsersManager.UpdateAsync(userAccount);
        }

        if (!identityResult.Succeeded) throw new AuthServicesException($"{AuthErrorsMessagesException.ResetPassword} - {identityResult}");

        return identityResult;
    }
    public async Task<IdentityResult> PasswdChangeAsync(UserAccount user, string CurrentPwd, string NewPwd)
    {
        return await _AUTH_SERVICES_INJECTION.UsersManager.ChangePasswordAsync(user, CurrentPwd, NewPwd);
    }
    private protected async Task ValidateUniqueUserCredentials(RegisterModelDto register)
    {
        if (await IsUserNameDuplicate(register.UserName))
        {
            // _logger.LogWarning("Duplicate username attempt: {UserName}", register.UserName);
            throw new AuthServicesException(AuthErrorsMessagesException.UserNameAlreadyRegisterd);
        }

        if (await IsEmailDuplicate(register.Email))
        {
            // _logger.LogWarning("Duplicate email attempt: {Email}", register.Email);
            throw new AuthServicesException(AuthErrorsMessagesException.EmailAlreadyRegisterd);
        }
    }
    private async Task<bool> IsUserNameDuplicate(string userName)
    {
        var userAccount = await _AUTH_SERVICES_INJECTION.UsersManager.FindByNameAsync(userName);

        return userAccount != null;
    }
    private async Task<bool> IsEmailDuplicate(string email)
    {
        var userAccount = await _AUTH_SERVICES_INJECTION.UsersManager.FindByEmailAsync(email);

        return userAccount != null;
    }


    private protected static async Task SendAsync(string To = "register@nostopti.com.br", string From = "register@nostopti.com.br", string DisplayName = "Sonny System",
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
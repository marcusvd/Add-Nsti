using Domain.Entities.Authentication;
using Authentication.Exceptions;
using Application.Shared.Validators;
using Application.Auth.UsersAccountsServices.EmailUsrAccountServices.Services;
using Microsoft.AspNetCore.Identity;
using Application.EmailServices.Exceptions;

namespace Application.Auth.Extends;

public abstract class AuthenticationBase : IAuthenticationBase
{

    // private readonly UserManager<UserAccount> _userManager;
    // private readonly EmailUserAccountServices _emailUserAccountServices;

    public AuthenticationBase(
            // UserManager<UserAccount> userManager,
            // EmailUserAccountServices emailUserAccountServices
    )
    {
        // _userManager = userManager;
        // _emailUserAccountServices = emailUserAccountServices;
    }

    public string IsValidEmail(string email) => GenericValidators.IsValidEmail(email) ? email : throw new AuthServicesException("Invalid email");
    public int ValidateUserId(int userId) => !(userId <= 0) ? userId : throw new AuthServicesException("Invalid id");

    // public async Task ValidateUserAccountAsync(UserAccount userAccount)
    // {
    //     if (await _userManager.IsLockedOutAsync(userAccount))
    //     {
    //         await _emailUserAccountServices.NotifyAccountLockedAsync(userAccount);
    //         throw new AuthenticationBaseException(AuthenticationBaseMessagesException.UserIsLocked);
    //     }

    //     if (!await _userManager.IsEmailConfirmedAsync(userAccount))
    //     {
    //         await _emailUserAccountServices.ResendConfirmEmailAsync(userAccount.Email);
    //         throw new AuthenticationBaseException(AuthenticationBaseMessagesException.EmailIsNotConfirmed);
    //     }
    // }


    // private protected async Task<bool> ValidateAccountStatusAsync(UserAccount userAccount)
    // {
    //     if (await _authServicesInjection.UsersManager.IsLockedOutAsync(userAccount))
    //         return true;

    //     if (!await _authServicesInjection.UsersManager.IsEmailConfirmedAsync(userAccount))
    //         throw new AuthServicesException(AuthErrorsMessagesException.EmailIsNotConfirmed);

    //     return false;
    // }


    // public ApiResponse<T> ErrorsHandler<T>(List<string> errors, bool result, string message, T obj)
    // {
    //     var errorsListFormat = new List<string>();
    //     var response = ApiResponse<T>.Response(result, message, obj);

    //     if (!result)
    //     {
    //         errors.ForEach(x =>
    //         {
    //             errorsListFormat.Add(x);

    //         });

    //         response.Errors = errorsListFormat;
    //     }

    //     return response;
    // }

    // public async Task<UserAccount> FindUserAsync(string userNameOrEmail)
    // {
    //     return await _authServicesInjection.UsersManager.FindByEmailAsync(userNameOrEmail) ?? await _authServicesInjection.UsersManager.FindByNameAsync(userNameOrEmail) ?? throw new AuthServicesException(GlobalErrorsMessagesException.IsObjNull);
    // }
    // public async Task<UserAccount> FindUserByIdAsync(int id)
    // {
    //     return await _authServicesInjection.UsersManager.FindByIdAsync(id.ToString()) ?? throw new AuthServicesException(GlobalErrorsMessagesException.IsObjNull);
    // }
    // public async Task<IdentityResult> IsUserExist(string email)
    // {
    //     var userFound = await _authServicesInjection.UsersManager.FindByEmailAsync(email);

    //     return userFound != null ? IdentityResult.Success : IdentityResult.Failed([new IdentityError() { Description = "Usuário não encontrado." }]);
    // }
    // public async Task<bool> IsAccountLockedOutAsync(UserAccount userAccount)
    // {
    //     return await _authServicesInjection.UsersManager.IsLockedOutAsync(userAccount);
    // }
    // public async Task<bool> IsEmailConfirmedAsync(UserAccount userAccount)
    // {
    //     return await _authServicesInjection.UsersManager.IsEmailConfirmedAsync(userAccount);
    // }
    // public async Task<IdentityResult> CheckPasswordAsync(UserAccount userAccount, string password)
    // {
    //     var isValid = await _authServicesInjection.UsersManager.CheckPasswordAsync(userAccount, password);

    //     if (isValid)
    //         return await _authServicesInjection.UsersManager.ResetAccessFailedCountAsync(userAccount);
    //     else
    //     {
    //         await _authServicesInjection.UsersManager.AccessFailedAsync(userAccount);
    //         return IdentityResult.Failed(new IdentityError() { Description = "User or password is invalid." });
    //     }
    // }
    // public async Task<IdentityResult> PasswordSignInAsync(UserAccount userAccount, string password, bool isPersistent = true, bool lockoutOnFailure = true)
    // {
    //     var isValid = await _authServicesInjection.SignInManager.PasswordSignInAsync(userAccount, password, isPersistent, lockoutOnFailure);

    //     if (isValid.Succeeded || isValid.RequiresTwoFactor)
    //         return await _authServicesInjection.UsersManager.ResetAccessFailedCountAsync(userAccount);
    //     else
    //     {
    //         await _authServicesInjection.UsersManager.AccessFailedAsync(userAccount);
    //         return IdentityResult.Failed(new IdentityError() { Description = "User or password is invalid." });
    //     }
    // }
    // public RoleDto CreateRole(string role, string DisplayRole)
    // {
    //     return new RoleDto
    //     {
    //         Name = role,
    //         DisplayRole = DisplayRole,
    //     };
    // // }
    // public async Task<string[]> UpdateUserRoles(UpdateUserRoleDto[] roles)
    // {

    //     var results = new List<string>();

    //     foreach (var role in roles)
    //     {
    //         var userAccount = await FindUserAsync(role.UserName);

    //         var rolesFromDb = await _authServicesInjection.UsersManager.GetRolesAsync(userAccount);

    //         if (role.Delete)
    //         {
    //             await _authServicesInjection.UsersManager.RemoveFromRoleAsync(userAccount, role.Role);
    //             results.Add("Role removed");
    //         }
    //         else
    //         {
    //             if (!await _authServicesInjection.UsersManager.IsInRoleAsync(userAccount, role.Role))
    //                 await _authServicesInjection.UsersManager.AddToRoleAsync(userAccount, role.Role);

    //             results.Add("Role Added");
    //         }
    //     }

    //     return results.ToArray();

    // }
    // public async Task<IList<string>> GetRolesAsync(UserAccount userAccount) => await _authServicesInjection.UsersManager.GetRolesAsync(userAccount);
    // public async Task<IdentityResult> CreateRoleAsync(RoleDto roleDto) => await _authServicesInjection.RolesManager.CreateAsync(new Role { Name = roleDto.Name, DisplayRole = roleDto.DisplayRole });





    // public async Task<IdentityResult> IsUserExistCheckByEmailAsync(string email) => await IsUserExist(email);

    // public async Task SendEmailConfirmationAsync(DataConfirmEmail dataConfirmEmail, string body)
    // {
    //     try
    //     {
    //         //var confirmationUrl = await GenerateEmailUrl(dataConfirmEmail);

    //         if (string.IsNullOrEmpty(dataConfirmEmail.TokenConfirmationUrl))
    //         {
    //             // _logger.LogError("Failed to generate email confirmation URL for {Email}", dataConfirmEmail.UserAccount.Email);
    //             throw new AuthServicesException(AuthErrorsMessagesException.ErrorWhenGenerateEmailLink);
    //         }

    //         // var formattedUrl = dataConfirmEmail.WelcomeMessage();
    //         // var formattedUrl = FormatEmailUrl(dataConfirmEmail.UrlFront, dataConfirmEmail.TokenConfirmationUrl, dataConfirmEmail.UrlBack, dataConfirmEmail.UserAccount);

    //         await SendAsync(To: dataConfirmEmail.UserAccount.Email, Subject: dataConfirmEmail.SubjectEmail, Body: body);
    //     }
    //     catch (Exception ex)
    //     {
    //         // _logger.LogError(ex, "Error sending confirmation email to {Email}", dataConfirmEmail.UserAccount.Email);
    //         throw;
    //     }
    // }
    // public DataConfirmEmail DataConfirmEmailMaker(UserAccount user, string[] dataConfirmation)
    // {
    //     return new DataConfirmEmail()
    //     {
    //         UserAccount = user,
    //         TokenConfirmationUrl = dataConfirmation[0],
    //         UrlFront = dataConfirmation[1],
    //         UrlBack = dataConfirmation[2],
    //         SubjectEmail = dataConfirmation[3]
    //     };
    // }


    // public async Task<IdentityResult> ForgotPasswordAsync(ForgotPasswordDto forgotPassword)
    // {
    //     var userAccount = await FindUserAsync(forgotPassword.Email);

    //     var genToken = await GenerateUrlTokenPasswordReset(userAccount, "ForgotPassword", "auth");

    //     var dataConfirmEmail = DataConfirmEmail.DataConfirmEmailMaker(userAccount, [genToken, "http://localhost:4200/password-reset", "api/auth/ForgotPassword", "I.M - Link para recadastramento de senha."]);

    //     await SendEmailConfirmationAsync(dataConfirmEmail, dataConfirmEmail.PasswordReset());

    //     return IdentityResult.Success;
    // }
    // public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto resetPassword)
    // {
    //     var userAccount = await _authServicesInjection.UsersManager.FindByEmailAsync(resetPassword.Email) ?? throw new AuthServicesException(AuthErrorsMessagesException.ObjectIsNull);

    //     IdentityResult identityResult = await _authServicesInjection.UsersManager.ResetPasswordAsync(userAccount, resetPassword.Token, resetPassword.Password);

    //     if (identityResult.Succeeded)
    //     {
    //         await _authServicesInjection.UsersManager.ResetAccessFailedCountAsync(userAccount);
    //         userAccount.WillExpire = DateTime.MinValue;
    //         userAccount.LockoutEnd = DateTimeOffset.MinValue;
    //         userAccount.EmailConfirmed = true;
    //         await _authServicesInjection.UsersManager.UpdateAsync(userAccount);
    //     }

    //     if (!identityResult.Succeeded) throw new AuthServicesException($"{AuthErrorsMessagesException.ResetPassword} - {identityResult}");

    //     return identityResult;
    // }
    // public async Task<IdentityResult> PasswdChangeAsync(UserAccount user, string CurrentPwd, string NewPwd)
    // {
    //     return await _authServicesInjection.UsersManager.ChangePasswordAsync(user, CurrentPwd, NewPwd);
    // }
    // private protected async Task ValidateUniqueUserCredentials(string userName, string email)
    // {
    //     if (await IsUserNameDuplicate(userName))
    //     {
    //         // _logger.LogWarning("Duplicate username attempt: {UserName}", register.UserName);
    //         throw new AuthServicesException(AuthErrorsMessagesException.UserNameAlreadyRegisterd);
    //     }

    //     if (await IsEmailDuplicate(email))
    //     {
    //         // _logger.LogWarning("Duplicate email attempt: {Email}", register.Email);
    //         throw new AuthServicesException(AuthErrorsMessagesException.EmailAlreadyRegisterd);
    //     }
    // }




    // public async Task SendAsync(string To = "register@nostopti.com.br", string From = "register@nostopti.com.br",
    // string Subject = "Test Subject", string Body = "Test", string MailServer = "smtp.nostopti.com.br",
    //  int Port = 587, bool IsUseSsl = false, string UserName = "register@nostopti.com.br", string Password = "Nsti$2024")
    // {
    //     var message = new MailMessage(From, To, Subject, Body);
    //     message.IsBodyHtml = true;

    //     SmtpClient SmtpClient = new SmtpClient(MailServer)
    //     {
    //         Port = Port,
    //         Credentials = new NetworkCredential(UserName, Password),
    //         EnableSsl = IsUseSsl
    //     };
    //     SmtpClient.SendCompleted += (s, e) =>
    //    {
    //        SmtpClient.Dispose();
    //        message.Dispose();
    //    };
    //     try
    //     {
    //         SmtpClient.SendAsync(message, null);
    //         await Task.CompletedTask;
    //     }
    //     catch (SmtpFailedRecipientException ex)
    //     {
    //         throw new Exception($"{ex}");
    //     }
    // }




}
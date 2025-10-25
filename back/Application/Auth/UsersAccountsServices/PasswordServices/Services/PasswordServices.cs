using Microsoft.AspNetCore.Identity;
using Application.Auth.UsersAccountsServices.PasswordServices.Extends;
using AApplication.Auth.UsersAccountsServices.PasswordServices.Dtos;
using Domain.Entities.Authentication;
using Application.Auth.IdentityTokensServices;
using Application.Auth.UsersAccountsServices.EmailUsrAccountServices.dtos;
using Authentication.Exceptions;
using Application.Shared.Dtos;
using Application.Auth.UsersAccountsServices.PasswordServices.Exceptions;
using Application.EmailServices.Services;
using Application.Auth.UsersAccountsServices.Auth;
using Application.Helpers.Inject;
using Application.Exceptions;
using Application.Auth.Dtos;
using System.Reflection.Metadata;

namespace Application.Auth.UsersAccountsServices.PasswordServices.Services;

public class PasswordServices : PasswordServicesBase, IPasswordServices
{
    private readonly IIdentityTokensServices _identityTokensServices;
    private readonly ISmtpServices _emailService;
    private readonly UserAccountAuthServices _userAccountAuthServices;
    private readonly UserManager<UserAccount> _userManager;
    private readonly SignInManager<UserAccount> _signInManager;
    private readonly IValidatorsInject _validatorsInject;

    public PasswordServices(
          IIdentityTokensServices identityTokensServices,
          ISmtpServices emailService,
          UserAccountAuthServices userAccountAuthServices,
          UserManager<UserAccount> userManager,
          SignInManager<UserAccount> signInManager,
          IValidatorsInject validatorsInject
      )
    {
        _identityTokensServices = identityTokensServices;
        _emailService = emailService;
        _userAccountAuthServices = userAccountAuthServices;
        _userManager = userManager;
        _signInManager = signInManager;
        _validatorsInject = validatorsInject;
    }


    public async Task<ApiResponse<string>> ForgotPasswordAsync(ForgotPasswordDto dto)
    {
        string email = IsValidEmail(dto.Email);

        var userAccount = await _userAccountAuthServices.GetUserAccountByEmailAsync(email);

        var genToken = await _identityTokensServices.GenerateUrlTokenPasswordReset(userAccount, "ForgotPassword", "auth");

        var dataConfirmEmail = DataConfirmEmail.DataConfirmEmailMaker(userAccount, [genToken, "http://localhost:4200/password-reset", "api/auth/ForgotPassword", "I.M - Link para recadastramento de senha."]);

        await _emailService.SendTokensEmailAsync(dataConfirmEmail, dataConfirmEmail.PasswordReset());

        return ApiResponse<string>.Response([$@"{PasswordServicesMessagesException.ForgotPassword} - {userAccount.Email}"], dataConfirmEmail != null && genToken.Length > 0, "ForgotPasswordAsync", userAccount.Email);
    }
    public async Task<ApiResponse<IdentityResult>> ResetPasswordAsync(ResetPasswordDto resetPassword)
    {
        var userAccount = await _userManager.FindByEmailAsync(resetPassword.Email) ?? throw new PasswordServicesException(AuthErrorsMessagesException.ObjectIsNull);

        IdentityResult identityResult = await _userManager.ResetPasswordAsync(userAccount, resetPassword.Token, resetPassword.Password);

        if (!identityResult.Succeeded) throw new PasswordServicesException($"{PasswordServicesMessagesException.ResetPassword} - {identityResult}");

        await _userManager.ResetAccessFailedCountAsync(userAccount);

        userAccount = AssignValueUserAccount(userAccount);

        await _userManager.UpdateAsync(userAccount);

        return ApiResponse<IdentityResult>.Response([""], identityResult.Succeeded, "EmailChangeResponse", identityResult);
    }
    private UserAccount AssignValueUserAccount(UserAccount userAccount)
    {
        userAccount.WillExpire = DateTime.MinValue;
        userAccount.LockoutEnd = DateTimeOffset.MinValue;
        userAccount.EmailConfirmed = true;

        return userAccount;
    }

    public async Task<ApiResponse<IdentityResult>> PasswordSignInAsync(UserAccount userAccount, string password, bool isPersistent = true, bool lockoutOnFailure = true)
    {
        var isValid = await _signInManager.PasswordSignInAsync(userAccount, password, isPersistent, lockoutOnFailure);

        if (isValid.Succeeded || isValid.RequiresTwoFactor)
        {
            var identityResult = await _userManager.ResetAccessFailedCountAsync(userAccount);

            return ApiResponse<IdentityResult>.Response([""], identityResult.Succeeded, "PasswordSignInAsync", identityResult);
        }
        else
        {
            var identityResult = await _userManager.AccessFailedAsync(userAccount);
            return ApiResponse<IdentityResult>.Response([""], identityResult.Succeeded, "PasswordSignInAsync", identityResult);
        }
    }
    public async Task<ApiResponse<IdentityResult>> CheckPasswordAsync(UserAccount userAccount, string password)
    {
        var isValid = await _userManager.CheckPasswordAsync(userAccount, password);

        if (isValid)
        {
            var identityResult = await _userManager.ResetAccessFailedCountAsync(userAccount);
            return ApiResponse<IdentityResult>.Response([""], identityResult.Succeeded, "PasswordSignInAsync", identityResult);
        }
        else
        {
            var identityResult = await _userManager.AccessFailedAsync(userAccount);
            return ApiResponse<IdentityResult>.Response([""], identityResult.Succeeded, "PasswordSignInAsync", identityResult);
        }
    }
    public async Task<ApiResponse<IdentityResult>> PasswordChangeAsync(PasswordChangeDto passwordChange)
    {
        _validatorsInject.GenericValidators.IsObjNull(passwordChange);

        int id = ValidateUserId(passwordChange.UserId);

        passwordChange.PasswordSimpleValidate();

        var userFromDb = await _userAccountAuthServices.GetUserAccountByUserIdAsync(id);

        _validatorsInject.GenericValidators.IsObjNull(userFromDb);

        _validatorsInject.GenericValidators.Validate(userFromDb.Id, passwordChange.UserId, GlobalErrorsMessagesException.IdIsDifferentFromEntityUpdate);

        return await PwdChangeAsync(userFromDb, passwordChange.CurrentPwd, passwordChange.Password);
    }
    private async Task<ApiResponse<IdentityResult>> PwdChangeAsync(UserAccount userAccount, string currentPwd, string newPwd)
    {
        var identityResult = await _userManager.ChangePasswordAsync(userAccount, currentPwd, newPwd);
        return ApiResponse<IdentityResult>.Response([""], identityResult.Succeeded, "PasswdChangeAsync", identityResult);
    }
    public async Task<ApiResponse<IdentityResult>> MarkPasswordExpireAsync(PasswordWillExpiresDto passwordWillExpires)
    {

        int id = ValidateUserId(passwordWillExpires.UserId);

        var userAccount = await _userAccountAuthServices.GetUserAccountByUserIdAsync(id);

        _validatorsInject.GenericValidators.IsObjNull(userAccount);

        if (passwordWillExpires.UserId <= 0) throw new AuthServicesException(GlobalErrorsMessagesException.IdIsNull);

        return await WillExpireAsync(userAccount, passwordWillExpires.WillExpires);
    }
    private async Task<ApiResponse<IdentityResult>> WillExpireAsync(UserAccount userAccount, bool expires, string newPwd = "123456")
    {
        var genToken = await _userManager.GeneratePasswordResetTokenAsync(userAccount);

        bool IsPwdReseted = (await _userManager.ResetPasswordAsync(userAccount, genToken, newPwd)).Succeeded;

        userAccount = AssignValuesWillExpire(userAccount, expires && IsPwdReseted);

        var identityResult = await _userManager.UpdateAsync(userAccount);

        return ApiResponse<IdentityResult>.Response([""], identityResult.Succeeded, "WillExpireAsync", identityResult);
    }
    private UserAccount AssignValuesWillExpire(UserAccount userAccount, bool expires)
    {
        if (expires)
        {
            userAccount.WillExpire = DateTime.Now;
            userAccount.EmailConfirmed = true;
        }
        else
            userAccount.WillExpire = DateTime.MinValue;

        return userAccount;
    }

    public async Task<ApiResponse<IdentityResult>> ManualAccountLockedOut(AccountLockedOutManualDto emailConfirmManual)
    {

        string emailValidated = IsValidEmail(emailConfirmManual.Email);

        var userAccount = await _userAccountAuthServices.GetUserAccountByEmailAsync(emailValidated);

        userAccount = AssignValuesManualAccountLockedOut(userAccount, emailConfirmManual.AccountLockedOut);

        var identityResult = await _userManager.UpdateAsync(userAccount);

        return ApiResponse<IdentityResult>.Response([""], identityResult.Succeeded, "WillExpireAsync", identityResult);
    }

    private UserAccount AssignValuesManualAccountLockedOut(UserAccount userAccount, bool isAccountLockedOut)
    {
        if (isAccountLockedOut)
            userAccount.LockoutEnd = DateTime.Now.AddYears(10);
        else
            userAccount.LockoutEnd = DateTime.MinValue;

        return userAccount;
    }


    public async Task<ApiResponse<IdentityResult>> SetStaticPassword(ResetStaticPasswordDto reset)
    {
        string emailValidated = IsValidEmail(reset.Email);

        var userAccount = await _userAccountAuthServices.GetUserAccountByEmailAsync(emailValidated);

        var genToken = await _userManager.GeneratePasswordResetTokenAsync(userAccount);

        userAccount = AssignValuesSetStaticPassword(userAccount);

        return await ResponseSetStaticPassword(userAccount, genToken, reset.Password);
    }

    private UserAccount AssignValuesSetStaticPassword(UserAccount userAccount)
    {
        userAccount.WillExpire = DateTime.MinValue;

        userAccount.LockoutEnd = DateTimeOffset.MinValue;

        return userAccount;
    }

    private async Task<ApiResponse<IdentityResult>> ResponseSetStaticPassword(UserAccount userAccount, string token, string password)
    {
        bool updateResult = (await _userManager.UpdateAsync(userAccount)).Succeeded;

        var result = new IdentityResult();

        if (updateResult)
        {
            result = await _userManager.ResetPasswordAsync(userAccount, token, password);

            return ApiResponse<IdentityResult>.Response([""], result.Succeeded, "A senha foi modificada.", result);
        }
        else
            return ApiResponse<IdentityResult>.Response([""], updateResult, "ResponseSetStaticPassword", result);
    }

    public async Task<ApiResponse<bool>> IsPasswordExpiresAsync(int userId)
    {
        int id = ValidateUserId(userId);
        
        var userAccount = await _userAccountAuthServices.GetUserAccountByUserIdAsync(id);

        if (userAccount.WillExpire.Year == DateTime.MinValue.Year)
            return ApiResponse<bool>.Response([""], false, "ResponseSetStaticPassword", false);
        else
            return ApiResponse<bool>.Response([""], true, "ResponseSetStaticPassword", true);
    }
}


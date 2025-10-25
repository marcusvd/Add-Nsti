
using Microsoft.AspNetCore.Identity;

using UnitOfWork.Persistence.Operations;
using Application.Auth.UsersAccountsServices.EmailUsrAccountServices.dtos;
using Application.Auth.UsersAccountsServices.EmailUsrAccountServices.Exceptions;
using Application.Auth.IdentityTokensServices;
using Domain.Entities.Authentication;
using Application.Shared.Dtos;
using Application.EmailUsrAccountServices.Extensions;
using Application.UsersAccountsServices.Dtos.Extends;
using Application.EmailServices.Services;

namespace Application.Auth.UsersAccountsServices.EmailUsrAccountServices.Services;

public class EmailUserAccountServices : EmailUserAccountBase, IEmailUserAccountServices
{
    private readonly IUserAccountAuthServices _userAccountAuthServices;
    private readonly UserManager<UserAccount> _userManager;
    private readonly IIdentityTokensServices _identityTokensServices;
    private readonly ISmtpServices _emailService;


    public EmailUserAccountServices(
           IUserAccountAuthServices userAccountAuthServices,
           UserManager<UserAccount> userManager,
           IIdentityTokensServices identityTokensServices,
           ISmtpServices emailService
      ) 
    {
        _userAccountAuthServices = userAccountAuthServices;
        _identityTokensServices = identityTokensServices;
        _emailService = emailService;
        _userManager = userManager;
    }

    public async Task<ApiResponse<IdentityResult>> ConfirmEmailAddressAsync(ConfirmEmailDto dto)
    {
        string email = IsValidEmail(dto.Email);

        var userAccount = await  _userAccountAuthServices.GetUserAccountByEmailAsync(email);

        var result = await _userManager.ConfirmEmailAsync(userAccount, dto.Token);

        return ApiResponse<IdentityResult>.Response([$@"{EmailUserAccountMessagesException.confirmEmail} - {userAccount.Email}"], result.Succeeded, "ConfirmEmailAddressAsync", result);
    }
    public async Task<ApiResponse<IdentityResult>> ManualConfirmEmailAddress(EmailConfirmManualDto dto)
    {
        string email = IsValidEmail(dto.Email);

        var userAccount = await _userAccountAuthServices.GetUserAccountByEmailAsync(email);

        userAccount = AssignValueEmailConfirmed(userAccount, dto.EmailConfirmed);

        var result = await _userManager.UpdateAsync(userAccount);

        return ApiResponse<IdentityResult>.Response([$@"{EmailUserAccountMessagesException.confirmEmail} - {userAccount.Email}"], result.Succeeded, "ManualConfirmEmailAddress", result);
    }
    private UserAccount AssignValueEmailConfirmed(UserAccount userAccount, bool isConformed)
    {
        userAccount.EmailConfirmed = isConformed;
        return userAccount;
    }
    public async Task<ApiResponse<string>> RequestEmailChangeAsync(RequestEmailChangeDto dto)
    {
        string email = IsValidEmail(dto.OldEmail);

        var userAccount = await _userAccountAuthServices.GetUserAccountByEmailAsync(email);

        userAccount!.InitiateEmailChange(dto.NewEmail);

        var genToken = await _identityTokensServices.GenerateUrlTokenEmailChange(userAccount, "ConfirmRequestEmailChange", "auth", dto.NewEmail);

        var dataConfirmEmail = DataConfirmEmail.DataConfirmEmailMaker(userAccount, [genToken, "http://localhost:4200/confirm-request-email-change", "api/auth/ConfirmRequestEmailChange", "I.M - Link para confirmação mudança de email."]);

        await _emailService.SendTokensEmailAsync(dataConfirmEmail, dataConfirmEmail.EmailUpdated());

        return ApiResponse<string>.Response([$@"{EmailUserAccountMessagesException.emailChange} - {userAccount.Email}"], dataConfirmEmail != null && genToken.Length > 0, "RequestEmailChangeAsync", dto.NewEmail);
    }
    public async Task<ApiResponse<IdentityResult>> ConfirmYourEmailChangeAsync(ConfirmEmailChangeDto dto)
    {
        int id = ValidateUserId(dto.Id);

        var userAccount = await _userAccountAuthServices.GetUserAccountByUserIdAsync(id);

        var result = await _userManager.ChangeEmailAsync(userAccount, dto.Email, dto.Token);

        string email = IsValidEmail(dto.Email);

        if (result.Succeeded)
            await EmailChangeResponse(userAccount, email);

        return await EmailChangeResponse(userAccount, email);
    }
    private async Task<ApiResponse<IdentityResult>> EmailChangeResponse(UserAccount userAccount, string email)
    {
        userAccount.UserName = email;
        userAccount.Email = email;

        var result = await _userManager.UpdateAsync(userAccount);

        return ApiResponse<IdentityResult>.Response([$@"Error when trying to change user email: {email}"], result.Succeeded, "EmailChangeResponse", result);
    }
    public async Task<ApiResponse<string>> ResendConfirmEmailAsync(string emailParam)
    {
        string email = IsValidEmail(emailParam);

        var userAccount = await _userAccountAuthServices.GetUserAccountByEmailAsync(email);

        await _userAccountAuthServices.ValidateUserAccountAsync(userAccount);

        var genToken = _identityTokensServices.GenerateUrlTokenEmailConfirmation(userAccount, "ConfirmEmailAddress", "auth");

        var dataConfirmEmail = DataConfirmEmail.DataConfirmEmailMaker(userAccount, [await genToken, "http://localhost:4200/confirm-email", "api/auth/ConfirmEmailAddress", "I.M - Link para confirmação de e-mail"]);

        await _emailService.SendTokensEmailAsync(dataConfirmEmail, dataConfirmEmail.WelcomeMessage());

        return ApiResponse<string>.Response([$@"Error when trying to change user email: {email}"], true, "ResendConfirmEmailAsync", email);
    }
      public async Task NotifyAccountLockedAsync(UserAccount userAccount)
    {
        try
        {
            var dataConfirmEmail = DataConfirmEmail.DataConfirmEmailMaker(userAccount, ["invalid", "http://localhost:4200/confirm-email", "api/auth/ConfirmEmailAddress", "I.M - Link para confirmação de e-mail"]);

            await _emailService.SendTokensEmailAsync(dataConfirmEmail, dataConfirmEmail.AccountBlockedMessage());
        }
        catch (Exception ex)
        {
            // _logger.LogError(ex, "Failed to send account locked notification to {Email}", userAccount.Email);
        }
    }



}
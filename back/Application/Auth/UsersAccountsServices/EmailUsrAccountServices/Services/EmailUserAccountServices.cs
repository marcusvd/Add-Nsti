using Microsoft.AspNetCore.Identity;
using Application.Auth.UsersAccountsServices.EmailUsrAccountServices.dtos;
using Application.Auth.UsersAccountsServices.EmailUsrAccountServices.Exceptions;
using Application.Auth.IdentityTokensServices;
using Domain.Entities.Authentication;
using Application.Shared.Dtos;
using Application.EmailUsrAccountServices.Extensions;
using Application.UsersAccountsServices.Dtos.Extends;
using Application.EmailServices.Services;
using Application.Auth.JwtServices;
using Application.Exceptions;
using System.Security.Claims;

namespace Application.Auth.UsersAccountsServices.EmailUsrAccountServices.Services;

public class EmailUserAccountServices : EmailUserAccountBase, IEmailUserAccountServices
{
    // private readonly IUserAccountAuthServices _userAccountAuthServices;
    private readonly UserManager<UserAccount> _userManager;
    private readonly IIdentityTokensServices _identityTokensServices;
    private readonly ISmtpServices _emailService;
    private readonly IJwtServices _jwtServices;


    public EmailUserAccountServices(
           //    IUserAccountAuthServices userAccountAuthServices,
           UserManager<UserAccount> userManager,
           IIdentityTokensServices identityTokensServices,
           ISmtpServices emailService,
           IJwtServices jwtServices
      )
    {
        // _userAccountAuthServices = userAccountAuthServices;
        _userManager = userManager;
        _identityTokensServices = identityTokensServices;
        _emailService = emailService;
        _jwtServices = jwtServices;
    }

    public async Task<ApiResponse<IdentityResult>> ConfirmEmailAddressAsync(ConfirmEmailDto dto)
    {
        string email = IsValidEmail(dto.Email);

        var userAccount = await _userManager.FindByEmailAsync(email);

        var result = await _userManager.ConfirmEmailAsync(userAccount, dto.Token);

        return ApiResponse<IdentityResult>.Response([$@"{EmailUserAccountMessagesException.confirmEmail} - {userAccount.Email}"], result.Succeeded, "ConfirmEmailAddressAsync", result);
    }
    public async Task<ApiResponse<UserToken>> FirstEmailConfirmationCheckTokenAsync(ConfirmEmailDto dto)
    {
        string email = IsValidEmail(dto.Email);

        var usrTkn = new UserToken()
        {
            Id = 0,
            BusinessId = 0,
            Authenticated = true,
            Expiration = DateTime.MinValue,
            Token = dto.Token,
            UserName = email,
            Email = email,
            Action = "FirstRegister",
            Roles = ["first"]
        };

        var userClaims = _jwtServices.ValidateJwtToken(usrTkn.Token);

        if (userClaims != null)
        {
            var emailFromClaim = userClaims.FindFirst(ClaimTypes.Email)?.Value;
            
        }


        return ApiResponse<UserToken>.Response([$@"{EmailUserAccountMessagesException.confirmEmail} - {usrTkn.Email}"], usrTkn.Authenticated, "ManualConfirmEmailAddress", usrTkn);
    }
    public async Task<ApiResponse<IdentityResult>> ManualConfirmEmailAddress(EmailConfirmManualDto dto)
    {
        string email = IsValidEmail(dto.Email);

        var userAccount = await _userManager.FindByEmailAsync(email);

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

        var userAccount = await _userManager.FindByEmailAsync(email);

        userAccount!.InitiateEmailChange(dto.NewEmail);

        var genToken = await _identityTokensServices.GenerateUrlTokenEmailChange(userAccount, "ConfirmRequestEmailChange", "auth", dto.NewEmail);

        var dataConfirmEmail = DataConfirmEmail.DataConfirmEmailMaker(userAccount, [genToken, "http://localhost:4200/confirm-request-email-change", "api/auth/ConfirmRequestEmailChange", "I.M - Link para confirmação mudança de email."]);

        await _emailService.SendTokensEmailAsync(dataConfirmEmail, dataConfirmEmail.EmailUpdated());

        return ApiResponse<string>.Response([$@"{EmailUserAccountMessagesException.emailChange} - {userAccount.Email}"], dataConfirmEmail != null && genToken.Length > 0, "RequestEmailChangeAsync", dto.NewEmail);
    }
    public async Task<ApiResponse<IdentityResult>> ConfirmYourEmailChangeAsync(ConfirmEmailChangeDto dto)
    {
        int id = ValidateUserId(dto.Id);

        var userAccount = await _userManager.FindByIdAsync(id.ToString());

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
    public async Task<ApiResponse<string>> SendConfirmEmailAsync(bool registerResult, UserAccount userAccount)
    {
        if (registerResult)
        {
            var genToken = _identityTokensServices.GenerateUrlTokenEmailConfirmation(userAccount, JwtSettings.ActionConfirmEmailAddress, JwtSettings.EmailUserAccountController);

            var dataConfirmEmail = DataConfirmEmail.DataConfirmEmailMaker(userAccount, [await genToken, "http://localhost:4200/confirm-email", @$"api/{JwtSettings.EmailUserAccountController}/{JwtSettings.ActionConfirmEmailAddress}", "I.M - Link para confirmação de e-mail"]);

            await _emailService.SendTokensEmailAsync(dataConfirmEmail, dataConfirmEmail.WelcomeMessage());

            bool result = genToken != null && dataConfirmEmail != null;

            return ApiResponse<string>.Response([$@"Error when trying to resend email confirmation: {userAccount.Email}"], result, "Email confirmation was sent successfully sent.", userAccount.Email);
        }
        return ApiResponse<string>.Response([$@"Error when trying to send email confirmation: {userAccount.Email}"], true, "SendConfirmEmailAsync", userAccount.Email);
    }
    public async Task<ApiResponse<UserToken>> FirstEmailConfirmationAsync(UserToken userToken)
    {
        var dataConfirmEmail = DataConfirmEmail.DataConfirmEmailMaker(new UserAccount() { UserProfileId = "", DisplayUserName = "", Email = userToken.Email ?? "invalid@invalid.com.br", }, [userToken.Token ?? "Invalid Token!", "http://localhost:4200/confirm-email", userToken.Email ?? "invalid@invalid.com.br", "I.M - Link para confirmação de e-mail"]);

        var result = (dataConfirmEmail != null) && !string.IsNullOrWhiteSpace(userToken.Email);

        if (!result)
            throw new EmailUserAccountException(@$"{GlobalErrorsMessagesException.IsObjNull} / FirstEmailConfirmationAsync");

        await _emailService.SendTokensEmailAsync(dataConfirmEmail, dataConfirmEmail.FirstConfirmEmailWelcomeMessage());

        return ApiResponse<UserToken>.Response([$@"Message successfully sent to -> {userToken.Email}"], true, "FirstEmailConfirmationAsync", new UserToken());
    }
    private async Task NotifyAccountLockedAsync(UserAccount userAccount)
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
    public async Task<bool> IsEmailConfirmedAsync(string email)
    {
        string emailValidated = IsValidEmail(email);

        var userAccount = await _userManager.FindByEmailAsync(emailValidated);

        return await _userManager.IsEmailConfirmedAsync(userAccount);
    }
    public async Task ValidateUserAccountAsync(UserAccount userAccount)
    {
        if (await _userManager.IsLockedOutAsync(userAccount))
        {
            await NotifyAccountLockedAsync(userAccount);
            throw new EmailUserAccountException(EmailUserAccountMessagesException.UserIsLocked);
        }

        if (!await _userManager.IsEmailConfirmedAsync(userAccount))
        {
            await SendConfirmEmailAsync(true, userAccount);
            throw new EmailUserAccountException(EmailUserAccountMessagesException.EmailIsNotConfirmed);
        }
    }


}
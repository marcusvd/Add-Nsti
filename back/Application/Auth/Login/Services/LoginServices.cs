using Microsoft.AspNetCore.Identity;

using Domain.Entities.Authentication;
using Authentication.Exceptions;
using Application.Shared.Dtos;
using Application.Helpers.Inject;
using Application.Auth.TwoFactorAuthentication;
using Application.Auth.Login.Extends;
using Application.Auth.UsersAccountsServices.TimedAccessCtrlServices.Services;
using Application.Auth.UsersAccountsServices.PasswordServices.Services;
using AApplication.Auth.UsersAccountsServices.PasswordServices.Dtos;
using Application.Auth.UsersAccountsServices.PasswordServices.Exceptions;
using Application.Auth.JwtServices;
using Application.Auth.Login.Dtos;
using Application.Auth.UsersAccountsServices.EmailUsrAccountServices.Services;
using Application.Auth.UsersAccountsServices.Services.Auth;

namespace Application.Auth.Login.Services;

public partial class LoginServices : LoginBase, ILoginServices
{
    private readonly UserManager<UserAccount> _userManager;
    private readonly SignInManager<UserAccount> _signInManager;
    private readonly IValidatorsInject _validatorsInject;
    private readonly IUserAccountAuthServices _userAccountAuthServices;
    private readonly ITimedAccessControlServices _timedAccessControlServices;
    private readonly IPasswordServices _passwordServices;
    private readonly ITwoFactorAuthenticationServices _twoFactorAuthenticationServices;
    private readonly IJwtServices _jwtServices;
    private readonly IEmailUserAccountServices _emailUserAccountServices;

    public LoginServices(
                        UserManager<UserAccount> userManager,
                        SignInManager<UserAccount> signInManager,
                        //   IAuthServicesInjection authServicesInjection,
                        IValidatorsInject validatorsInject,
                        ITimedAccessControlServices timedAccessControlServices,
                        IUserAccountAuthServices userAccountAuthServices,
                        IPasswordServices passwordServices,
                        ITwoFactorAuthenticationServices twoFactorAuthenticationServices,
                        IJwtServices jwtServices,
                        IEmailUserAccountServices emailUserAccountServices

      )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _validatorsInject = validatorsInject;
        _timedAccessControlServices = timedAccessControlServices;
        _userAccountAuthServices = userAccountAuthServices;
        _passwordServices = passwordServices;
        _twoFactorAuthenticationServices = twoFactorAuthenticationServices;
        _jwtServices = jwtServices;
        _emailUserAccountServices = emailUserAccountServices;
    }

    public async Task<ApiResponse<UserToken>> LoginAsync(LoginModelDto user)
    {
        _validatorsInject.GenericValidators.IsObjNull(user);

        var userAccount = await _userAccountAuthServices.GetUserAsync(user.Email);

        _validatorsInject.GenericValidators.IsObjNull(userAccount);

        await _emailUserAccountServices.ValidateUserAccountAsync(userAccount);

        await GetTimedAccessControlAsync(userAccount.Id);

        await WillExpire(userAccount.WillExpire, userAccount.Email);

        var result = await _passwordServices.PasswordSignInAsync(userAccount, user.Password, true, true);

        var IsEnable2FA = await _twoFactorAuthenticationServices.HandleTwoFactorAuthenticationAsync(userAccount);

        if (result.Data!.Succeeded && IsEnable2FA)
            return await ResponseWith2FA(userAccount);

        if (result.Data!.Succeeded)
            return await ResponseNo2FA(userAccount);

        return ApiResponse<UserToken>.Response([$"login fail for user: {userAccount.Email}"], new UserToken().Authenticated, "LoginAsync", new UserToken());
    }

    private async Task GetTimedAccessControlAsync(int userId)
    {
        var timedAccessControl = await _timedAccessControlServices.GetTimedAccessControlAsync(userId);
        if (!await _timedAccessControlServices.CheckTimeIntervalAsync(timedAccessControl)) throw new AuthServicesException(AuthErrorsMessagesException.TimeIsOutside);
    }
    private async Task WillExpire(DateTime expire, string email)
    {
        if (expire.Year != DateTime.MinValue.Year)
        {
            await _passwordServices.ForgotPasswordAsync(new ForgotPasswordDto() { Email = email });
            throw new AuthServicesException(PasswordServicesMessagesException.PasswordWillExpire);
        }
    }
    private async Task<ApiResponse<UserToken>> ResponseWith2FA(UserAccount userAccount)
    {
        var userToken_2FA = await _jwtServices.CreateTwoFactorResponse(userAccount);

        return ApiResponse<UserToken>.Response([""], userToken_2FA.Authenticated, $"Successful login for user: {userAccount.Email}", userToken_2FA);
    }
    private async Task<ApiResponse<UserToken>> ResponseNo2FA(UserAccount userAccount)
    {
        var userToken = await _jwtServices.CreateAuthenticationResponseAsync(userAccount);
        return ApiResponse<UserToken>.Response([""], userToken.Authenticated, $"Successful login for user: {userAccount.Email}", userToken);
    }



    public async Task<ApiResponse<string>> LogoutAsync()
    {
        await _signInManager.SignOutAsync();

        return ApiResponse<string>.Response([""], true, "Logout realizado com sucesso", "");
    }

    private async Task<IdentityResult> WriteLastLogin(string email)
    {
        var userAccount = await _userAccountAuthServices.GetUserAccountByEmailAsync(email);

        _validatorsInject.GenericValidators.IsObjNull(userAccount);

        userAccount.LastLogin = DateTime.Now;

        var Update = await _userManager.UpdateAsync(userAccount);

        return Update;
    }
    public async Task<DateTime> GetLastLogin(string email)
    {
        var userAccount = await _userAccountAuthServices.GetUserAccountByEmailAsync(email);

        _validatorsInject.GenericValidators.IsObjNull(userAccount);

        return userAccount.LastLogin;
    }

}
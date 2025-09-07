using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Authentication.Helpers;
using Domain.Entities.Authentication;
using Authentication.Exceptions;
using Authentication.Jwt;
using Microsoft.Extensions.Logging;
using Application.Services.Operations.Auth.Dtos;
using Application.Services.Operations.Auth;
using Application.Services.Operations.Auth.Account.dtos;
using Authentication.AuthenticationRepository.UserAccountRepository;
using Application.Exceptions;
using Domain.Entities.System.Profiles;
using UnitOfWork.Persistence.Operations;


namespace Application.Services.Operations.Account;

public class AccountManagerServices : AuthenticationBase, IAccountManagerServices
{
    private readonly ILogger<AccountManagerServices> _logger;
    private readonly UserManager<UserAccount> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly AuthGenericValidatorServices _genericValidatorServices;
    // private readonly IUserAccountRepository _userAccountRepository;
    private readonly IUnitOfWork _GENERIC_REPO;
    private readonly IUrlHelper _url;
    public AccountManagerServices(
          UserManager<UserAccount> userManager,
          RoleManager<Role> roleManager,
          JwtHandler jwtHandler,
          IUrlHelper url,
          AuthGenericValidatorServices genericValidatorServices,
          ILogger<AccountManagerServices> logger,
          // //   IUserAccountRepository userAccountRepository,
          IUnitOfWork GENERIC_REPO
      ) : base(userManager, jwtHandler, logger, url)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
        _url = url;
        _genericValidatorServices = genericValidatorServices;
        _GENERIC_REPO = GENERIC_REPO;
        // _userAccountRepository = userAccountRepository;
    }

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

        var result = await _userManager.ChangeEmailAsync(userAccount, confirmRequestEmailChange.Email, confirmRequestEmailChange.Token);
        if (result.Succeeded)
        {
            userAccount.UserName = confirmRequestEmailChange.Email;
            userAccount.Email = confirmRequestEmailChange.Email;
            await _userManager.UpdateAsync(userAccount);
        }

        return result;
    }
    public async Task<IdentityResult> ConfirmEmailAddressAsync(ConfirmEmail confirmEmail)
    {
        var userAccout = await FindUserAsync(confirmEmail.Email);

        var result = await _userManager.ConfirmEmailAsync(userAccout, confirmEmail.Token);

        return result;

    }
    public async Task<IdentityResult> ForgotPasswordAsync(ForgotPassword forgotPassword)
    {
        var userAccount = await FindUserAsync(forgotPassword.Email);

        var genToken = GenerateUrlTokenPasswordReset(userAccount, "ForgotPassword", "auth");

        var dataConfirmEmail = DataConfirmEmailMaker(userAccount, [await genToken, "http://localhost:4200/password-reset", "api/auth/ForgotPassword", "I.M - Link para recadastramento de senha."]);

        await SendEmailConfirmationAsync(dataConfirmEmail, dataConfirmEmail.PasswordReset());

        return IdentityResult.Success;
    }
    public async Task<IdentityResult> ResetPasswordAsync(ResetPassword resetPassword)
    {
        var userAccount = await _userManager.FindByEmailAsync(resetPassword.Email) ?? throw new AuthServicesException(AuthErrorsMessagesException.ObjectIsNull);

        IdentityResult identityResult = await _userManager.ResetPasswordAsync(userAccount, resetPassword.Token, resetPassword.Password);

        if (!identityResult.Succeeded) throw new AuthServicesException($"{AuthErrorsMessagesException.ResetPassword} - {identityResult}");

        return identityResult;
    }
    public async Task<IdentityResult> UpdateUserAccountAuthAsync(UserAccountAuthUpdateDto userAccount, int id)
    {

        _genericValidatorServices.Validate(userAccount.Id, id, GlobalErrorsMessagesException.EntityFromIdIsNull);

        var userAccountFromDb = await _userManager.FindByEmailAsync(userAccount.Email) ?? (UserAccount)_genericValidatorServices.ReplaceNullObj<UserAccount>();

        var toUpdate = userAccount.ToUpdate(userAccountFromDb);

        return await _userManager.UpdateAsync(toUpdate);

    }
    public async Task<IdentityResult> UpdateUserAccountProfileAsync(UserAccountProfileUpdateDto userAccount, int id)
    {

        _genericValidatorServices.Validate(userAccount.Id, id, GlobalErrorsMessagesException.EntityFromIdIsNull);

        var userAccountFromDb = await GetUserProfileAsync(id);

        var toUpdate = userAccount.ToUpdate(userAccountFromDb);

        _GENERIC_REPO.UsersProfiles.Update(toUpdate);

        if (await _GENERIC_REPO.Save())
            return IdentityResult.Success;
        else
            return IdentityResult.Failed(new IdentityError() { Description = "Faild update profile user" });

    }

    private async Task<UserProfile> GetUserProfileAsync(int id)
    {
        return await _GENERIC_REPO.UsersProfiles.GetByPredicate(
                   x => x.Id == id,
                   null,
                   selector => selector,
                   null
                   ) ?? (UserProfile)_genericValidatorServices.ReplaceNullObj<UserProfile>();
    }


    public async Task<string> UpdateUserRoles(UpdateUserRole role)
    {
        var myUser = await FindUserAsync(role.UserName);

        if (role.Delete)
        {
            await _userManager.RemoveFromRoleAsync(myUser, role.Role);

            return "Role removed";
        }
        else
        {
            await _userManager.AddToRoleAsync(myUser, role.Role);
            return "Role Added";
        }
    }
    public async Task<IList<string>> GetRolesAsync(UserAccount userAccount) => await _userManager.GetRolesAsync(userAccount);
    public async Task<IdentityResult> CreateRoleAsync(RoleDto roleDto) => await _roleManager.CreateAsync(new Role { Name = roleDto.Name, DisplayRole = roleDto.DisplayRole });
}


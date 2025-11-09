using Application.Auth.UsersAccountsServices.Exceptions;
using Application.Auth.UsersAccountsServices.Extends;
using Application.Exceptions;
using Application.Helpers.Inject;
using Application.Shared.Dtos;
using Application.UsersAccountsServices.Dtos;
using Domain.Entities.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UnitOfWork.Persistence.Operations;

namespace Application.Auth.UsersAccountsServices.Services.Auth;

public class UserAccountAuthServices : UserAccountServicesBase, IUserAccountAuthServices
{
    private readonly IUnitOfWork _genericRepo;
    private UserManager<UserAccount> _userManager { get; }
    private IValidatorsInject _validatorsInject { get; }



    public UserAccountAuthServices(
                           IUnitOfWork genericRepo,
                           UserManager<UserAccount> usersManager,
                           IValidatorsInject validatorsInject

                           )
    {
        _genericRepo = genericRepo;
        _userManager = usersManager;
        _validatorsInject = validatorsInject;
    }

    public async Task<List<CompanyUserAccount>> GetCompanyUserAccountByCompanyId(int companyAuthId)
    {
        return await _genericRepo.CompaniesUserAccounts.Get(
            x => x.CompanyAuthId == companyAuthId && x.Deleted == DateTime.MinValue,
              add => add.Include(x => x.UserAccount),
            selector => selector
            ).ToListAsync() ?? new List<CompanyUserAccount>();
    }
    public async Task<ApiResponse<bool>> IsUserExistCheckByEmailAsync(string emailParam)
    {
        // string email = IsValidEmail(emailParam);

        var userAccount = await _userManager.FindByEmailAsync(emailParam) ?? new UserAccount() { DisplayUserName = "invalid", UserProfileId = "invalid" };

        return ApiResponse<bool>.Response([@$"Usuário não encontrado. {emailParam}"], userAccount != null, "IsUserExistCheckByEmailAsync", userAccount != null);
    }
    public async Task<UserAccount> GetUserIncluded(int userId)
    {
        return await _genericRepo.UsersAccounts.GetByPredicate(x =>
                       x.Id == userId && x.Deleted.Year == DateTime.MinValue.Year,
                       add => add.Include(x => x.TimedAccessControl),
                       selector => selector,
                       null);
    }
    public async Task<UserAccount> GetUserAsync(string userNameOrEmail)
    {
        return await _userManager.FindByEmailAsync(userNameOrEmail) ?? await _userManager.FindByNameAsync(userNameOrEmail) ?? throw new UserAccountException(GlobalErrorsMessagesException.IsObjNull);
    }
    public async Task<UserAccount> GetUserAccountByEmailAsync(string email) => await _userManager.FindByEmailAsync(email) ?? throw new UserAccountException(GlobalErrorsMessagesException.IsObjNull);
    public async Task<UserAccount> GetUserAccountByUserIdAsync(int id)
    {
        var fromDb = await _genericRepo.UsersAccounts.GetByPredicate(
              x => x.Id == id && x.Deleted == DateTime.MinValue,
              add => add.Include(x => x.BusinessAuth)
             .Include(x => x.TimedAccessControl),
              selector => selector,
             null
          );

        return fromDb ?? throw new UserAccountException(GlobalErrorsMessagesException.IsObjNull);
    }
    // public async Task<UserAccount> GetUserAccountByUserIdAsync(int id) => await _userManager.FindByIdAsync(id.ToString()) ?? throw new UserAccountException(GlobalErrorsMessagesException.IsObjNull);

    public async Task<bool> IsAccountLockedOut(string email)
    {
        string validated = IsValidEmail(email);

        var user = await GetUserAsync(validated);

        return await _userManager.IsLockedOutAsync(user);
    }
    public async Task<ApiResponse<bool>> UpdateUserAccountAuthAsync(UserAccountDto userAccount, int id)
    {
        int idValidated = ValidateUserId(id);

        _validatorsInject.GenericValidators.Validate(userAccount.Id, id, GlobalErrorsMessagesException.EntityFromIdIsNull);

        var userAccountFromDb = await GetUserAccountByUserIdAsync(idValidated);

        var toUpdate = userAccount.ToUpdate(userAccountFromDb);

        _genericRepo.UsersAccounts.Update(toUpdate);

        return await ResponseUpdateUserAccountAuthAsync(userAccount.Id);

    }
    private async Task<ApiResponse<bool>> ResponseUpdateUserAccountAuthAsync(int id)
    {
        if (await _genericRepo.Save())
            return ApiResponse<bool>.Response([""], true, $@"{"User Account successfully updated. UserId: "} - {id}", true);
        else
            return ApiResponse<bool>.Response([$@"{"Faild update Auth user. UserId: "} - {id}"], true, "ResponseUpdateUserAccountAuthAsync", true);
    }

    public async Task<ApiResponse<IdentityResult>> ManualAccountLockedOut(AccountLockedOutManualDto emailConfirmManual)
    {

        string emailValidated = IsValidEmail(emailConfirmManual.Email);

        var userAccount = await GetUserAccountByEmailAsync(emailValidated);

        userAccount = AssignValuesManualAccountLockedOut(userAccount, emailConfirmManual.AccountLockedOut);

        var identityResult = await _userManager.UpdateAsync(userAccount);

        return ApiResponse<IdentityResult>.Response([""], identityResult.Succeeded, "WillExpireAsync", identityResult);
    }

    private UserAccount AssignValuesManualAccountLockedOut(UserAccount userAccount, bool isAccountLockedOut)
    {
        if (isAccountLockedOut)
        {
            userAccount.LockoutEnd = DateTimeOffset.Now.AddYears(10);
            userAccount.LockoutEnabled = true;
        }
        else
        {
            userAccount.LockoutEnd = DateTimeOffset.MinValue;
            userAccount.LockoutEnabled = false;
        }

        return userAccount;
    }




}
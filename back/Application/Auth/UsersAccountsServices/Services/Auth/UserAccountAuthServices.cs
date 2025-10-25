
using Application.Auth.UsersAccountsServices.EmailUsrAccountServices.Services;
using Application.Auth.UsersAccountsServices.Exceptions;
using Application.Auth.UsersAccountsServices.Extends;
using Application.Exceptions;
using Domain.Entities.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UnitOfWork.Persistence.Operations;

namespace Application.Auth.UsersAccountsServices.Auth;

public class UserAccountAuthServices : UserAccountServicesBase, IUserAccountAuthServices
{
    private readonly IUnitOfWork _genericRepo;
    private UserManager<UserAccount> _usersManager { get; }
    private readonly IEmailUserAccountServices _emailUserAccountServices;

    public UserAccountAuthServices(
                           IUnitOfWork genericRepo,
                           UserManager<UserAccount> usersManager,
                           IEmailUserAccountServices emailUserAccountServices
                           )
    {
        _genericRepo = genericRepo;
        _usersManager = usersManager;
        _emailUserAccountServices = emailUserAccountServices;
    }

    public async Task<List<CompanyUserAccount>> GetCompanyUserAccountByCompanyId(int companyAuthId)
    {
        return await _genericRepo.CompaniesUserAccounts.Get(
            x => x.CompanyAuthId == companyAuthId && x.Deleted == DateTime.MinValue,
              add => add.Include(x => x.UserAccount),
            selector => selector
            ).ToListAsync() ?? new List<CompanyUserAccount>();
    }

    // public async Task<ApiResponse<IdentityResult>> IsUserExistCheckByEmailAsync(string emailParam)
    // {
    //     string email = IsValidEmail(emailParam);

    //     var userAccount = await GetUserAccountByEmailAsync(email);

    //     return ApiResponse<IdentityResult>.Response([@$"Usuário não encontrado. {emailParam}"], userAccount != null, "IsUserExistCheckByEmailAsync", IdentityResult.Success);
    // }

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
        return await _usersManager.FindByEmailAsync(userNameOrEmail) ?? await _usersManager.FindByNameAsync(userNameOrEmail) ?? throw new UserAccountException(GlobalErrorsMessagesException.IsObjNull);
    }

    public async Task<UserAccount> GetUserAccountByEmailAsync(string email) => await _usersManager.FindByEmailAsync(email) ?? throw new UserAccountException(GlobalErrorsMessagesException.IsObjNull);
    public async Task<UserAccount> GetUserAccountByUserIdAsync(int id) => await _usersManager.FindByIdAsync(id.ToString()) ?? throw new UserAccountException(GlobalErrorsMessagesException.IsObjNull);

    public async Task ValidateUserAccountAsync(UserAccount userAccount)
    {
        if (await _usersManager.IsLockedOutAsync(userAccount))
        {
            await _emailUserAccountServices.NotifyAccountLockedAsync(userAccount);
            throw new UserAccountException(UserAccountMessagesException.UserIsLocked);
        }

        if (!await _usersManager.IsEmailConfirmedAsync(userAccount))
        {
            await _emailUserAccountServices.ResendConfirmEmailAsync(userAccount.Email);
            throw new UserAccountException(UserAccountMessagesException.EmailIsNotConfirmed);
        }
    }

}
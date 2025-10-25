using Microsoft.AspNetCore.Identity;
using Domain.Entities.Authentication;
using Repository.Data.Operations.AuthRepository.UserAccountRepository;
using Repository.Data.Operations.AuthRepository.BusinessRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UnitOfWork.Persistence.Operations;

public interface IAuthServicesInjection
{
    #region AUTH
    IUserAccountRepository UsersAccounts { get; }
    IUrlHelper UrlHelper { get; }
    SignInManager<UserAccount> SignInManager { get; }
    UserManager<UserAccount> UsersManager { get; }
    RoleManager<Role> RolesManager { get; }
    // IGenericValidatorServices GenericValidatorServices { get; }
    ITimedAccessControlRepository TimedAccessControls { get; }
    IHttpContextAccessor HttpContextAccessor { get; }
    IUserClaimsPrincipalFactory<UserAccount> UserClaimsPrincipalFactory { get; }
    #endregion
}


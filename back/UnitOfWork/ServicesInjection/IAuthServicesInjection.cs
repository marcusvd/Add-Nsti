using System.Threading.Tasks;

using Repository.Data.PersonalData.Contracts;
using Repository.Data.Operations.Companies;
using Repository.Data.Operations.BusinessesProfiles;
using Authentication.Helpers;
using Microsoft.AspNetCore.Identity;
using Domain.Entities.Authentication;
using Repository.Data.Operations.AuthRepository.UserAccountRepository;
using Repository.Data.Operations.AuthRepository.BusinessRepository;
using Repository.Data.Operations.Customers;
using Repository.Data.Operations.AddressRepository;
using Microsoft.AspNetCore.Http;
using Authentication.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace UnitOfWork.Persistence.Operations
{
    public interface IAuthServicesInjection
    {
        #region AUTH
        IUserAccountRepository UsersAccounts { get; }
        JwtHandler JwtHandler { get; }
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
}


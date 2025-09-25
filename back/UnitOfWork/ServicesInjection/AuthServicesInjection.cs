using Authentication.Helpers;
using Microsoft.AspNetCore.Identity;
using Domain.Entities.Authentication;
using Repository.Data.Operations.AuthRepository.UserAccountRepository;
using Repository.Data.Context.Auth;
using Repository.Data.Operations.AuthRepository.BusinessRepository;
using Microsoft.AspNetCore.Http;
using Authentication.Jwt;
using Microsoft.AspNetCore.Mvc;

namespace UnitOfWork.Persistence.Operations;

public class AuthServicesInjection : IAuthServicesInjection
{
  private readonly IdImDbContext _ID_CONTEXT; //context
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly IUserClaimsPrincipalFactory<UserAccount> _userClaimsPrincipalFactory;
  private readonly SignInManager<UserAccount> _SIGN_IN_MANAGER;
  private readonly UserManager<UserAccount> _USER_MANAGER_REPO;
  private readonly RoleManager<Role> _ROLE_MANAGER_REPO;
  private readonly JwtHandler _JWT_HANDLER;
  private readonly IUrlHelper _URL;
  private UserAccountRepository _USER_ACCOUNT_REPO;
  // private GenericValidatorServices GENERIC_VALIDATOR_SERVICES;
  private TimedAccessControlRepository _TIMED_ACCESS_CONTROL_REPO;
  public AuthServicesInjection(
               IdImDbContext ID_CONTEXT,
               UserManager<UserAccount> USER_MANAGER,
               SignInManager<UserAccount> SIGN_IN_MANAGER,
               RoleManager<Role> ROLE_MANAGER,
               IHttpContextAccessor httpContextAccessor,
               IUserClaimsPrincipalFactory<UserAccount> userClaimsPrincipalFactory,
               JwtHandler JWT_HANDLER,
               IUrlHelper URL
               )
  {
    _ID_CONTEXT = ID_CONTEXT;
    _USER_MANAGER_REPO = USER_MANAGER;
    _SIGN_IN_MANAGER = SIGN_IN_MANAGER;
    _ROLE_MANAGER_REPO = ROLE_MANAGER;
    _httpContextAccessor = httpContextAccessor;
    _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
    _JWT_HANDLER = JWT_HANDLER;
    _URL = URL;

  }
  #region AUTh

  public IUserAccountRepository UsersAccounts
  {
    get
    {
      return _USER_ACCOUNT_REPO = _USER_ACCOUNT_REPO ?? new UserAccountRepository(_ID_CONTEXT);
    }
  }

  public JwtHandler JwtHandler
  {
    get
    {
      return _JWT_HANDLER;
    }
  }

  public IUrlHelper UrlHelper
  {
    get
    {
      return _URL;
    }
  }

  public SignInManager<UserAccount> SignInManager => _SIGN_IN_MANAGER;

  public UserManager<UserAccount> UsersManager => _USER_MANAGER_REPO;

  public RoleManager<Role> RolesManager => _ROLE_MANAGER_REPO;

  // public IGenericValidatorServices GenericValidatorServices
  // {
  //   get
  //   {
  //     return GENERIC_VALIDATOR_SERVICES = GENERIC_VALIDATOR_SERVICES ?? new GenericValidatorServices();
  //   }
  // }
  public ITimedAccessControlRepository TimedAccessControls
  {
    get
    {
      return _TIMED_ACCESS_CONTROL_REPO = _TIMED_ACCESS_CONTROL_REPO ?? new TimedAccessControlRepository(_ID_CONTEXT);
    }
  }
  public IHttpContextAccessor HttpContextAccessor
  {
    get
    {
      return _httpContextAccessor;
    }
  }
  public IUserClaimsPrincipalFactory<UserAccount> UserClaimsPrincipalFactory
  {
    get
    {
      return _userClaimsPrincipalFactory;
    }
  }
  #endregion
}
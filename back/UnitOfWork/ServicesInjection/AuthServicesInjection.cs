using Microsoft.AspNetCore.Identity;
using Domain.Entities.Authentication;
using Repository.Data.Operations.AuthRepository.UserAccountRepository;
using Repository.Data.Context.Auth;
using Repository.Data.Operations.AuthRepository.BusinessRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UnitOfWork.Persistence.Operations;

public class AuthServicesInjection : IAuthServicesInjection
{
  private readonly IdImDbContext _IdContext; //context
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly IUserClaimsPrincipalFactory<UserAccount> _userClaimsPrincipalFactory;
  private readonly SignInManager<UserAccount> _signInManager;
  private readonly UserManager<UserAccount> _userManagerRepo;
  private readonly RoleManager<Role> _roleManagerRepo;
  private readonly IUrlHelper _url;
  private UserAccountRepository _userAccountRepo;
  private TimedAccessControlRepository _timedAccessControlRepo;


  public AuthServicesInjection() { }


  public AuthServicesInjection(
               IdImDbContext IdContext,
               UserManager<UserAccount> userManagerRepo,
               SignInManager<UserAccount> signInManager,
               RoleManager<Role> roleManagerRepo,
               IHttpContextAccessor httpContextAccessor,
               IUserClaimsPrincipalFactory<UserAccount> userClaimsPrincipalFactory,
               IUrlHelper url
               )
  {
    _IdContext = IdContext;
    _userManagerRepo = userManagerRepo;
    _signInManager = signInManager;
    _roleManagerRepo = roleManagerRepo;
    _httpContextAccessor = httpContextAccessor;
    _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
    _url = url;
  }


  #region AUTH

  public IUserAccountRepository UsersAccounts
  {
    get
    {
      return _userAccountRepo ??= new UserAccountRepository(_IdContext);
    }
  }

  public IUrlHelper UrlHelper
  {
    get
    {
      return _url;
    }
  }

  public SignInManager<UserAccount> SignInManager => _signInManager;

  public UserManager<UserAccount> UsersManager => _userManagerRepo;

  public RoleManager<Role> RolesManager => _roleManagerRepo;

  public ITimedAccessControlRepository TimedAccessControls
  {
    get
    {
      return _timedAccessControlRepo ??= new TimedAccessControlRepository(_IdContext);
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
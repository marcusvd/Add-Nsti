using System.Threading.Tasks;
using Repository.Data.Context;


using Repository.Data.PersonalData.Contracts;
using Repository.Data.Operations.Companies;
using Repository.Data.Operations.BusinessesProfiles;
using Authentication.Helpers;
using Repository.Data.Operations.AuthRepository.UserAccountRepository;
using Repository.Data.Context.Auth;
using Repository.Data.Operations.AuthRepository.BusinessRepository;
using Repository.Data.Operations.Customers;
using Repository.Data.Operations.AddressRepository;
using Repository.Data.Operations.ContactsRepository;

namespace UnitOfWork.Persistence.Operations
{
  public class Worker : IUnitOfWork
  {
    private readonly ImSystemDbContext _CONTEXT;
    private readonly IdImDbContext _ID_CONTEXT;
    // private readonly ILogger<GenericValidatorServices> _LOGGER;
    // private readonly ILogger _LOGGER;
    // private readonly IHttpContextAccessor _httpContextAccessor;
    // private readonly IUserClaimsPrincipalFactory<UserAccount> _userClaimsPrincipalFactory;
    // private readonly SignInManager<UserAccount> _SIGN_IN_MANAGER;
    // private readonly UserManager<UserAccount> _USER_MANAGER_REPO;
    // private readonly RoleManager<Role> _ROLE_MANAGER_REPO;
    // private readonly JwtHandler _JWT_HANDLER;
    // private readonly IUrlHelper _URL;

    public Worker(
                 ImSystemDbContext CONTEXT,
                 IdImDbContext ID_CONTEXT
                   //  ILogger<GenericValidatorServices> LOGGER,
                  //  ILogger LOGGER,
                //  UserManager<UserAccount> USER_MANAGER,
                //  SignInManager<UserAccount> SIGN_IN_MANAGER,
                //  RoleManager<Role> ROLE_MANAGER,
                //  IHttpContextAccessor httpContextAccessor,
                //  IUserClaimsPrincipalFactory<UserAccount> userClaimsPrincipalFactory,
                //  JwtHandler JWT_HANDLER,
                //  IUrlHelper URL
                 )
    {
      _CONTEXT = CONTEXT;
      _ID_CONTEXT = ID_CONTEXT;
      // _LOGGER = LOGGER;
      // _LOGGER = LOGGER;
      // _USER_MANAGER_REPO = USER_MANAGER;
      // _ROLE_MANAGER_REPO = ROLE_MANAGER;
      // _SIGN_IN_MANAGER = SIGN_IN_MANAGER;
      // _httpContextAccessor = httpContextAccessor;
      // _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
      // _JWT_HANDLER = JWT_HANDLER;
      // _URL = URL;

    }

    #region BUSINESSES
    private BusinessesProfilesRepository _BUSINESS_PROFILE_REPO;
    public IBusinessesProfilesRepository BusinessesProfiles
    {
      get
      {
        return _BUSINESS_PROFILE_REPO = _BUSINESS_PROFILE_REPO ?? new BusinessesProfilesRepository(_CONTEXT);
      }
    }
    private BusinessAuthRepository _BUSINESS_AUTH;
    public IBusinessAuthRepository BusinessesAuth
    {
      get
      {
        return _BUSINESS_AUTH = _BUSINESS_AUTH ?? new BusinessAuthRepository(_ID_CONTEXT);
      }
    }
    #endregion
    #region USER
    private UserProfileRepository _USER_PROFILE_REPO;
    public IUserProfileRepository UsersProfiles
    {
      get
      {
        return _USER_PROFILE_REPO = _USER_PROFILE_REPO ?? new UserProfileRepository(_CONTEXT);
      }
    }
    private UserAccountRepository _USER_ACCOUNT_REPO;
    public IUserAccountRepository UsersAccounts
    {
      get
      {
        return _USER_ACCOUNT_REPO = _USER_ACCOUNT_REPO ?? new UserAccountRepository(_ID_CONTEXT);
      }
    }
    
    // public JwtHandler JwtHandler
    // {
    //   get
    //   {
    //     return _JWT_HANDLER;
    //   }
    // }
    // public IUrlHelper UrlHelper
    // {
    //   get
    //   {
    //     return _URL;
    //   }
    // }
    // public ILogger Logger
    // {
    //   get
    //   {
    //     return _LOGGER;
    //   }
    // }


    // public SignInManager<UserAccount> SignInManager => _SIGN_IN_MANAGER;

    // public UserManager<UserAccount> UsersManager => _USER_MANAGER_REPO;

    // public RoleManager<Role> RolesManager => _ROLE_MANAGER_REPO;




    #endregion
    #region CUSTOMER
    private CustomerRepository _CUSTOMER_REPO;
    public ICustomerRepository Customers
    {
      get
      {
        return _CUSTOMER_REPO = _CUSTOMER_REPO ?? new CustomerRepository(_CONTEXT);
      }
    }
    #endregion
    #region COMPANIES
    private CompanyProfileRepository _COMPANIES_REPO;
    public ICompanyProfileRepository CompaniesProfile
    {
      get
      {
        return _COMPANIES_REPO = _COMPANIES_REPO ?? new CompanyProfileRepository(_CONTEXT);
      }
    }
    private CompanyAuthRepository _COMPANIES_AUTH;
    public ICompanyAuthRepository CompaniesAuth
    {
      get
      {
        return _COMPANIES_AUTH = _COMPANIES_AUTH ?? new CompanyAuthRepository(_ID_CONTEXT);
      }
    }
    #endregion
    #region COMPANY_USERACCOUNT

    private CompanyAuthUserAccountRepository _COMPANIES_USERACCOUNTS_REPO;
    public ICompanyAuthUserAccountRepository CompaniesUserAccounts
    {
      get
      {
        return _COMPANIES_USERACCOUNTS_REPO = _COMPANIES_USERACCOUNTS_REPO ?? new CompanyAuthUserAccountRepository(_ID_CONTEXT);
      }
    }
    #endregion
    #region ADDRESSES
    private AddressRepository _ADDRESSES_REPO;
    public IAddressRepository Addresses
    {
      get
      {
        return _ADDRESSES_REPO = _ADDRESSES_REPO ?? new AddressRepository(_CONTEXT);
      }
    }
    #endregion
    #region CONTACTS
    private ContactsRepository _CONTACTS_REPO;
    public IContactsRepository Contacts
    {
      get
      {
        return _CONTACTS_REPO = _CONTACTS_REPO ?? new ContactsRepository(_CONTEXT);
      }
    }
    #endregion

    #region 
    private GenericValidatorServices GENERIC_VALIDATOR_SERVICES;
    public IGenericValidatorServices _GenericValidatorServices
    {
      get
      {
        return GENERIC_VALIDATOR_SERVICES = GENERIC_VALIDATOR_SERVICES ?? new GenericValidatorServices();
      }
    }

    private TimedAccessControlRepository _TIMED_ACCESS_CONTROL_REPO;
    public ITimedAccessControlRepository TimedAccessControls
    {
      get
      {
        return _TIMED_ACCESS_CONTROL_REPO = _TIMED_ACCESS_CONTROL_REPO ?? new TimedAccessControlRepository(_ID_CONTEXT);
      }
    }

    // public IHttpContextAccessor HttpContextAccessor
    // {
    //   get
    //   {
    //     return _httpContextAccessor;
    //   }
    // }

    // public IUserClaimsPrincipalFactory<UserAccount> UserClaimsPrincipalFactory
    // {
    //   get
    //   {
    //     return _userClaimsPrincipalFactory;
    //   }
    // }
    #endregion
    public async Task<bool> Save()
    {
      return await _CONTEXT.SaveChangesAsync() > 0;
    }
    public async Task<bool> SaveID()
    {
      return await _ID_CONTEXT.SaveChangesAsync() > 0;
    }
  }
}

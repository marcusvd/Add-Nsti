using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using UnitOfWork.Persistence.Operations;
using Domain.Entities.Authentication;

using Application.Helpers.ServicesLauncher;
using Application.Auth.TwoFactorAuthentication;
using Application.Auth.UsersAccountsServices;
using Application.Shared.Operations;
using Application.Auth.Register.Services;
using Application.Auth.JwtServices;
using Application.Auth.Roles.Services;
using Application.Auth.IdentityTokensServices;
using Application.CompaniesServices.Services.Gateway;
using Application.CompaniesServices.Services.Auth;
using Application.CompaniesServices.Services.Profile;
using Application.Auth.UsersAccountsServices.Services.Gateway;
using Application.Auth.Login.Extends;
using Application.Auth.UsersAccountsServices.PasswordServices.Services;
using Application.EmailServices.Services;
using Application.BusinessesServices.Services.Profile;
using Application.BusinessesServices.Services.Auth;
using Application.Auth.UsersAccountsServices.Auth;
using Application.Auth.UsersAccountsServices.TimedAccessCtrlServices.Services;
using Application.Auth.UsersAccountsServices.EmailUsrAccountServices.Services;
using Application.Auth.Login.Services;
using Application.Auth.UsersAccountsServices.Profile;

namespace Application.Helpers.Inject;

public class ServiceLaucherService : IServiceLaucherService
{
    private readonly IUnitOfWork _genericRepo;
    private readonly IAuthServicesInjection _authServicesInjection;
    private readonly IValidatorsInject _validatorsInject;
    private readonly IConfiguration _configuration;
    private readonly ISmtpServices _emailServices;
    private readonly IUrlHelper _urlHelper;
    private readonly UserManager<UserAccount> _userManager;
    private readonly SignInManager<UserAccount> _signInManager;
    private RoleManager<Role> _rolesManager;
    private readonly ITimedAccessControlServices _timedAccessControlServices;
    private readonly IEmailUserAccountServices _emailUserAccountServices;

    public ServiceLaucherService(
        IUnitOfWork genericRepo,
        IAuthServicesInjection authServicesInjection,
        IValidatorsInject validatorsInject,
        IConfiguration configuration,
        ISmtpServices emailServices,
        IUrlHelper urlHelper,
        ITimedAccessControlServices timedAccessControlServices,
        IEmailUserAccountServices emailUserAccountServices,
        UserManager<UserAccount> userManager,
        SignInManager<UserAccount> signInManager,
        RoleManager<Role> rolesManager
    )
    {
        _genericRepo = genericRepo;
        _authServicesInjection = authServicesInjection;
        _validatorsInject = validatorsInject;
        _configuration = configuration;
        _emailServices = emailServices;
        _urlHelper = urlHelper;
        _timedAccessControlServices = timedAccessControlServices;
        _userManager = userManager;
        _signInManager = signInManager;
        _rolesManager = rolesManager;
        _emailUserAccountServices = emailUserAccountServices;
    }

    private JwtServices? _jwtServices;
    public IJwtServices JwtServices
    {
        get
        {
            return _jwtServices ??= new JwtServices(_configuration, _rolesServices);
        }
    }
    private TwoFactorAuthenticationServices? _twoFactorAuthenticationServices;
    public ITwoFactorAuthenticationServices TwoFactorAuthenticationServices
    {
        get
        {
            return _twoFactorAuthenticationServices ??= new TwoFactorAuthenticationServices(
                _userAccountAuthServices, _identityTokensServices,
                _userManager, _signInManager, _validatorsInject, _emailServices, _jwtServices,_authServicesInjection.HttpContextAccessor
                );
        }
    }
    BusinessProfileServices? _businessProfileServices;
    public IBusinessProfileServices BusinessProfileServices
    {
        get
        {
            return _businessProfileServices ??= new BusinessProfileServices(_genericRepo);
        }
    }
    private BusinessAuthServices? _businessesAuthServices;
    public IBusinessAuthServices BusinessesAuthServices
    {
        get
        {
            return _businessesAuthServices ??= new BusinessAuthServices(_genericRepo, _validatorsInject);
        }
    }
    private CompanyServices? _companyServices;
    public ICompanyServices CompanyServices
    {
        get
        {
            return _companyServices ??= new CompanyServices(_genericRepo, CompanyAuthServices, CompanyProfileServices, _validatorsInject, _businessesAuthServices);
        }
    }

    private CompanyAuthServices? _companyAuthServices;
    public ICompanyAuthServices CompanyAuthServices
    {
        get
        {
            return _companyAuthServices ??= new CompanyAuthServices(_genericRepo, _validatorsInject);
        }
    }
    private CompanyProfileServices? _companyProfileServices;
    public ICompanyProfileServices CompanyProfileServices
    {
        get
        {
            return _companyProfileServices ??= new CompanyProfileServices(_genericRepo, _authServicesInjection, _validatorsInject);
        }
    }

    private PasswordServices? _passwordServices;
    public IPasswordServices PasswordServices
    {
        get
        {
            return _passwordServices ??= new PasswordServices(_identityTokensServices, _emailServices, _userAccountAuthServices, _userManager, _signInManager);
        }
    }


    private UserAccountServices? _userAccountServices;
    public IUserAccountServices UserAccountServices
    {
        get
        {
            return _userAccountServices ??= new UserAccountServices(_userAccountAuthServices, _userAccountProfileServices);
        }
    }
    private UserAccountProfileServices? _userAccountProfileServices;
    public IUserAccountProfileServices? UserAccountProfileServices
    {
        get
        {
            return _userAccountProfileServices ??= new UserAccountProfileServices(_genericRepo, _validatorsInject);
        }
    }

    private UserAccountAuthServices? _userAccountAuthServices;
    public IUserAccountAuthServices UserAccountAuthServices
    {
        get
        {
            return _userAccountAuthServices ??= new UserAccountAuthServices(_genericRepo, _userManager, _emailUserAccountServices);
        }
    }


    private IdentityTokensServices? _identityTokensServices;
    public IIdentityTokensServices IdentityTokensServices
    {
        get
        {
            return _identityTokensServices ??= new IdentityTokensServices(_userManager, _urlHelper);
        }
    }
    private RolesServices? _rolesServices;
    public IRolesServices RolesServices
    {
        get
        {
            return _rolesServices ??= new RolesServices(_userManager, _rolesManager, _userAccountAuthServices);
        }
    }
    private RegisterUserAccountServices? _registerUserAccountServices;
    public IRegisterUserAccountServices RegisterUserAccountServices
    {
        get
        {
            return _registerUserAccountServices ??= new RegisterUserAccountServices(
                _genericRepo, _userManager, _validatorsInject, _identityTokensServices,
                _companyAuthServices, _businessProfileServices, _businessesAuthServices,
                _emailServices, _rolesServices, _jwtServices
                );
        }
    }
    private LoginServices? _loginServices;
    public ILoginServices LoginServices
    {
        get
        {
            return _loginServices ??= new LoginServices(
                                                    _userManager,
                                                    _signInManager,
                                                    _validatorsInject,
                                                    _timedAccessControlServices,
                                                    _userAccountAuthServices,
                                                    _passwordServices,
                                                    _twoFactorAuthenticationServices,
                                                    _jwtServices
                                                    );
        }
    }
    private FirstRegisterBusinessServices? _first_Register_Business_Services;
    public IFirstRegisterBusinessServices RegisterServices
    {
        get
        {
            return _first_Register_Business_Services ??= new FirstRegisterBusinessServices(
                                                                                            _genericRepo,
                                                                                            _validatorsInject,
                                                                                            _identityTokensServices,
                                                                                            _emailServices,
                                                                                            _userManager,
                                                                                            _rolesServices,
                                                                                            _jwtServices
                                                                                            );
        }
    }
    private AddressServices? _addressServices;
    public IAddressServices AddressServices
    {
        get
        {
            return _addressServices ??= new AddressServices(_genericRepo, _validatorsInject);
        }
    }
    private ContactServices? _contactServices;
    public IContactServices ContactServices
    {
        get
        {
            return _contactServices ??= new ContactServices(_genericRepo, _validatorsInject);
        }
    }

}

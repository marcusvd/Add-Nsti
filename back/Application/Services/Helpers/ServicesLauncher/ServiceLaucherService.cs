using Application.Services.Operations.Account;
using Application.Services.Operations.Auth.CompanyAuthServices;
using Application.Services.Operations.Auth.Login;
using Application.Services.Operations.Auth.Register;
using Application.Services.Shared.Operations;
using Authentication.Operations.AuthAdm;
using Authentication.Operations.TwoFactorAuthentication;
using UnitOfWork.Persistence.Operations;

namespace Application.Services.Helpers.ServicesLauncher;

public class ServiceLaucherService : IServiceLaucherService
{
    private readonly IUnitOfWork _GENERIC_REPO;
    private readonly IAuthServicesInjection _AUTH_SERVICES_INJECTION;
    public ServiceLaucherService(
        IUnitOfWork GENERIC_REPO,
        IAuthServicesInjection AUTH_SERVICES_INJECTION
    )
    {
        _GENERIC_REPO = GENERIC_REPO;
        _AUTH_SERVICES_INJECTION = AUTH_SERVICES_INJECTION;
    }

    private TwoFactorAuthenticationServices TWO_FACTOR_AUTHENTICATION_SERVICES;
    public ITwoFactorAuthenticationServices TwoFactorAuthenticationServices
    {
        get
        {
            return TWO_FACTOR_AUTHENTICATION_SERVICES = TWO_FACTOR_AUTHENTICATION_SERVICES ?? new TwoFactorAuthenticationServices(_GENERIC_REPO, _AUTH_SERVICES_INJECTION);
        }
    }
    private AccountManagerServices _ACCOUNT_MANAGER_SERVICES;
    public IAccountManagerServices AccountManagerServices
    {
        get
        {
            return _ACCOUNT_MANAGER_SERVICES = _ACCOUNT_MANAGER_SERVICES ?? new AccountManagerServices(_GENERIC_REPO, _AUTH_SERVICES_INJECTION);
        }
    }
    private AuthAdmServices _AUTH_ADM_SERVICES;
    public IAuthAdmServices AuthAdmServices
    {
        get
        {
            return _AUTH_ADM_SERVICES = _AUTH_ADM_SERVICES ?? new AuthAdmServices(_GENERIC_REPO);
        }
    }
    private CompanyAuthServices _COMPANY_AUTH_SERVICES;
    public ICompanyAuthServices CompanyAuthServices
    {
        get
        {
            return _COMPANY_AUTH_SERVICES = _COMPANY_AUTH_SERVICES ?? new CompanyAuthServices(_GENERIC_REPO, _AUTH_SERVICES_INJECTION);
        }
    }
    private RegisterUserAccountServices _REGISTER_USER_ACCOUNT_SERVICES;
    public IRegisterUserAccountServices RegisterUserAccountServices
    {
        get
        {
            return _REGISTER_USER_ACCOUNT_SERVICES = _REGISTER_USER_ACCOUNT_SERVICES ?? new RegisterUserAccountServices(_GENERIC_REPO, _AUTH_SERVICES_INJECTION);
        }
    }
    private LoginServices LOGIN_SERVICES;
    public ILoginServices LoginServices
    {
        get
        {
            return LOGIN_SERVICES = LOGIN_SERVICES ?? new LoginServices(_GENERIC_REPO, _AUTH_SERVICES_INJECTION);
        }
    }
    private FirstRegisterBusinessServices FIRST_REGISTER_BUSINESS_SERVICES;
    public IFirstRegisterBusinessServices RegisterServices
    {
        get
        {
            return FIRST_REGISTER_BUSINESS_SERVICES = FIRST_REGISTER_BUSINESS_SERVICES ?? new FirstRegisterBusinessServices(_GENERIC_REPO, _AUTH_SERVICES_INJECTION);
        }
    }
    private AddressServices ADDRESS_SERVICES;
    public IAddressServices AddressServices
    {
        get
        {
            return ADDRESS_SERVICES = ADDRESS_SERVICES ?? new AddressServices(_GENERIC_REPO);
        }
    }
    private ContactServices CONTACT_SERVICES;
    public IContactServices ContactServices
    {
        get
        {
            return CONTACT_SERVICES = CONTACT_SERVICES ?? new ContactServices(_GENERIC_REPO);
        }
    }


    // private readonly ITwoFactorAuthenticationServices _TWO_FACTOR_AUTHENTICATION_SERVICES;
    // ITwoFactorAuthenticationServices TwoFactorAuthenticationServices
    // {
    //     get
    //     {
    //         return _TWO_FACTOR_AUTHENTICATION_SERVICES = _TWO_FACTOR_AUTHENTICATION_SERVICES ?? new TwoFactorAuthenticationServices(_GENERIC_REPO, _AUTH_SERVICES_INJECTION);
    //     }
    // }
}
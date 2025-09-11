using Application.Services.Operations.Account;
using Application.Services.Operations.Auth.CompanyAuthServices;
using Application.Services.Operations.Auth.Login;
using Application.Services.Operations.Auth.Register;
using Application.Services.Shared.Operations;
using Authentication.Jwt;
using Authentication.Operations.AuthAdm;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UnitOfWork.Persistence.Operations;

namespace Application.Services.Helpers.ServicesLauncher;

public class ServiceLaucherService : IServiceLaucherService
{
    private readonly ILogger<AccountManagerServices> _LOGGER_AccountManagerServices;
    private readonly ILogger<RegisterUserAccountServices> _LOGGER_RegisterUserAccountServices;
    private readonly ILogger<LoginServices> _LOGGER_LoginServices;
    private readonly ILogger<FirstRegisterBusinessServices> _LOGGER_FirstRegisterBusinessServices;

    private readonly IUnitOfWork _GENERIC_REPO;
    private readonly IUrlHelper _URL;
    private readonly JwtHandler _JWTHANDLER;
    public ServiceLaucherService(
        ILogger<AccountManagerServices> LOGGER_AccountManagerServices,
        ILogger<RegisterUserAccountServices> LOGGER_RegisterUserAccountServices,
        ILogger<LoginServices> LOGGER_LoginServices,
        ILogger<FirstRegisterBusinessServices> LOGGER_FirstRegisterBusinessServices,
        IUnitOfWork GENERIC_REPO,
        IUrlHelper URL,
        JwtHandler JWTHANDLER
    )
    {
        _LOGGER_AccountManagerServices = LOGGER_AccountManagerServices;
        _LOGGER_RegisterUserAccountServices = LOGGER_RegisterUserAccountServices;
        _LOGGER_LoginServices = LOGGER_LoginServices;
        _LOGGER_FirstRegisterBusinessServices = LOGGER_FirstRegisterBusinessServices;
        _GENERIC_REPO = GENERIC_REPO;
        _URL = URL;
        _JWTHANDLER = JWTHANDLER;
    }

    private AccountManagerServices _ACCOUNT_MANAGER_SERVICES;
    public IAccountManagerServices AccountManagerServices
    {
        get
        {
            return _ACCOUNT_MANAGER_SERVICES = _ACCOUNT_MANAGER_SERVICES ?? new AccountManagerServices(_JWTHANDLER, _URL, _LOGGER_AccountManagerServices, _GENERIC_REPO);
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
            return _COMPANY_AUTH_SERVICES = _COMPANY_AUTH_SERVICES ?? new CompanyAuthServices(_GENERIC_REPO);
        }
    }
    private RegisterUserAccountServices _REGISTER_USER_ACCOUNT_SERVICES;
    public IRegisterUserAccountServices RegisterUserAccountServices
    {
        get
        {
            return _REGISTER_USER_ACCOUNT_SERVICES = _REGISTER_USER_ACCOUNT_SERVICES ?? new RegisterUserAccountServices(_JWTHANDLER, _URL, _GENERIC_REPO, _LOGGER_RegisterUserAccountServices);
        }
    }
    private LoginServices LOGIN_SERVICES;
    public ILoginServices LoginServices
    {
        get
        {
            return LOGIN_SERVICES = LOGIN_SERVICES ?? new LoginServices(_LOGGER_LoginServices, _GENERIC_REPO, _JWTHANDLER, _URL);
        }
    }
    private FirstRegisterBusinessServices FIRST_REGISTER_BUSINESS_SERVICES;
    public IFirstRegisterBusinessServices RegisterServices
    {
        get
        {
            return FIRST_REGISTER_BUSINESS_SERVICES = FIRST_REGISTER_BUSINESS_SERVICES ?? new FirstRegisterBusinessServices(_JWTHANDLER, _URL, _GENERIC_REPO, _LOGGER_FirstRegisterBusinessServices);
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
}
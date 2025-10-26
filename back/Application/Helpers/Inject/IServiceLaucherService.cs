using Application.Auth.TwoFactorAuthentication;
using Application.Auth.UsersAccountsServices;
using Application.Shared.Operations;
using Application.Auth.JwtServices;
using Application.Auth.Register.Services;
using Application.Auth.Roles.Services;
using Application.Auth.IdentityTokensServices;
using Application.BusinessesServices.Services.Auth;
using Application.CompaniesServices.Services.Gateway;
using Application.CompaniesServices.Services.Profile;
using Application.CompaniesServices.Services.Auth;
using Application.Auth.UsersAccountsServices.Services.Gateway;
using Application.Auth.Login.Extends;
using Application.BusinessesServices.Services.Profile;
using Application.Auth.UsersAccountsServices.PasswordServices.Services;
using Application.Auth.UsersAccountsServices.Profile;
using Application.Auth.UsersAccountsServices.EmailUsrAccountServices.Services;
using Application.Auth.UsersAccountsServices.TimedAccessCtrlServices.Services;


namespace Application.Helpers.ServicesLauncher;

public interface IServiceLaucherService
{
    IJwtServices JwtServices { get; }
    ITwoFactorAuthenticationServices TwoFactorAuthenticationServices { get; }
    IBusinessProfileServices BusinessProfileServices { get; }
    IBusinessAuthServices BusinessesAuthServices { get; }
    ICompanyServices CompanyServices { get; }
    ICompanyAuthServices CompanyAuthServices { get; }
    ICompanyProfileServices CompanyProfileServices { get; }
    IPasswordServices PasswordServices { get; }
    IEmailUserAccountServices EmailUserAccountServices { get; }
    IUserAccountServices UserAccountServices { get; }
    IUserAccountProfileServices UserAccountProfileServices { get; }
    IUserAccountAuthServices UserAccountAuthServices { get; }
    IIdentityTokensServices IdentityTokensServices { get; }
    IRolesServices RolesServices { get; }
    IRegisterUserAccountServices RegisterUserAccountServices { get; }
    ILoginServices LoginServices { get; }
    IFirstRegisterBusinessServices RegisterServices { get; }
    ITimedAccessControlServices UserAccountTimedAccessControlServices { get; }
    IAddressServices AddressServices { get; }
    IContactServices ContactServices { get; }
}
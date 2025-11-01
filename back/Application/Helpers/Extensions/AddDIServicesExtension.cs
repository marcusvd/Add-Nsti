using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;

using UnitOfWork.Persistence.Operations;
using Application.Customers;
using Application.Shared.Seed.EntitiesSeed;
using Application.Helpers.ServicesLauncher;
using Application.Shared.Validators;
using Application.Helpers.Inject;
using Application.Auth.IdentityTokensServices;
using Application.Auth.JwtServices;
using Application.Auth.Register.Services;
using Application.Auth.Roles.Services;
using Application.BusinessesServices.Services.Auth;
using Application.Auth.Login.Extends;
using Application.Auth.Login.Services;
using Application.CompaniesServices.Services.Auth;
using Application.CompaniesServices.Services.Profile;
using Application.CompaniesServices.Services.Gateway;
using Application.Auth.UsersAccountsServices.TimedAccessCtrlServices.Services;
using Application.Auth.UsersAccountsServices.Profile;
using Application.Auth.UsersAccountsServices.Services.Gateway;
using Application.BusinessesServices.Services.Profile;
using Application.Auth.TwoFactorAuthentication;
using Application.Auth.UsersAccountsServices.EmailUsrAccountServices.Services;
using Application.Auth.UsersAccountsServices.PasswordServices.Services;
using Application.BusinessesServices.Services.Gateway;
using Application.Auth.UsersAccountsServices.Services.Auth;
using Application.Helpers.Inject.ServicesLauncher;
using Application.Helpers.Tools.CpfCnpj;
using Application.Helpers.Tools.Services;
using Application.Helpers.Tools.Cnpj;
using Application.Helpers.Tools.ZipCode;

namespace Application.Helpers.Extensions;

public static class AddDIServicesExtension
{

    public static void AddDiServices(this IServiceCollection services)
    {
        // services.AddScoped<ISmtpServices, SmtpServices>();
        services.AddScoped<ILoginServices, LoginServices>();
        services.AddScoped<ITwoFactorAuthenticationServices, TwoFactorAuthenticationServices>();

        services.AddScoped<IFirstRegisterBusinessServices, FirstRegisterBusinessServices>();
        services.AddScoped<IRegisterUserAccountServices, RegisterUserAccountServices>();

        services.AddScoped<IPasswordServices, PasswordServices>();

        services.AddScoped<IAuthServicesInjection, AuthServicesInjection>();

        services.AddScoped<IEmailUserAccountServices, EmailUserAccountServices>();

        services.AddScoped<IUserAccountServices, UserAccountServices>();
        services.AddScoped<IUserAccountAuthServices, UserAccountAuthServices>();
        services.AddScoped<IUserAccountProfileServices, UserAccountProfileServices>();

        services.AddScoped<ICompanyAuthServices, CompanyAuthServices>();
        services.AddScoped<ICompanyProfileServices, CompanyProfileServices>();
        services.AddScoped<ICompanyServices, CompanyServices>();

        services.AddScoped<IValidatorsInject, ValidatorsInject>();
        services.AddScoped<IIdentityTokensServices, IdentityTokensServices>();
        services.AddScoped<IJwtServices, JwtServices>();

        services.AddScoped<ITimedAccessControlServices, TimedAccessControlServices>();
        services.AddScoped<IRolesServices, RolesServices>();
        services.AddScoped<ICustomerAddServices, CustomerAddServices>();
        services.AddScoped<ICustomerUpdateServices, CustomerUpdateServices>();
        services.AddScoped<IAuthServicesInjection, AuthServicesInjection>();

        services.AddScoped<IBusinessAuthServices, BusinessAuthServices>();
        services.AddScoped<IBusinessProfileServices, BusinessProfileServices>();
        services.AddScoped<IBusinessServices, BusinessServices>();

        services.AddScoped<IGenericValidators, GenericValidators>();
        services.AddScoped<IServiceLaucherService, ServiceLaucherService>();

        services.AddHttpClient<ICpfCnpjGetDataServices, UsefulToolsServices>();
        services.AddHttpClient<IZipCodeGetDataServices, UsefulToolsServices>();
        services.AddHttpClient<IPhoneNumberValidateServices, UsefulToolsServices>();
        // ILoginServices -> ITimedAccessControlServices -> IUserAccountAuthServices -> IEmailUserAccountServices -> IUserAccountAuthServices

        services.AddScoped<SeedFirstDbServices>();
    }
    public static void AddDiFluentValidationAutoValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation()
        .AddFluentValidationClientsideAdapters();
    }


}

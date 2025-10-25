using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;

using UnitOfWork.Persistence.Operations;
using Repository.Data.Operations.Companies;
using Repository.Data.Operations.AuthRepository.BusinessRepository;
using Repository.Data.Operations.Customers;
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

namespace Application.Helpers.Extensions;

public static class DiApplication
{

    public static void AddDiServicesRepositories(this IServiceCollection services)
    {

        #region Validators
        services.AddScoped<IValidatorsInject, ValidatorsInject>();
        services.AddScoped<IIdentityTokensServices, IdentityTokensServices>();
        #endregion
        #region JWT
        services.AddScoped<IJwtServices, JwtServices>();
        #endregion
        #region seed
        services.AddScoped<SeedFirstDbServices>();
        #endregion

        #region Roles
        services.AddScoped<IRolesServices, RolesServices>();
        #endregion

        #region Customer
        services.AddScoped<ICustomerAddServices, CustomerAddServices>();

        services.AddScoped<ICustomerUpdateServices, CustomerUpdateServices>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        #endregion

        // #region Company
        // services.AddScoped<IProfilesCrudService, ProfilesCrudService>();
        // #endregion


        #region Company
        services.AddScoped<ICompanyProfileRepository, CompanyProfileRepository>();
        #endregion

        #region UnitOfWork
        services.AddScoped<IUnitOfWork, Worker>();
        services.AddScoped<IAuthServicesInjection, AuthServicesInjection>();
        #endregion
        #region MailServers
        // services.AddScoped<EmailServer>();
        // services.AddScoped<Email>();
        #endregion

        services.AddScoped<IBusinessAuthServices, BusinessAuthServices>();
        services.AddScoped<IRegisterUserAccountServices, RegisterUserAccountServices>();
        services.AddScoped<IBusinessAuthRepository, BusinessAuthRepository>();
        services.AddScoped<IGenericValidators, GenericValidators>();
        services.AddScoped<IServiceLaucherService, ServiceLaucherService>();

        services.AddScoped<IRolesServices, RolesServices>();


    }
    public static void AddDiFluentValidationAutoValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation()
        .AddFluentValidationClientsideAdapters();
    }


}

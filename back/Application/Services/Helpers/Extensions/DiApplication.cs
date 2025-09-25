using Microsoft.Extensions.DependencyInjection;
using Application.Services.Operations.Customers;
using Application.Services.Shared.Seed.EntitiesSeed;
using Application.Services.Operations.Companies;
using Application.Services.Shared.Mappers.BaseMappers;
using Repository.Data.Operations.Companies;
using UnitOfWork.Persistence.Operations;
using Application.Services.Shared.Email;
using FluentValidation.AspNetCore;
using Authentication.Operations.AuthAdm;
using Application.Services.Operations.Auth.Register;
using Authentication.Helpers;
using Repository.Data.Operations.AuthRepository.BusinessRepository;
using Application.Services.Helpers.ServicesLauncher;
using Repository.Data.Operations.Customers;

namespace Application.Services.Helpers.Extensions;

public static class DiApplication
{

    public static void AddDiServicesRepositories(this IServiceCollection services)
    {

        #region seed
        services.AddScoped<SeedFirstDbServices>();
        #endregion

        #region ObjectMapper
        services.AddScoped<IObjectMapper, ObjectMapper>();



        // services.AddScoped<MapperManagement>();
        // services.AddScoped<IAuthenticationObjectMapperServices, AuthenticationObjectMapperServices>();
        #endregion

        #region Customer
        // services.AddScoped<ICustomerObjectMapperServices, CustomerObjectMapperServices>();
        services.AddScoped<ICustomerAddServices, CustomerAddServices>();
        // services.AddScoped<ICustomerSearchService, CustomerSearchService>();
        // services.AddScoped<ICustomerGetServices, CustomerGetServices>();
        services.AddScoped<ICustomerUpdateServices, CustomerUpdateServices>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        #endregion

        #region Company
        services.AddScoped<IProfilesCrudService, ProfilesCrudService>();
        #endregion
        #region Company
        services.AddScoped<ICompanyProfileAddService, CompanyProfileAddService>();
        services.AddScoped<ICompanyGetService, CompanyGetService>();
        services.AddScoped<ICompanyProfileRepository, CompanyProfileRepository>();
        #endregion
        #region Addresses
        // services.AddScoped<IAddressesRepository, AddressesRepository>();
        // services.AddScoped<IAddressesServices, AddressesServices>();
        #endregion
        #region Contacts
        // services.AddScoped<IContactsRepository, ContactsRepository>();
        // services.AddScoped<IContactsServices, ContactsServices>();
        #endregion
        #region UnitOfWork
        services.AddScoped<IUnitOfWork, Worker>();
        services.AddScoped<IAuthServicesInjection, AuthServicesInjection>();
        #endregion
        #region MailServers
        services.AddScoped<EmailServer>();
        // services.AddScoped<Email>();
        #endregion

        services.AddScoped<IAuthAdmServices, AuthAdmServices>();
        services.AddScoped<IRegisterUserAccountServices, RegisterUserAccountServices>();
        services.AddScoped<IBusinessAuthRepository, BusinessAuthRepository>();
        services.AddScoped<IGenericValidatorServices, GenericValidatorServices>();
        services.AddScoped<IServiceLaucherService, ServiceLaucherService>();


    }
    public static void AddDiFluentValidationAutoValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation()
        .AddFluentValidationClientsideAdapters();
        // #region Authentication
        // services.AddScoped<IValidator<UserAccountDto>, UserAccountValidator>();
        // #endregion

        // #region Customer
        // services.AddScoped<IValidator<CustomerDto>, CustomerDtoValidator>();
        // #endregion

        // #region Shared
        // services.AddScoped<IValidator<ContactDto>, ContactValidator>();
        // services.AddScoped<IValidator<AddressDto>, AddressValidator>();
        // #endregion
        // #region Tests
        // #endregion
    }


}

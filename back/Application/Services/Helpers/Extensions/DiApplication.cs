using Microsoft.Extensions.DependencyInjection;

// using Application.Services.Operations.Customers.Dtos.Mappers;
using Application.Services.Operations.Customers;
using Application.Services.Shared.Seed.EntitiesSeed;
using Application.Services.Operations.Companies;
using Application.Services.Shared.Mappers.BaseMappers;
using Application.Services.Operations.Customers.Search;

using Repository.Data.Operations.Companies;
using Repository.Data.Operations.Main.Customers;

using UnitOfWork.Persistence.Operations;
using Application.Services.Shared.Email;
using FluentValidation.AspNetCore;
using Authentication.Operations.AuthAdm;
using Authentication.AuthenticationRepository.BusinessAuthRepository;
using Application.Services.Operations.Auth.Register;
using Domain.Entities.Shared;
using Application.Services.Shared.Dtos;
using Domain.Entities.Authentication;
using Application.Services.Operations.Auth.Dtos;


namespace Application.Services.Helpers.Extensions;

public static class DiApplication
{

    public static void AddDiServicesRepositories(this IServiceCollection services)
    {

        #region seed
        services.AddScoped<SeedFirstDbServices>();
        #endregion

        #region ObjectMapper
        services.AddScoped<ICommonObjectMapper, CommonObjectMapper>();

        

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
        #endregion
        #region MailServers
        services.AddScoped<EmailServer>();
        // services.AddScoped<Email>();
        #endregion

        services.AddScoped<IAuthAdmServices, AuthAdmServices>();
        services.AddScoped<IRegisterUserAccountServices, RegisterUserAccountServices>();
        services.AddScoped<IBusinessAuthRepository, BusinessAuthRepository>();

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

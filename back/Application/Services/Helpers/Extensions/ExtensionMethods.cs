using System.Net;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;


using Application.Services.Operations.Customers.DtoValidation;
using Application.Services.Operations.Customers.Dtos;
using Application.Services.Operations.Customers.Dtos.Mappers;
using Application.Services.Operations.Customers;
using Application.Services.Shared.Seed.EntitiesSeed;
using Application.Services.Operations.Companies;
using Application.Services.Operations.Authentication.Dtos;
using Application.Services.Operations.Authentication.DtoValidation;
using Application.Services.Shared.Dtos;
using Application.Services.Shared.Dtos.Mappers;
using Application.Services.Shared.DtoValidation;
using Application.Services.Operations.Authentication;
using Application.Services.Operations.Customers.Search;

using Repository.Data.Operations.Companies;
using Repository.Data.Operations.Main.Customers;

using UnitOfWork.Persistence.Operations;

using Domain.Entities.GlobalSystem;
using Application.Services.Operations.Authentication.Dtos.Mappers;


namespace Application.Services.Helpers.Extensions;

public static class ExtensionMethods
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    await context.Response.WriteAsync(new GlobalErrorHandling()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = contextFeature.Error.Message,
                        Trace = contextFeature.Error.StackTrace
                    }.ToString());
                }
            });
        });
    }
    public static void AddScopedDependencyInjection(this IServiceCollection services)
    {

        #region seed
        services.AddScoped<SeedSonnyDbServices>();
        #endregion
        
        #region ObjectMapper
        services.AddScoped<ICommonObjectMapper, CommonObjectMapper>();
        services.AddScoped<IAuthenticationObjectMapperServices, AuthenticationObjectMapperServices>();
        #endregion

        #region Customer
        services.AddScoped<ICustomerObjectMapperServices, CustomerObjectMapperServices>();
        services.AddScoped<ICustomerAddServices, CustomerAddServices>();
        services.AddScoped<ICustomerSearchService, CustomerSearchService>();
        services.AddScoped<ICustomerGetServices, CustomerGetServices>();
        services.AddScoped<ICustomerUpdateServices, CustomerUpdateServices>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        #endregion

        #region Company
        services.AddScoped<ICompanyAddService, CompanyAddService>();
        services.AddScoped<ICompanyGetService, CompanyGetService>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
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
        services.AddScoped<Email>();
        #endregion

    }
    public static void AddScopedValidations(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation()
        .AddFluentValidationClientsideAdapters();
        #region Authentication
        services.AddScoped<IValidator<UserAccountDto>, UserAccountValidator>();
        #endregion

        #region Customer
        services.AddScoped<IValidator<CustomerDto>, CustomerDtoValidator>();
        #endregion

        #region Shared
        services.AddScoped<IValidator<ContactDto>, ContactValidator>();
        services.AddScoped<IValidator<AddressDto>, AddressValidator>();
        #endregion
        #region Tests
        #endregion
    }

    public static void ConfigsStartupProject(this IServiceCollection services)
    {
        services.AddControllers().AddNewtonsoftJson(opt =>
        {
            opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        });
    }


}

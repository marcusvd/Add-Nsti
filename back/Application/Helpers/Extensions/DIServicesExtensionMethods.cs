
using Microsoft.Extensions.DependencyInjection;

using Repository.Data.Operations.BusinessesProfiles;
using Repository.Data.Operations.Companies;
using Repository.Data.Operations.AuthRepository.BusinessRepository;
using Repository.Data.Operations.AuthRepository.UserAccountRepository;

using Application.Auth.Register.Services;
using Application.Auth.Login.Extends;
using Application.Auth.Login.Services;
using Application.CompaniesServices.Services.Auth;
using Application.CompaniesServices.Services.Profile;
using Application.CompaniesServices.Services.Gateway;

namespace Application.Helpers.Extensions;

public static class DIServicesExtensionMethods
{
    public static void AddDiAuthentication(this IServiceCollection services)
    {
        services.AddScoped<ILoginServices, LoginServices>();
        services.AddScoped<IFirstRegisterBusinessServices, FirstRegisterBusinessServices>();
        services.AddScoped<IBusinessesProfilesRepository, BusinessesProfilesRepository>();

        services.AddScoped<ICompanyAuthServices, CompanyAuthServices>();
        services.AddScoped<ICompanyAuthRepository, CompanyAuthRepository>();

        services.AddScoped<ICompanyProfileServices, CompanyProfileServices>();
        services.AddScoped<ICompanyServices, CompanyServices>();

        services.AddScoped<IUserAccountRepository, UserAccountRepository>();
    }
}
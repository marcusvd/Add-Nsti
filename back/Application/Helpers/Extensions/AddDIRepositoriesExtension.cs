
using Microsoft.Extensions.DependencyInjection;

using Repository.Data.Operations.BusinessesProfiles;
using Repository.Data.Operations.Companies;
using Repository.Data.Operations.AuthRepository.BusinessRepository;
using Repository.Data.Operations.AuthRepository.UserAccountRepository;
using Repository.Data.Operations.Customers;
using UnitOfWork.Persistence.Operations;

namespace Application.Helpers.Extensions;

public static class AddDIRepositoriesExtension
{
    public static void AddDiRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBusinessesProfilesRepository, BusinessesProfilesRepository>();
        services.AddScoped<ICompanyAuthRepository, CompanyAuthRepository>();
        services.AddScoped<IUserAccountRepository, UserAccountRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ICompanyProfileRepository, CompanyProfileRepository>();
        services.AddScoped<IBusinessAuthRepository, BusinessAuthRepository>();
        services.AddScoped<IUnitOfWork, Worker>();
    }
}
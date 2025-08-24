
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Repository.Data.Context;
using Microsoft.EntityFrameworkCore;
using Application.Services.Operations.Auth.Login;
using Application.Services.Operations.Auth.Register;
using Application.Services.Operations.Account;
using Authentication.Operations.AuthAdm;
using Authentication.AuthenticationRepository.UserAccountRepository;
using Authentication.AuthenticationRepository.BusinessRepository;
using Application.Services.Operations.Auth.CompanyAuthServices;
using Repository.Data.Operations.BusinessesProfiles;
using Repository.Data.Operations.Companies;

namespace Application.Services.Helpers.Extensions
{
    public static class DIServicesExtensionMethods
    {

        public static void AddDiAuthentication(this IServiceCollection services)
        {
            services.AddScoped<ILoginServices, LoginServices>();
            services.AddScoped<IRegisterServices, RegisterServices>();
            services.AddScoped<IAccountManagerServices, AccountManagerServices>();
            services.AddScoped<IAuthAdmServices, AuthAdmServices>();
            services.AddScoped<IBusinessesProfilesRepository, BusinessesProfilesRepository>();
            services.AddScoped<ICompanyAuthServices, CompanyAuthServices>();
            services.AddScoped<ICompanyAuthRepository, CompanyAuthRepository>();
            services.AddScoped<IUserAccountRepository, UserAccountRepository>();
        }

    }
}
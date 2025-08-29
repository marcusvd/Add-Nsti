using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Authentication.Helpers;
using Authentication.Context;
using Domain.Entities.Authentication;
using Authentication.Jwt;



namespace Authentication.Settings;

public static class IdentityConfiguration
{
    //time before expires token for password reset.
    public static void DataProtectionTokenProviderOptions(this IServiceCollection services)
    {
        services.Configure<DataProtectionTokenProviderOptions>(
            opt => opt.TokenLifespan = TimeSpan.FromHours(1)
        );
    }
    public static void AddAuthorizeAllControllers(this IServiceCollection services)
    {
        services.AddMvc(opt =>
        {
            var policy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();
            opt.Filters.Add(new AuthorizeFilter(policy));
        });
    }

    public static void AddIdentitySettings(this IServiceCollection services)
    {

        services.AddIdentity<UserAccount, Role>(opt =>
         {
             opt.SignIn.RequireConfirmedEmail = true;
             //
             opt.User.RequireUniqueEmail = true;
             //
             opt.Password.RequireDigit = false;
             opt.Password.RequireNonAlphanumeric = false;
             opt.Password.RequireLowercase = false;
             opt.Password.RequireUppercase = false;
             opt.Password.RequiredLength = 3;
             //
             opt.Lockout.MaxFailedAccessAttempts = 3;
             opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
             opt.Lockout.AllowedForNewUsers = true;
         })
            .AddEntityFrameworkStores<IdImDbContext>()
           .AddDefaultTokenProviders()
            .AddRoles<Role>()
           .AddRoleManager<RoleManager<Role>>()

            .AddPasswordValidator<PasswordValidatorPolicies<UserAccount>>()
           .AddRoleValidator<RoleValidator<Role>>()
           .AddUserManager<UserManager<UserAccount>>()
           .AddSignInManager<SignInManager<UserAccount>>()

           .AddUserStore<UserStore<UserAccount, Role, IdImDbContext, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityUserToken<int>, IdentityRoleClaim<int>>>()
            .AddRoleStore<RoleStore<Role, IdImDbContext, int, UserRole, IdentityRoleClaim<int>>>();

        // services.AddScoped<IUserClaimsPrincipalFactory<UserAccount>, UserAccountClaimsPrincipalFactory>();
    }
    public static void AddDiIdentity(this IServiceCollection services)
    {
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        //
        services.AddScoped<JwtHandler>();
        services.AddScoped<AuthGenericValidatorServices>();
      
        //  services.AddScoped<IRegisterUserAccountServices, UserAccountRepository>();
        // services.AddScoped<IBusinessRepository, BusinessRepository>();
        //
        services.AddScoped<IUrlHelper>(x =>
      {
          var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
          var factory = x.GetRequiredService<IUrlHelperFactory>();
          return factory.GetUrlHelper(actionContext);
      });

    }

    public static void AddAuthorizationSettings(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
            options.AddPolicy("TwoFactorEnabled",
                x => x.RequireClaim("amr", "sub")));
    }


}
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Repository.Data.Context.Auth;
using Domain.Entities.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Authentication.Validators;

namespace Authentication.Settings;

public static class IdentityConfiguration
{
    //time before expires token for password reset.
    public static void DataProtectionTokenProviderOptions(this IServiceCollection services)
    {
        //Time life token 2fa, reset password and confirm email
        services.Configure<DataProtectionTokenProviderOptions>(
            opt => opt.TokenLifespan = TimeSpan.FromMinutes(10)
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
             //add TEST TWOFACTOR   
             opt.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;

             //
             opt.User.RequireUniqueEmail = true;
             //  opt.SignIn.RequireConfirmedAccount = true;
             //
             opt.Password.RequireDigit = false;
             opt.Password.RequireNonAlphanumeric = false;
             opt.Password.RequireLowercase = false;
             opt.Password.RequireUppercase = false;
             opt.Password.RequiredLength = 3;
             //
             opt.Lockout.AllowedForNewUsers = true;
             opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(3);
             opt.Lockout.MaxFailedAccessAttempts = 3;

             // Configurações específicas do 2FA
             opt.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
             opt.Tokens.ChangeEmailTokenProvider = TokenOptions.DefaultProvider;
             opt.Tokens.ChangePhoneNumberTokenProvider = TokenOptions.DefaultPhoneProvider;
             opt.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultProvider;
             opt.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;

             //allow 2FA
             opt.Tokens.ProviderMap.Add("Authenticator", new TokenProviderDescriptor(typeof(AuthenticatorTokenProvider<UserAccount>)));

         })
            .AddEntityFrameworkStores<IdImDbContext>()
           .AddDefaultTokenProviders()
           .AddTokenProvider<AuthenticatorTokenProvider<UserAccount>>(TokenOptions.DefaultAuthenticatorProvider)


            .AddRoles<Role>()
           .AddRoleManager<RoleManager<Role>>()

            .AddPasswordValidator<PasswordValidatorPolicies<UserAccount>>()
           .AddRoleValidator<RoleValidator<Role>>()
                .AddSignInManager<SignInManager<UserAccount>>()

           .AddUserStore<UserStore<UserAccount, Role, IdImDbContext, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityUserToken<int>, IdentityRoleClaim<int>>>()
            .AddRoleStore<RoleStore<Role, IdImDbContext, int, UserRole, IdentityRoleClaim<int>>>();
    }
    public static void AddDiIdentity(this IServiceCollection services)
    {
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        //
        services.AddScoped<UserClaimsPrincipalFactory<UserAccount>>();
 

        services.ConfigureApplicationCookie(options =>
   {
       options.LoginPath = "/Account/Login";
       options.AccessDeniedPath = "/Account/AccessDenied";
       options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
   });
        services.Configure<CookieAuthenticationOptions>(
              IdentityConstants.TwoFactorUserIdScheme,
              options =>
              {
                  options.Cookie.Name = IdentityConstants.TwoFactorUserIdScheme;
                  options.ExpireTimeSpan = TimeSpan.FromMinutes(15); // Tempo curto para segurança
              });

        services.AddHttpContextAccessor();

        services.AddScoped<IUrlHelper>(x =>
      {
          var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
          var factory = x.GetRequiredService<IUrlHelperFactory>();
          return factory.GetUrlHelper(actionContext);
      });

    }

    public static void AddAuthorizationSettings(this IServiceCollection services)
    {
        services.AddAuthorization(auth =>
        {
            auth.AddPolicy("TwoFactorEnable", policy =>
            {
                policy.RequireAssertion(context => context.User.HasClaim(c => c.Type == "amr" && c.Value == "sub"));
            });
        });
    }


}
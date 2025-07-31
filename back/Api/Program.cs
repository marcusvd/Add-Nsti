using Application.Services.Helpers.Extensions;
using Authentication;
using Authentication.Entities;
using Authentication.Jwt;
using Authentication.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
// using Authentication.Settings;
using Repository.Data.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddContextImSystemDb(builder.Configuration);
builder.Services.AddDiServicesRepositories();
builder.Services.AddDiFluentValidationAutoValidation();


builder.Services.AddContextIdImDb(builder.Configuration);
builder.Services.AddIdentitySettings();
builder.Services.AddDependencyInjectionIdentity();
builder.Services.DataProtectionTokenProviderOptions();
builder.Services.AddAuthorizationSettings();

// builder.Services.AddAuthorization(options =>
//     options.AddPolicy("TwoFactorEnabled",
//         x => x.RequireClaim("amr", "sub")));

builder.Services.AddJwt(builder.Configuration);

builder.Services.AddAuthorizeAllControllers();






// Configurar o Identity
// builder.Services.AddIdentity<UserAccount, Role>(options =>
// {
//     // Configurações de senha
//     options.Password.RequireDigit = true;
//     options.Password.RequiredLength = 8;
//     options.Password.RequireNonAlphanumeric = false;
//     options.Password.RequireUppercase = true;
//     options.Password.RequireLowercase = true;

//     // Configurações de usuário
//     options.User.RequireUniqueEmail = true;

//     // Configurações de lockout
//     options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
//     options.Lockout.MaxFailedAccessAttempts = 5;
// })

// .AddEntityFrameworkStores<IdImDbContext>()
// .AddDefaultTokenProviders()
// .AddUserManager<UserManager<UserAccount>>()
// .AddRoleManager<RoleManager<Role>>()
// .AddSignInManager<SignInManager<UserAccount>>()
// .AddUserStore<UserStore<UserAccount, Role, IdImDbContext, int, IdentityUserClaim<int>,
//     UserRole, IdentityUserLogin<int>, IdentityUserToken<int>, IdentityRoleClaim<int>>>()
// .AddRoleStore<RoleStore<Role, IdImDbContext, int, UserRole, IdentityRoleClaim<int>>>();

// // Registrar o factory de claims
// builder.Services.AddScoped<IUserClaimsPrincipalFactory<UserAccount>, UserAccountClaimsPrincipalFactory>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

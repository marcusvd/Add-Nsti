using Api.Configuration;
using Application.Auth.JwtServices.Extensions;
using Application.EmailServices.ExtensionMethods;
using Application.Helpers.Extensions;
using Authentication.Settings;
using Repository.Data.Context.Auth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddNewtonsoftJsonControllers();

builder.Services.AddIdentitySettings();

builder.Services.AddDiIdentity();

builder.Services.AddEmailService(builder.Configuration);
builder.Services.AddDiRepositories();
builder.Services.AddDiServices();

builder.Services.DataProtectionTokenProviderOptions();


builder.Services.AddContextImSystemDb(builder.Configuration);
builder.Services.AddDiFluentValidationAutoValidation();


builder.Services.AddContextIdImDb(builder.Configuration);
builder.Services.AddAuthorizationSettings();

// builder.Services.AddAuthorization(options =>
//     options.AddPolicy("TwoFactorEnabled",
//         x => x.RequireClaim("amr", "sub")));

builder.Services.AddJwtSettings(builder.Configuration);


// builder.Services.Configure<AuthenticatorTokenProviderOptions>(opt =>
// {
//     opt.TokenLifespan = TimeSpan.FromMinutes(15);
// });



builder.Services.AddAuthorizeAllControllers();

builder.Services.AddCorsApiSettings();



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


app.AddGlobalExceptionHandler();

app.UseStaticFilesExtension();

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();

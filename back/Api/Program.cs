using Api.Configuration;
using Application.Services.Helpers.Extensions;
using Authentication.Jwt;
using Authentication.Settings;

// using Authentication.Settings;
using Repository.Data.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddNewtonsoftJsonControllers();
builder.Services.AddIdentitySettings();
builder.Services.AddDiIdentity();
builder.Services.AddDiAuthentication();

builder.Services.AddDiServicesRepositories();

builder.Services.DataProtectionTokenProviderOptions();


builder.Services.AddContextImSystemDb(builder.Configuration);
builder.Services.AddDiFluentValidationAutoValidation();


builder.Services.AddContextIdImDb(builder.Configuration);
builder.Services.AddAuthorizationSettings();

// builder.Services.AddAuthorization(options =>
//     options.AddPolicy("TwoFactorEnabled",
//         x => x.RequireClaim("amr", "sub")));

builder.Services.AddJwt(builder.Configuration);

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

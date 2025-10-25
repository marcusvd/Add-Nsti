
using Microsoft.Extensions.DependencyInjection;
using Application.EmailServices.Validators;
using Application.EmailServices.Dtos;
using Microsoft.Extensions.Configuration;
using Application.EmailServices.Services;

namespace Application.EmailServices.ExtensionMethods;

public static class ExtensionMethods
{
    public static void AddEmailService(this IServiceCollection services, IConfiguration configuration)
    {

        services.Configure<EmailServiceConfiguration>(configuration.GetSection(EmailServiceConfiguration.SectionName));

        services.AddSingleton<IEmailServiceValidator, EmailServiceValidator>();
        services.AddScoped<ISmtpServices, SmtpServices>();
    }
    public static IServiceCollection AddEmailService(this IServiceCollection services, Action<EmailServiceConfiguration> configure)
    {
        services.Configure(configure);

        services.AddSingleton<IEmailServiceValidator, EmailServiceValidator>();
        services.AddScoped<ISmtpServices, SmtpServices>();

        return services;
    }


    public static void SetEmailServiceConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEmailService(configuration);

        // var host = builder.Build();

        // Exemplo de uso chama um serviço dentro do program;
        // var emailService = host.Services.GetRequiredService<IEmailService>();

        // var message = new EmailMessage(
        //     to: "client@example.com",
        //     subject: "Bem-vindo!",
        //     body: "<h1>Bem-vindo ao nosso serviço!</h1>")
        //     .WithFrom("noreply@nostopti.com.br");

        // await emailService.SendAsync(message);
    }

}
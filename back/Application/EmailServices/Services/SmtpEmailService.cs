// Services/SmtpServices.cs
using System.Net;
using System.Net.Mail;
using Application.Auth.UsersAccountsServices.EmailUsrAccountServices.dtos;
using Application.EmailServices.Dtos;
using Application.EmailServices.Exceptions;
using Application.EmailServices.Validators;
using Authentication.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.EmailServices.Services;

public sealed class SmtpServices : ISmtpServices, IDisposable
{
    private readonly EmailServiceConfiguration _config;
    private readonly IEmailServiceValidator _validator;
    private readonly ILogger<SmtpServices> _logger;
    private readonly SmtpClient _smtpClient;
    private bool _disposed;

    public SmtpServices(IOptions<EmailServiceConfiguration> config,
        IEmailServiceValidator validator,
        ILogger<SmtpServices> logger)
    {
        _config = config.Value;
        _validator = validator;
        _logger = logger;

        _smtpClient = CreateSmtpClient();
    }

    private SmtpClient CreateSmtpClient() => new(_config.SmtpServer)
    {
        Port = _config.Port,
        Credentials = new NetworkCredential(_config.UserName, _config.Password),
        EnableSsl = _config.UseSsl,
        Timeout = _config.TimeoutSeconds * 1000
    };

    public async Task SendAsync(EmailMessage message, CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(_disposed, nameof(SmtpServices));
        ArgumentNullException.ThrowIfNull(message);

        _validator.ValidateMessage(message);

        using var mailMessage = CreateMailMessage(message);
        await SendMailAsync(mailMessage, cancellationToken);
    }
    public async Task SendTokensEmailAsync(DataConfirmEmail dataConfirmEmail, string body)
    {
        try
        {
            //var confirmationUrl = await GenerateEmailUrl(dataConfirmEmail);

            if (string.IsNullOrEmpty(dataConfirmEmail.TokenConfirmationUrl))
            {
                // _logger.LogError("Failed to generate email confirmation URL for {Email}", dataConfirmEmail.UserAccount.Email);
                throw new EmailServicesException(AuthErrorsMessagesException.ErrorWhenGenerateEmailLink);
            }

            // var formattedUrl = dataConfirmEmail.WelcomeMessage();
            // var formattedUrl = FormatEmailUrl(dataConfirmEmail.UrlFront, dataConfirmEmail.TokenConfirmationUrl, dataConfirmEmail.UrlBack, dataConfirmEmail.UserAccount);

            // await SendAsync(To: dataConfirmEmail.UserAccount.Email, Subject: dataConfirmEmail.SubjectEmail, Body: body);

            var message = new EmailMessage(to: dataConfirmEmail.UserAccount.Email, subject: dataConfirmEmail.SubjectEmail, body: body).WithFrom("noreply@nostopti.com.br");


            await SendAsync(message);
        }
        catch (Exception ex)
        {
            // _logger.LogError(ex, "Error sending confirmation email to {Email}", dataConfirmEmail.UserAccount.Email);
            throw;
        }
    }
    public void Dispose()
    {
        if (!_disposed)
        {
            _smtpClient?.Dispose();
            _disposed = true;
        }
    }
    private MailMessage CreateMailMessage(EmailMessage message)
    {
        var from = message.From ?? _config.DefaultFrom;
        var mailMessage = new MailMessage(from, message.To, message.Subject, message.Body)
        {
            IsBodyHtml = message.IsBodyHtml
        };

        return mailMessage;
    }
    private async Task SendMailAsync(MailMessage message, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Enviando email para {To}", message.To);

            await _smtpClient.SendMailAsync(message, cancellationToken);

            _logger.LogInformation("Email enviado com sucesso para {To}", message.To);
        }
        catch (SmtpFailedRecipientException ex) when (ex.StatusCode == SmtpStatusCode.MailboxBusy)
        {
            _logger.LogWarning(@$"{EmailServicesMessagesException.MailAddressDoesNotExist} {message.To}", message.To);
            throw new EmailServicesException(@$"{EmailServicesMessagesException.MailAddressDoesNotExist} {message.To}Exception: {ex}");
        }
        catch (SmtpFailedRecipientException ex)
        {
            _logger.LogError(ex, "Falha no envio para destinatário {To}", message.To);
            throw new EmailServicesException($"Falha no envio para destinatário:{message.To} Exception: {ex}");
        }
        catch (SmtpException ex)
        {
            _logger.LogError(ex, "Erro SMTP ao enviar mensagem para {To}", message.To);
            throw new EmailServicesException($"Erro SMTP ao enviar mensagem:{message.To} Exception: {ex}");
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("Envio de email cancelado para {To}", message.To);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado ao enviar email para {To}", message.To);
            throw new EmailServicesException($"Erro inesperado ao enviar email:{message.To} Exception: {ex}");
        }
    }


}

using Application.Auth.UsersAccountsServices.EmailUsrAccountServices.dtos;
using Application.EmailServices.Dtos;

namespace Application.EmailServices.Services;

public interface ISmtpServices
{
    Task SendAsync(EmailMessage message, CancellationToken cancellationToken = default);
    Task SendTokensEmailAsync(DataConfirmEmail dataConfirmEmail, string body);
}
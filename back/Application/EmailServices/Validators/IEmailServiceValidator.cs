using Application.EmailServices.Dtos;

namespace Application.EmailServices.Validators;

public interface IEmailServiceValidator
{
    bool IsValidEmail(string email);
    void ValidateMessage(EmailMessage message);
}
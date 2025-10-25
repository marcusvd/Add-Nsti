using Application.EmailServices.Dtos;
using Application.Helpers.Inject;
using UnitOfWork.Persistence.Operations;
using System.Text.RegularExpressions;
using Application.EmailServices.Exceptions;


namespace Application.EmailServices.Validators;

public partial class EmailServiceValidator : IEmailServiceValidator
{
    private readonly IUnitOfWork _genericRepo;
    private readonly IValidatorsInject _validatorsInject;

    public EmailServiceValidator(
                    IUnitOfWork genericRepo,
                    IValidatorsInject validatorsInject
      )
    {
        _genericRepo = genericRepo;
        _validatorsInject = validatorsInject;
    }

    public bool IsValidEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
            return false;

        return EmailRegex().IsMatch(email);
    }

    public void ValidateMessage(EmailMessage message)
    {
        ArgumentNullException.ThrowIfNull(message);

        if (!IsValidEmail(message.To))
            throw new EmailServicesValidationException($"Destinatário inválido: {message.To}");

        if (!string.IsNullOrEmpty(message.From) && !IsValidEmail(message.From))
            throw new EmailServicesValidationException($"Remetente inválido: {message.From}");

        if (string.IsNullOrWhiteSpace(message.Subject))
            throw new EmailServicesValidationException("Assunto não pode ser vazio");

        if (string.IsNullOrWhiteSpace(message.Body))
            throw new EmailServicesValidationException("Corpo da mensagem não pode ser vazio");
    }
    
    [GeneratedRegex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
    private static partial Regex EmailRegex();
}

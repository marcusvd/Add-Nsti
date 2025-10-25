namespace Application.EmailServices.Exceptions;

public class EmailServicesValidationException : EmailServicesException
{
    public EmailServicesValidationException(string message) : base(message) { }
}
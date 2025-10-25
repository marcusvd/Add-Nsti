namespace Application.EmailServices.Exceptions;

public class EmailServicesException : Exception
{
    public EmailServicesException(string message) : base(message) { }
}
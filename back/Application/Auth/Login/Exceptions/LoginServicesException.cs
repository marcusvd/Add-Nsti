namespace Application.Auth.Login.Exceptions;

public class LoginServicesException : Exception
{
    public LoginServicesException(string message) : base(message) { }
}
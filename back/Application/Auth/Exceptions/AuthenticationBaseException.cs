namespace Application.Auth.Exceptions;

public class AuthenticationBaseException : Exception
{
    public AuthenticationBaseException(string message) : base(message) { }
}
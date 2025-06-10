using System;
namespace Application.Exceptions
{
    public class AuthServicesException : ApplicationException
    {
        public AuthServicesException(string message) : base(message) { }
    }
}
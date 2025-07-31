using System;

namespace Authentication.Exceptions;
public class AuthServicesException : ApplicationException
{
    public AuthServicesException(string message) : base(message) { }
}
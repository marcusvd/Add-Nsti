using System;
namespace Application.Application.Customers.Exceptions;

public class CustomerApplicationException : ApplicationException
{
    public CustomerApplicationException(string message) : base(message) { }
}
using System;
namespace Application.Application.Services.Operations.Customers.Exceptions;

public class CustomerApplicationException : ApplicationException
{
    public CustomerApplicationException(string message) : base(message) { }
}
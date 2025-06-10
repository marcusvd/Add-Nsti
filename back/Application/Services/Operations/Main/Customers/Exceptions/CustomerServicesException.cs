using System;
namespace Application.Application.Services.Operations.Main.Customers.Exceptions
{
    public class CustomerApplicationException : ApplicationException
    {
        public CustomerApplicationException(string message) : base(message) { }
    }
  
}
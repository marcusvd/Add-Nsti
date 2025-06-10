using System;
namespace Application.Exceptions
{
    public class GlobalServicesException : ApplicationException
    {
        public GlobalServicesException(string message) : base(message) { }
    }
    public class GlobalServicesSystemException : SystemException
    {
        public GlobalServicesSystemException(string message) : base(message) { }
    }
}
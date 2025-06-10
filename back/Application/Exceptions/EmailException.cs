using System;
using System.Net.Mail;

namespace Application.Exceptions
{
    public class EmailException : SmtpFailedRecipientException
    {
        public EmailException(string message) : base(message) { }
    }
}
using System.Net.Mail;
using System.Net;
using Application.Exceptions;
using System.Threading.Tasks;

namespace Application.Services.Operations.Authentication
{
    public class Email
    {
        public Email()
        {

        }


        public void SendEmail(string to, string subject, string body)
        {

            var message = new MailMessage("marcus@nostopti.com.br", to, subject, body);
            SmtpClient SmtpClient = new SmtpClient("smtp.nostopti.com.br")
            {
                Port = 587,
                Credentials = new NetworkCredential("marcus@nostopti.com.br", "Nsti@2023"),
            };
            SmtpClient.SendCompleted += (s, e) =>
            {
                SmtpClient.Dispose();
                message.Dispose();
            };
            try
            {
              SmtpClient.SendAsync(message, null);

            }
            catch (SmtpFailedRecipientException ex)
            {
                throw new EmailException($"{EmailErrosMessagesException.InvalidDomain} - {ex}");
            }
        }


    }
}

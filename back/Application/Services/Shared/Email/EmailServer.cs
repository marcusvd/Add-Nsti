using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Application.Exceptions;
using MimeKit;

namespace Application.Services.Shared.Email;

    public class EmailServer
    {
        public async Task SendAsync(string To = "register@nostopti.com.br", string From = "register@nostopti.com.br", string DisplayName = "Sonny System",
        string Subject = "Test Subject", string Body = "Test", string MailServer = "smtp.nostopti.com.br",
         int Port = 587, bool IsUseSsl = false, string UserName = "register@nostopti.com.br", string Password = "Nsti$2024")
        {
            var message = new MailMessage("register@nostopti.com.br", To, Subject, Body);
            SmtpClient SmtpClient = new SmtpClient(MailServer)
            {
                Port = 587,
                Credentials = new NetworkCredential(UserName, Password),
            };
            SmtpClient.SendCompleted += (s, e) =>
           {
               SmtpClient.Dispose();
               message.Dispose();
           };
            try
            {
                SmtpClient.SendAsync(message, null);

                await Task.CompletedTask;

            }
            catch (SmtpFailedRecipientException ex)
            {
                throw new EmailException($"{EmailErrosMessagesException.InvalidDomain} - {ex}");
            }
        }

    }
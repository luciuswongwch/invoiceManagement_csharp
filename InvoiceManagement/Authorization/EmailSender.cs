using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;

namespace InvoiceManagement.Authorization
{
    public class EmailSender : IEmailSender
    {
        private readonly IOptions<EmailSenderSettings> _emailSenderSettings;

        public EmailSender(IOptions<EmailSenderSettings> emailSenderSettings)
        {
            _emailSenderSettings = emailSenderSettings;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailToSend = new MimeMessage();
            emailToSend.From.Add(MailboxAddress.Parse(_emailSenderSettings.Value.EmailAddress));
            emailToSend.To.Add(MailboxAddress.Parse(email));
            emailToSend.Subject = subject;
            emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

            using (var emailClient = new SmtpClient())
            {
                emailClient.Connect(_emailSenderSettings.Value.SMTPServer, _emailSenderSettings.Value.SMTPPort, MailKit.Security.SecureSocketOptions.StartTls);
                emailClient.Authenticate(_emailSenderSettings.Value.EmailAddress, _emailSenderSettings.Value.EmailPassword);
                emailClient.Send(emailToSend);
                emailClient.Disconnect(true);
            }

            return Task.CompletedTask;
        }
    }

    public class EmailSenderSettings
    {
        public string SMTPServer { get; set; }
        public int SMTPPort { get; set; }
        public string EmailAddress { get; set; }
        public string EmailPassword { get; set; }
    }
}

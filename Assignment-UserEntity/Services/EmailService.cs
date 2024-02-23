using Assignment_UserEntity.Constants;
using Assignment_UserEntity.Dtos;
using Assignment_UserEntity.Services.Contract;
using MailKit.Net.Smtp;
using MimeKit;

namespace Assignment_UserEntity.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(MailboxAddress.Parse(_config.GetSection(EmailConstants.EmailConfigUserName).Value));
            emailMessage.To.Add(MailboxAddress.Parse(email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };
            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection(EmailConstants.EmailConfigServer).Value, int.Parse(_config.GetSection(EmailConstants.EmailConfigPort).Value), MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection(EmailConstants.EmailConfigSenderEmail).Value, _config.GetSection(EmailConstants.EmailConfigPassword).Value);
            await smtp.SendAsync(emailMessage);
            smtp.Disconnect(true);
        }

    }
}

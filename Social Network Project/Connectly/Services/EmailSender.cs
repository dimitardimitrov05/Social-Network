using Connectly.Contracts;
using Connectly.Helpers;
using Humanizer;
using System.Net;
using System.Net.Mail;
using System.Net.WebSockets;
using System.Text;

namespace Connectly.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _configuration;

        public EmailSender(EmailConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmailAsync(string toEmail, string subject)
        {
            // Set up SMTP client
            SmtpClient client = new SmtpClient(_configuration.SmtpServer, _configuration.Port);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_configuration.UserName, _configuration.Password);

            // Create email message
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_configuration.From);
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendLine("Verification code");
            mailBody.AppendLine();
            string code = GenerateVerificationCode();
            mailBody.AppendLine($"This is your verification code: {code}");
            mailMessage.Body = mailBody.ToString();

            // Send email
            client.Send(mailMessage);
        }

        private static string GenerateVerificationCode()
        {
            Random random = new Random();
            int code = random.Next(100000, 1000000);
            return code.ToString("D6");
        }
    }
}

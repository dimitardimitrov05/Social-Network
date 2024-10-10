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

        public void SendEmail(string toEmail, string code)
        {
            SmtpClient client = new SmtpClient(_configuration.SmtpServer, _configuration.Port);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_configuration.UserName, _configuration.Password);

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_configuration.From);
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = "Verification";
            mailMessage.IsBodyHtml = true;
            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendLine($"This is your verification code: {code}");
            mailBody.AppendFormat("Register with the same email <a href=\"https://localhost:7283/Account/Register\">here</a>");
            mailMessage.Body = mailBody.ToString();

            client.Send(mailMessage);
        }
    }
}

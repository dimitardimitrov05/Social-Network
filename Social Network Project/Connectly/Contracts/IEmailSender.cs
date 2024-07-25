namespace Connectly.Contracts
{
    public interface IEmailSender
    {
        void SendEmailAsync(string toEmail, string subject);
    }
}

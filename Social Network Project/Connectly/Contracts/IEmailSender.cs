namespace Connectly.Contracts
{
    public interface IEmailSender
    {
        void SendEmail(string toEmail, string code);
    }
}

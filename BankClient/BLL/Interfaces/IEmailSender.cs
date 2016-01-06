namespace BLL.Interfaces
{
    public interface IEmailSender
    {
        void SendVerifyToEmail(string email, string userId, string baseUrl);
        void SendLockoutNotification(string email, string login);
        void SendChangeEmail(string userId, string newEmail, string baseUrl);
        void SendSuperSecretCode(string email, string code);
    }
}
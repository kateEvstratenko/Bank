namespace BLL.Interfaces
{
    public interface IEmailSender
    {
        void SendVerifyToEmail(string email, string userId, string baseUrl);
        void SendLockoutNotification(string email, string login);
    }
}
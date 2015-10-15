using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using BLL.Interfaces;

namespace BLL.Services
{
    public class EmailSender: IEmailSender
    {
        private static readonly string EmailSenderHost = ConfigurationManager.AppSettings["EmailSenderHost"];
        private const int EmailSenderPort = 587;
        private static readonly string EmailSenderUserName = ConfigurationManager.AppSettings["EmailSenderUserName"];
        private static readonly string EmailSenderPassword = ConfigurationManager.AppSettings["EmailSenderPassword"];
        private static readonly string ConfirmEmailUrl = ConfigurationManager.AppSettings["ConfirmEmailUrl"];

        public void SendVerifyToEmail(string email, string userId, string baseUrl)
        {
            var smtpClient = GetSmtpClient();
            var mail = GetMailMessage(email, userId, baseUrl);
            smtpClient.Send(mail);
        }

        private SmtpClient GetSmtpClient()
        {
            return new SmtpClient(EmailSenderHost, EmailSenderPort)
            {
                Credentials = new NetworkCredential(EmailSenderUserName, EmailSenderPassword),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true
            };
        }

        private MailMessage GetMailMessage(string email, string userId, string baseUrl)
        {
            var mail = new MailMessage
            {
                Subject = "Завершение регистрации",
                Body = GenerateMailBody(email, userId, baseUrl),
                From = new MailAddress(EmailSenderUserName, "Kokovik")
            };
            mail.To.Add(new MailAddress(email));
            return mail;
        }

        private string GenerateMailBody(string email, string userId, string baseUrl)
        {
            return String.Format("Для завершения регистрации перейдите по ссылке: {0}/{1}?token={2}&email={3}", 
                baseUrl, ConfirmEmailUrl, userId, email);
        }
    }
}

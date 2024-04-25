using System.Net.Mail;
namespace MicroSAuth_GUser.Services
{
    public interface IEmailService
    {
        public void SendEmail(string receiverMail, string body);

    }
}

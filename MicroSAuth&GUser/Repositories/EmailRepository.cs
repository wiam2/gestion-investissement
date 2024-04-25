using MicroSAuth_GUser.Services;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace MicroSAuth_GUser.Repositories
{
    public class EmailRepository : IEmailService
    {
        public void SendEmail(string receiverMail, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("XSpace", "echberaalmail@gmail.com"));
            message.To.Add(new MailboxAddress("XSpace", receiverMail));
            message.Subject = "Reset your Password";

            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                client.Authenticate("rihabelfich@gmail.com", "wslv nzei zqeg apyf");
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}


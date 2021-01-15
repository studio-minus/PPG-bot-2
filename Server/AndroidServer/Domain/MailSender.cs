using MimeKit;
using MailKit.Net.Smtp;

namespace Domain
{
    public class MailSender
    {
        public string SMTPServer;
        public string Username;
        public string Password;
        public int Port;

        public void SendEmail(string sender, string receiverName, string receiverMail, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(sender, Username));
            message.To.Add(new MailboxAddress(receiverName, receiverMail));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = body,
            };

            using var client = new SmtpClient();
            client.Connect(SMTPServer, Port, MailKit.Security.SecureSocketOptions.StartTlsWhenAvailable);
            //client.Connect("smtp-mail.outlook.com", 587, MailKit.Security.SecureSocketOptions.StartTlsWhenAvailable);
            client.Authenticate(Username, Password);
            //client.Authenticate("mestiez@outlook.com", "Steffie17");

            client.Send(message);
            client.Disconnect(true);
        }
    }
}

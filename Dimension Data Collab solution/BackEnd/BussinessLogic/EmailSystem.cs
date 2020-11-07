using MailKit.Net.Smtp;
using MimeKit;

namespace BackEnd.BussinessLogic
{
    public class EmailSystem
    {
        public static void SendEmail(string theMessage, string email)
        {
            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 465, true);
            client.Authenticate("Carel.Haasbroekt@gmail.com", "Ca19980227");

            MimeMessage message = new MimeMessage();

            MailboxAddress from = new MailboxAddress("Admin",
            "NO-REPLY");
            message.From.Add(from);

            MailboxAddress to = new MailboxAddress("User",
            email);
            message.To.Add(to);

            message.Subject = "Email Validation";

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = theMessage;

            message.Body = bodyBuilder.ToMessageBody();

            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
        }
    }
}

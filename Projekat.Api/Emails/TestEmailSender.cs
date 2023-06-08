using Projekat.Application.Emails;

namespace Projekat.Api.Emails
{
    public class TestEmailSender : IEmailSender
    {
        public void Send(EmailMessageDto message)
        {
            System.Console.WriteLine("Sending email:");
            System.Console.WriteLine("To: " + message.SendTo);
            System.Console.WriteLine("Title: " + message.Subject);
            System.Console.WriteLine("Content: " + message.Content);
        }
    }
}

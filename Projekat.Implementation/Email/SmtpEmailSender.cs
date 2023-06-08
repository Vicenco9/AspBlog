using Projekat.Application.Emails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Implementation.Email
{
    public class SmtpEmailSender : IEmailSender
    {
        private string _fromEmail;
        private string _password;
        private int _port;
        private string _host;
        public SmtpEmailSender(string fromEmail, string password, int port, string host)
        {
            _fromEmail = fromEmail;
            _password = password;
            _port = port;
            _host = host;
        }

        public void Send(EmailMessageDto dto)
        {
            var smtp = new SmtpClient
            {
                Host = _host,
                Port = _port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(_fromEmail, _password),
                UseDefaultCredentials = false
            };

            var message = new MailMessage(_fromEmail, dto.SendTo);

            message.Subject = dto.Subject;
            message.Body = dto.Content;
            message.IsBodyHtml = true;
            smtp.Send(message);
        }
    }
}

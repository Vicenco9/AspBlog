using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Application.Emails
{
    public interface IEmailSender
    {
        void Send(EmailMessageDto dto);
    }
    public class EmailMessageDto
    {
        public string Subject { get; set; }
        public string Content { get; set; }
        public string SendTo { get; set; }
    }
}

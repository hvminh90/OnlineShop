using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace Common
{
    public class MailHelper
    {
        public void SendMail(string toEmailAddress, string subject, string content)
        {
            var fromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"];
            var fromEmailDisplayName = ConfigurationManager.AppSettings["FromEmailDisplayName"];
            var fromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"];
            var smtpHost = ConfigurationManager.AppSettings["SMTPHost"];
            var smtpPort = ConfigurationManager.AppSettings["SMTPPort"];

            bool enabledSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"]);
            string body = content;
            MailMessage message = new MailMessage(new MailAddress(fromEmailAddress,fromEmailDisplayName),new MailAddress(toEmailAddress));

            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;

            var client = new SmtpClient();
            client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
            client.Host = smtpHost;
            client.EnableSsl = enabledSsl;
            client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
            client.Send(message);
        }
    }
}

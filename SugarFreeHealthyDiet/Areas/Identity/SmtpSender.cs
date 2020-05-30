using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System;

namespace SugarFreeHealthyDiet.Areas.Identity
{
    public class SmtpSender : IEmailSender
    {
        SmtpConfiguration smtpConfiguration;
        SmtpClient smtpClient;

        public SmtpSender(SmtpConfiguration smtpConfiguration)
        {
            this.smtpConfiguration = smtpConfiguration;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            SetUpSmtpClient();

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(smtpConfiguration.Username);
            mail.IsBodyHtml = true;
            mail.Subject = string.Format("SugarFreeHealthyDiet.com - {0}", subject);
            mail.Body = message;
            mail.To.Add(email);
            Console.WriteLine("{0} {1} {2} {3}", smtpConfiguration.Server, smtpConfiguration.Port, smtpConfiguration.Username, smtpConfiguration.Password);
            smtpClient.Send(mail);

            return Task.FromResult(0);
        }

        private void SetUpSmtpClient()
        {
            smtpClient = new SmtpClient(smtpConfiguration.Server, smtpConfiguration.Port);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(smtpConfiguration.Username, smtpConfiguration.Password);
            smtpClient.EnableSsl = true;
        }
    }
}

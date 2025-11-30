using DotNetEnv;
using LetMeFix.Application.Interfaces;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public EmailService()
        {
            var basePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            var envPath = Path.Combine(basePath, "LetMeFixWeb", ".env");
            Env.Load(envPath);
        }

        public Task SendEmailAsync(string to, string subject, string body, bool isHtml = true)
        {
            var email = Env.GetString("EMAIL_USER");
            var pass = Env.GetString("EMAIL_PASS");
            var host = Env.GetString("EMAIL_HOST");
            var port = Env.GetInt("EMAIL_PORT");

            var client = new SmtpClient(host, port)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(email, pass)
            };

            var mime = new MimeMessage();
            var bodyBuilder = new BodyBuilder();
            if (isHtml) bodyBuilder.HtmlBody = body;
            else bodyBuilder.TextBody = body;
            mime.Body = bodyBuilder.ToMessageBody();
            var mailmessage = new MailMessage(from: email, to: to, subject, body);
            mailmessage.IsBodyHtml = isHtml;

            return client.SendMailAsync(mailmessage);
        }
    }
}

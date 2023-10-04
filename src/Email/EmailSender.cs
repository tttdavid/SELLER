using System;
using System.Net;
using System.Net.Mail;
using src.Email;

namespace Email
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAync(EMessage email)
        {
            string? mail = Environment.GetEnvironmentVariable("mail");
            string? pw = Environment.GetEnvironmentVariable("mailPassword");
            if(mail is null)
                mail = "Xdd";

            SmtpClient client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pw)
            };

            Console.WriteLine($"{mail} {pw}");

            return client.SendMailAsync(new MailMessage(from: mail, to: email.To, email.Subject, email.Message));
        }
    }
}

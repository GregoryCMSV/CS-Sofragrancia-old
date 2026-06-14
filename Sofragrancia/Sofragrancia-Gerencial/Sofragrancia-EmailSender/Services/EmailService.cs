using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace Sofragrancia_EmailSender.Services
{
    public class EmailService
    {
        public string User { get; private set; }
        public string Password { get; private set; }
        public string Provider { get; private set; }

        private readonly ILogger<EmailService> _logger;


        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            User = configuration["smtp:user"]!;
            Password = configuration["smtp:pass"]!;
            Provider = configuration["smtp:provider"]!;
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlFinal)
        {
            try
            {
                if (!EmailIsValid(email))
                {
                    _logger.LogWarning($"email: {email} inválido");
                    return;
                }
                var message = CreateMimeMessage(email, subject, htmlFinal);
                await SendMailMessage(message);
                _logger.LogInformation($"Email enviado para {email}");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao mandar email: {ex.Message}");
            }
        }

        private async Task SendMailMessage(MimeMessage message)
        {
            using var client = new MailKit.Net.Smtp.SmtpClient();
            await client.ConnectAsync(Provider, 587, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(User, Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }

        private MailMessage CreateMailMessage(string email, string subject, string htmlFinal)
        {
            MailMessage mailMessage = new MailMessage(User, email);
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = htmlFinal;
            mailMessage.BodyEncoding = Encoding.UTF8;
            mailMessage.SubjectEncoding = Encoding.UTF8;
            return mailMessage;
        }

        private MimeMessage CreateMimeMessage(string email, string subject, string htmlFinal)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Sofragrância", User));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = subject;
            var bodyBuilder = new BodyBuilder { HtmlBody = htmlFinal };
            message.Body = bodyBuilder.ToMessageBody();
            return message;
        }



        private System.Net.Mail.SmtpClient GetSmtpClient()
        {
            System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient(Provider, 587);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(User, Password);
            smtpClient.EnableSsl = true;
            return smtpClient;
        }

        private bool EmailIsValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            Regex regex = new Regex(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$");
            return regex.IsMatch(email);
        }

    }
}

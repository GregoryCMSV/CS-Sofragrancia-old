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
        public string Provider {  get; private set; }

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
                if (EmailIsValid(email))
                {
                    var smtp = GetSmtpClient();
                    var message = CreateMailMessage(email, subject, htmlFinal);
                    smtp.Send(message);
                    _logger.LogInformation($"Email enviado para {email}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao mandar email: {ex.Message}");
            }
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

        private SmtpClient GetSmtpClient()
        {
            SmtpClient smtpClient = new SmtpClient(Provider, 587);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(User, Password);
            smtpClient.EnableSsl = true;
            return smtpClient;
        }

        private bool EmailIsValid(string email)
        {
            if(string.IsNullOrWhiteSpace(email)) return false;
            Regex regex = new Regex(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$");
            return regex.IsMatch(email);
        }

    }
}

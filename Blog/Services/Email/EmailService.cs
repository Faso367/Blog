﻿using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using Blog.Configuration;

namespace Blog.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _settings;
        private readonly SmtpClient _client;
        // Так как HttpClient реализован криво (по крайней мере раньше соединения висели долго и все было плохо),
        // то используем SMTP клиентов

        public EmailService(IOptions<SmtpSettings> options)
        {
            _settings = options.Value;
            _client = new SmtpClient(_settings.Server)
            {
                Credentials = new NetworkCredential(_settings.Username, _settings.Password)
            };
        }
        public Task SendEmail(string email, string subject, string message)
        {
            var mailMessage = new MailMessage
            (
            _settings.From,
            email,
            subject,
            message
            );
            return _client.SendMailAsync(mailMessage);
        }
    }
}

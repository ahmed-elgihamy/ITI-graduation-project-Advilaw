using AdviLaw.Domain.Repositories;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace AdviLaw.Infrastructure.Repositories
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailConfirmationAsync(string email, string userId, string token)
        {
            var confirmationLink = $"{_configuration["AppUrl"]}/api/Auth/verify-email?userId={Uri.EscapeDataString(userId)}&token={Uri.EscapeDataString(token)}";

            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_configuration["Email:From"]));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = "Confirm your email";
            message.Body = new TextPart("html")
            {
                Text = $"<p>Please confirm your email by clicking <a href=\"{confirmationLink}\">here</a>.</p>"
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_configuration["Email:SmtpServer"],
                                    int.Parse(_configuration["Email:Port"]),
                                    SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_configuration["Email:Username"], _configuration["Email:Password"]);
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }

        public async Task SendResetPasswordEmailAsync(string email, string code)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_configuration["Email:From"]));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = "Your password reset code";

            message.Body = new TextPart("plain")
            {
                Text = $"Your reset code is: {code}. It expires in 15 minutes."
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_configuration["Email:SmtpServer"], int.Parse(_configuration["Email:Port"]), SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_configuration["Email:Username"], _configuration["Email:Password"]);
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }

    }
}

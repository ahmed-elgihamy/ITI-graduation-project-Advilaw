using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using AdviLaw.Domain.Repositories;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace AdviLaw.Infrastructure.Repositories
{
    public class EmailService: IEmailService
    {
         private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailConfirmationAsync(string email, string userId, string token)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("AdviLaw", _configuration["Email:From"]));
            emailMessage.To.Add(MailboxAddress.Parse(email));
            emailMessage.Subject = "Confirm your email";

            var confirmationLink = $"{_configuration["AppUrl"]}/api/Auth/verify-email?userId={Uri.EscapeDataString(userId)}&token={Uri.EscapeDataString(token)}";

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $"<p>Please confirm your email by clicking <a href=\"{confirmationLink}\">here</a>.</p>"
            };

            emailMessage.Body = bodyBuilder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            await smtp.ConnectAsync(
                _configuration["Email:SmtpServer"],
                int.Parse(_configuration["Email:Port"]),
                SecureSocketOptions.StartTls 
            );
            await smtp.AuthenticateAsync(_configuration["Email:Username"], _configuration["Email:Password"]);
            await smtp.SendAsync(emailMessage);
            await smtp.DisconnectAsync(true);

        }


    }
}

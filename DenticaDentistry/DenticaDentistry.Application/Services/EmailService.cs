using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace DenticaDentistry.Application.Services;

using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task SendEmailAsync(string recipient, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("System", "admin@test.com"));
        message.To.Add(new MailboxAddress("", recipient));
        message.Subject = subject;
        message.Body = new TextPart("plain")
        {
            Text = body
        };

        using (var client = new SmtpClient())
        {
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            string server = _configuration["EmailSettings:Server"];
            int port = int.Parse(_configuration["EmailSettings:Port"]);
            bool useSsl = bool.Parse(_configuration["EmailSettings:UseSsl"]);
            string username = _configuration["EmailSettings:Username"];
            string password = _configuration["EmailSettings:Password"];
        
            await client.ConnectAsync(server, port, useSsl);
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                await client.AuthenticateAsync(username, password);
            }
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}

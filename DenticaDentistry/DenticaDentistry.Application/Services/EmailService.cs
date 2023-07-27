using System.Net.Mail;

namespace DenticaDentistry.Application.Services;

using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

public class EmailService : IEmailService
{
    public async Task SendEmailAsync(string recipient, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Your Name", "admin@test.com"));
        message.To.Add(new MailboxAddress("", recipient));
        message.Subject = subject;
        message.Body = new TextPart("plain")
        {
            Text = body
        };

        using (var client = new SmtpClient())
        {
            // Set MailCatcher as the SMTP server
            client.ServerCertificateValidationCallback = (s, c, h, e) => true; // Allow any SSL certificate (for testing purposes)
            await client.ConnectAsync("localhost", 1025, false); // Connect to MailCatcher running on port 1025
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}

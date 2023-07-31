using System.Net.Mail;
using System.Text;
using DenticaDentistry.Application.Abstractions;
using Microsoft.Extensions.Configuration;

namespace DenticaDentistry.Application.Commands.Handlers;

public class SendEmailCommandHandler : ICommandHandler<SendEmail>
{
    private readonly IConfiguration _configuration;

    public SendEmailCommandHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task HandlerAsync(SendEmail command)
    {
        string server = _configuration["EmailSettings:Server"];
        int port = int.Parse(_configuration["EmailSettings:Port"]);
        bool useSsl = bool.Parse(_configuration["EmailSettings:UseSsl"]);
        string username = _configuration["EmailSettings:Username"];
        string password = _configuration["EmailSettings:Password"];

        using (var message = new MailMessage("admin@test.com", command.Recipient))
        {
            message.Subject = command.Subject;
            message.Body = command.Body;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = false;

            using (var client = new SmtpClient())
            {
                client.Host = server;
                client.Port = port;
                client.EnableSsl = useSsl;
                client.UseDefaultCredentials = false;
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    client.Credentials = new System.Net.NetworkCredential(username, password);
                }

                await client.SendMailAsync(message);
            }
        }
    }
}
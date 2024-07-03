using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;

namespace CarRentalPro.Services
{
    public class MailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_configuration["EmailSettings:SenderName"], _configuration["EmailSettings:SenderEmail"]));
            message.To.Add(new MailboxAddress("wissamcarrental24@gmail.com", to));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = body };

            using (var client = new SmtpClient())
            {
                client.Connect(_configuration["EmailSettings:smtp.gmail.com"], int.Parse(_configuration["EmailSettings:465"]), true);
                client.Authenticate(_configuration["EmailSettings:wissamcarrental24@gmail.com"], _configuration["EmailSettings:24CarRental24"]);
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}

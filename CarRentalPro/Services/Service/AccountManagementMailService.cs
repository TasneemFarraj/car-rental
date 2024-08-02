using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using MailKit.Security;
using System;
using System.Collections.Generic;

namespace CarRentalPro.Services
{
    public class AccountManagementMailService
    {
        private readonly IConfiguration _configuration;

        public AccountManagementMailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendAccountModificationEmail(string to, string userName, List<string> changes)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_configuration["EmailSettings:SenderName"], _configuration["EmailSettings:SenderEmail"]));
                message.To.Add(new MailboxAddress("Car Rental", to));
                message.Subject = "Account Modification Notification";

                var changesDescription = string.Join("\n", changes);

                message.Body = new TextPart("plain")
                {
                    Text = $"Dear {userName},\n\nThe following changes have been made to your account:\n\n{changesDescription}\n\nIf you did not make these changes or have any questions, please contact our support team immediately.\n\nBest regards,\nCar Rental Team"
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(_configuration["EmailSettings:SmtpServer"], int.Parse(_configuration["EmailSettings:SmtpPort"]), SecureSocketOptions.StartTls);
                    client.Authenticate(_configuration["EmailSettings:Username"], _configuration["EmailSettings:Password"]);
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending account modification email: {ex.Message}");
               
            }
        }
    }
}

using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using CarRentalPro.Data.Models;

namespace CarRentalPro.Services
{
    public class SendPaymentConfirmationEmailService
    {
        private readonly IConfiguration _configuration;

        public SendPaymentConfirmationEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendPaymentConfirmationEmail(string to, CarReservation reservation, string userName)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_configuration["EmailSettings:SenderName"], _configuration["EmailSettings:SenderEmail"]));
            message.To.Add(new MailboxAddress("Car Rental", to));
            message.Subject = "Payment Confirmation";
            message.Body = new TextPart("plain")
            {
                Text = $"Dear {userName},\n" +
                       $"Your payment has been successfully processed.\n" +
                       $"Reservation Details:\n" +
                       $"Reservation Code: {reservation.ReservationCode}\n" +
                       $"Car: {reservation.CarDetails.Name} {reservation.CarDetails.Type}\n" +
                       $"Pick-Up Date: {reservation.PickUpDate}\n" +
                       $"Drop-Off Date: {reservation.DropOffDate}\n"
            };

            using (var client = new SmtpClient())
            {
                client.Connect(_configuration["EmailSettings:SmtpServer"], int.Parse(_configuration["EmailSettings:SmtpPort"]), MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate(_configuration["EmailSettings:Username"], _configuration["EmailSettings:Password"]);
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}

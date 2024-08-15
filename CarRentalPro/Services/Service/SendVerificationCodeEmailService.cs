using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Cryptography;

namespace CarRentalPro.Services
{
    public class SendVerificationCodeEmailService
    {
        private readonly IConfiguration _configuration;

        public SendVerificationCodeEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string SendVerificationCode(string to)
        {
            var verificationCode = GenerateVerificationCode();
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_configuration["EmailSettings:SenderName"], _configuration["EmailSettings:SenderEmail"]));
            message.To.Add(new MailboxAddress("Car Rental", to));
            message.Subject = "Your Verification Code";
            message.Body = new TextPart("plain") { Text = $"Your verification code is: {verificationCode}" };

            using (var client = new SmtpClient())
            {
                client.Connect(_configuration["EmailSettings:SmtpServer"], int.Parse(_configuration["EmailSettings:SmtpPort"]), MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate(_configuration["EmailSettings:Username"], _configuration["EmailSettings:Password"]);
                client.Send(message);
                client.Disconnect(true);
            }

            return verificationCode;
        }

        internal bool VerifyCode(object email, string enteredCode)
        {
            throw new NotImplementedException();
        }

        private string GenerateVerificationCode()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[4];
                rng.GetBytes(bytes);
                int code = BitConverter.ToInt32(bytes, 0) % 1000000;
                return Math.Abs(code).ToString("D6");
            }
        }
    }
}

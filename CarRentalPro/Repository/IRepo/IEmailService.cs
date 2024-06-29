namespace CarRentalPro.Repository.IRepo
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string message);

    }
}

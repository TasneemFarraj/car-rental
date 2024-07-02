using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
namespace CarRentalPro.Repository.IRepo
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string message);

    }
}

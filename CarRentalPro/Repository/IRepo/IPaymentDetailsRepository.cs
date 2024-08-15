using CarRentalPro.Data.Models;
using System.Threading.Tasks;

namespace CarRentalPro.Repository.IRepo
{
    public interface IPaymentDetailsRepository
    {
        Task<PaymentDetails> GetAsync(int Id);
        Task AddPaymentDetails(PaymentDetails paymentDetails);
        Task UpdatePaymentDetails(PaymentDetails paymentDetails);

    }
}

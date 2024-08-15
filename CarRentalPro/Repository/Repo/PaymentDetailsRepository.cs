using CarRentalPro.Data.Models;
using CarRentalPro.Repository.IRepo;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CarRentalPro.Repository.Repo
{
    public class PaymentDetailsRepository : IPaymentDetailsRepository
    {
        private readonly AppDBContext _db;

        public PaymentDetailsRepository(AppDBContext db)
        {
            _db = db;
        }

        public async Task<PaymentDetails> GetAsync(int Id)
        {
            return await _db.PaymentDetails
                .Include(r => r.CarReservation)
                .FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task AddPaymentDetails(PaymentDetails paymentDetails)
        {
            await _db.PaymentDetails.AddAsync(paymentDetails);
            await _db.SaveChangesAsync();
        }
        public async Task UpdatePaymentDetails(PaymentDetails paymentDetails)
        {
            _db.PaymentDetails.Update(paymentDetails);
            await _db.SaveChangesAsync();
        }
    }
}

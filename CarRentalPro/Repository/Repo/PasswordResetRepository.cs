using CarRentalPro.Data.Models;
using CarRentalPro.Repository.IRepo;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CarRentalPro.Repository.Repo
{
    public class PasswordResetRepository : IPasswordResetRepository
    {
        private readonly AppDBContext _db;

        public PasswordResetRepository(AppDBContext db)
        {
            _db = db;
        }

        public async Task AddPasswordReset(PasswordReset passwordReset)
        {
            await _db.PasswordReset.AddAsync(passwordReset);
            await _db.SaveChangesAsync();
        }

        public async Task<PasswordReset> GetByUserIdAsync(int userId)
        {
            return await _db.PasswordReset.SingleOrDefaultAsync(pr => pr.UserId == userId);
        }

        public async Task DeletePasswordReset(int id)
        {
            var passwordReset = await _db.PasswordReset.FindAsync(id);
            if (passwordReset != null)
            {
                _db.PasswordReset.Remove(passwordReset);
                await _db.SaveChangesAsync();
            }
        }
    }
}

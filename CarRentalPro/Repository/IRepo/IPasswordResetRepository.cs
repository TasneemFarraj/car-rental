using CarRentalPro.Data.Models;
using System.Threading.Tasks;

namespace CarRentalPro.Repository.IRepo
{
    public interface IPasswordResetRepository
    {
        Task AddPasswordReset(PasswordReset passwordReset);
        Task<PasswordReset> GetByUserIdAsync(int userId);
        Task DeletePasswordReset(int id);
    }
}

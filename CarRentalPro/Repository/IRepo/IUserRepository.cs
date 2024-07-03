using CarRentalPro.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRentalPro.Repository.IRepo
{
    public interface IUserRepository
    {
        Task<User> GetAsync(int Id);
        Task<List<User>> GetListAsync();
        Task AddUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(int Id);
        Task<User> LoginUser(User user);
      /*  Task SetResetPin(string email, string pin);
        Task<bool> ValidateResetPin(string email, string pin);
        Task ResetPassword(string email, string newPassword);*/
    }
}

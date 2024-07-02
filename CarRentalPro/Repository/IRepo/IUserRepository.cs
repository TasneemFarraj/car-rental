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
        Task<User> LoginUser(string username, string password);
        Task<User> ForgetPasswordUser(User user);
        Task<User> GetUserByEmailAsync(string email);
    }
}

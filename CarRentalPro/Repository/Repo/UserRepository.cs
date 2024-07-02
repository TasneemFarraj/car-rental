using CarRentalPro.Data.Models;
using CarRentalPro.Repository.IRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalPro.Repository.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDBContext _db;

        public UserRepository(AppDBContext db)
        {
            _db = db;
        }

        public async Task AddUser(User user)
        {
            await _db.User.AddAsync(user);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteUser(int Id)
        {
            var data = await _db.User.FindAsync(Id);
            if (data != null)
            {
                _db.User.Remove(data);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<User> ForgetPasswordUser(User user)
        {
            var existingUser = await _db.User.FirstOrDefaultAsync(u => u.Email == user.Email);

            if (existingUser != null)
            {
                existingUser.ResetPin = GenerateResetPin();
                existingUser.ResetPinExpiration = DateTime.UtcNow.AddHours(1); 
                _db.User.Update(existingUser);
                await _db.SaveChangesAsync();
            }

            return existingUser;
        }

        private string GenerateResetPin()
        {
           
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 6)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<User> GetAsync(int Id)
        {
            var data = await _db.User.FindAsync(Id);
            return data;
        }

        public async Task<List<User>> GetListAsync()
        {
            var data = await _db.User.ToListAsync();
            return data;
        }


        public async Task<User> LoginUser(string username, string password)
        {
            return await _db.User.SingleOrDefaultAsync(u => u.UserName == username && u.Password == password);
        }




        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _db.User.Where(u => u.Email == email).SingleOrDefaultAsync();
        }

        public async Task UpdateUser(User user)
        {
            var data = await _db.User.FindAsync(user.Id);
            if (data != null)
            {
                data.UserName = user.UserName;
                data.UserType = user.UserType;
                data.Password = user.Password;
                data.PasswordConfirm = user.PasswordConfirm;
                data.ResetPin = user.ResetPin;

                _db.User.Update(data);
                await _db.SaveChangesAsync();
            }
        }
    }
}

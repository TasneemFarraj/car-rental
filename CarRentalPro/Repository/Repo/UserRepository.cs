using CarRentalPro.Data.Models;
using CarRentalPro.Repository.IRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;

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

        public async Task<User> GetAsync(int Id)
        {
            return await _db.User.FindAsync(Id);
        }

        public async Task<List<User>> GetListAsync()
        {
            return await _db.User.ToListAsync();
        }

        public async Task<User> LoginUser(User user)
        {
            return await _db.User.SingleOrDefaultAsync(x => x.Password == user.Password && x.UserName == user.UserName);
        }

        public async Task UpdateUser(User user)
        {
            var data = await _db.User.FindAsync(user.Id);
            if (data != null)
            {
                data.UserName = user.UserName;
                data.UserType = user.UserType;
                data.FirstName = user.FirstName;
                data.LastName = user.LastName;
                data.Email = user.Email;
                data.Password = user.Password;
                data.PasswordConfirm = user.PasswordConfirm;

                _db.User.Update(data);
                await _db.SaveChangesAsync();
            }
        }
   

/*
public async Task SetResetPin(string email, string pin)
        {
            var user = await _db.User.SingleOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                user.ResetPin = pin;
                user.ResetPinExpiration = DateTime.UtcNow.AddMinutes(15); 
                await _db.SaveChangesAsync();
            }
        }

        public async Task<bool> ValidateResetPin(string email, string pin)
        {
            var user = await _db.User.SingleOrDefaultAsync(u => u.Email == email);
            return user != null && user.ResetPin == pin && user.ResetPinExpiration > DateTime.UtcNow;
        }

        public async Task ResetPassword(string email, string newPassword)
        {
            var user = await _db.User.SingleOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
                user.ResetPin = null;
                user.ResetPinExpiration = null;
                await _db.SaveChangesAsync();
            }
        }*/
    }
}
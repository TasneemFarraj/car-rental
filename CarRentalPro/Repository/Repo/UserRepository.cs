using CarRentalPro.Data.Models;
using CarRentalPro.Repository.IRepo;
using CarRentalPro.Services;
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
        private readonly MailService _mailService;

        public UserRepository(AppDBContext db, MailService mailService)
        {
            _db = db;
            _mailService = mailService;
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
            var existingUser = await _db.User.FindAsync(user.Id);
            if (existingUser != null)
            {
                var originalEmail = existingUser.Email;

                existingUser.UserName = user.UserName;
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                existingUser.PhoneNumber = user.PhoneNumber;
                existingUser.UserType = user.UserType;

                
                _db.User.Update(existingUser);
                await _db.SaveChangesAsync();

                if (originalEmail != user.Email)
                {
                    SendEmailChangeNotification(existingUser, originalEmail);
                }
            }
        }

        private void SendEmailChangeNotification(User user, string originalEmail)
        {
            var subject = "Your account details have been updated";
            var body = $"Dear {user.FirstName},\n\n" +
                       $"Your account email has been changed from {originalEmail} to {user.Email}.\n\n" +
                       "If you did not make this change, please contact support immediately.\n\n" +
                       "Best regards,\nCar Rental Team";

            _mailService.SendEmail(user.Email, subject, body);
            _mailService.SendEmail(originalEmail, subject, body);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _db.User.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            return await _db.User.FindAsync(userId);
        }
    }
}

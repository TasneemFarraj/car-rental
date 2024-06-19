using CarRentalPro.Data.Models;
using CarRentalPro.Repository.IRepo;
using Microsoft.EntityFrameworkCore;

namespace CarRentalPro.Repository.Repo
{
    public class ContactRepository: IContactRepository
    {
        private readonly AppDBContext _db;

        public ContactRepository(AppDBContext db)
        {
            _db = db;
        }
        public async Task AddContact(Contact contact)
        {
            await _db.Contact.AddAsync(contact);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteContact(int Id)
        {
            var data = await _db.Contact.FindAsync(Id);
            if (data != null)
            {
                _db.Contact.Remove(data);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<Contact> GetAsync(int Id)
        {
            var data = await _db.Contact.FindAsync(Id);
            return data;
        }

        public async Task<List<Contact>> GetListAsync()
        {

            var data = await _db.Contact.ToListAsync();
            return data;
        }

        public async Task UpdateContact(Contact contact)
        {
            var data = await _db.Contact.FindAsync(contact.Id);
            if (data != null)
            {
                data.Id = contact.Id;
                data.Image = contact.Image;
                data.PhoneNumber = contact.PhoneNumber;
                data.Email = contact.Email;
                data.RentalPoints = contact.RentalPoints;
                data.pages=contact.pages;
                data.WorkingHours = contact.WorkingHours;
            }
            _db.Contact.Update(data);
            _db.SaveChanges();
        }
    }

}


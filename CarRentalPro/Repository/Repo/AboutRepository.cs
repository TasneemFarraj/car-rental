using CarRentalPro.Data.Models;
using CarRentalPro.Repository.IRepo;
using Microsoft.EntityFrameworkCore;

namespace CarRentalPro.Repository.Repo
{
    public class AboutRepository : IAboutRepository
    {
        private readonly AppDBContext _db;

        public AboutRepository(AppDBContext db)
        {
            _db = db;
        }
        public async Task AddAbout(About about)
        {
            await _db.About.AddAsync(about);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAbout(int Id)
        {
            var data = await _db.About.FindAsync(Id);
            if (data != null)
            {
                _db.About.Remove(data);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<About> GetAsync(int Id)
        {
            var data = await _db.About.FindAsync(Id);
            return data;
        }

        public async Task<List<About>> GetListAsync()
        {

            var data = await _db.About.ToListAsync();
            return data;
        }

        public async Task UpdateAbout(About about)
        {
            var data = await _db.About.FindAsync(about.Id);
            if (data != null)
            {
                data.Id = about.Id;
                data.Title = about.Title;
                data.Description = about.Description;
                data.Image = about.Image;
                data.CarTypes = about.CarTypes;
                data.RentalPoints = about.RentalPoints;
                data.Customers = about.Customers;
                data.TotalCars = about.TotalCars;
                data.Offers = about.Offers;

                _db.About.Update(data);
                _db.SaveChanges();
            }

        }
    }
}


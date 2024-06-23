using CarRentalPro.Data;
using CarRentalPro.Data.Models;
using CarRentalPro.Repository.IRepo;
using Microsoft.EntityFrameworkCore;


namespace CarRentalPro.Repository.Repo
{
    public class HomeRepository : IHomeRepository
    {
        private readonly AppDBContext _db;

        public HomeRepository(AppDBContext db)
        {
            _db = db;
        }

        public async Task AddHome(Home home)
        {

            await _db.Home.AddAsync(home);
            await _db.SaveChangesAsync();

        }

        public async Task DeleteHome(int Id)
        {
            var data = await _db.Home.FindAsync(Id);
            if (data != null)
            {
                _db.Home.Remove(data);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<Home> GetAsync(int Id)
        {
            var data = await _db.Home.FindAsync(Id);
            return data;
        }

        public async Task<List<Home>> GetListAsync()
        {
            var data = await _db.Home.ToListAsync();
            return data;
        }

        public async Task UpdateHome(Home home)
        {
            var data = await _db.Home.FindAsync(home.Id);
            if (data != null)
            {
                data.Id = home.Id;
                data.UserName = home.UserName;
                data.NavBar= home.NavBar;
                data.Image= home.Image;
                data.Title= home.Title;
                data.Description= home.Description;
               
             _db.Home.Update(data);
             _db.SaveChanges();
            }
        }
    }
}

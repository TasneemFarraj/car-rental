using CarRentalPro.Data.Models;
using CarRentalPro.Repository.IRepo;
using Microsoft.EntityFrameworkCore;

namespace CarRentalPro.Repository.Repo
{
    public class WhyRepositoy: IWhyRepositoy
    {
        private readonly AppDBContext _db;

        public WhyRepositoy(AppDBContext db)
        {
            _db = db;
        }
        public async Task AddWhy(Why why)
        {
            await _db.Why.AddAsync(why);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteWhy(int Id)
        {
            var data = await _db.Why.FindAsync(Id);
            if (data != null)
            {
                _db.Why.Remove(data);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<Why> GetAsync(int Id)
        {
            var data = await _db.Why.FindAsync(Id);
            return data;
        }

        public async Task<List<Why>> GetListAsync()
        {

            var data = await _db.Why.ToListAsync();
            return data;
        }

        public async Task UpdateWhy(Why why)
        {
            var data = await _db.Why.FindAsync(why.Id);
            if (data != null)
            {
                data.Id = why.Id; 
                data.Title = why.Title;
                data.Description = why.Description;
                data.Image= why.Image;
            

            _db.Why.Update(data);
            _db.SaveChanges();
        }
    }
}

}
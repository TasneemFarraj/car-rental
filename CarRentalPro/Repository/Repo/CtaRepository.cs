using CarRentalPro.Data.Models;
using CarRentalPro.Repository.IRepo;
using Microsoft.EntityFrameworkCore;

namespace CarRentalPro.Repository.Repo;

    public class CtaRepository : ICtaRepository
    {
        private readonly AppDBContext _db;

        public CtaRepository(AppDBContext db)
        {
            _db = db;
        }
        public async Task AddCta(Cta cta)
        {
            await _db.Cta.AddAsync(cta);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteCta(int Id)
        {
            var data = await _db.Cta.FindAsync(Id);
            if (data != null)
            {
                _db.Cta.Remove(data);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<Cta> GetAsync(int Id)
        {
            var data = await _db.Cta.FindAsync(Id);
            return data;
        }

        public async Task<List<Cta>> GetListAsync()
        {
            var data = await _db.Cta.ToListAsync();
            return data;
        }

        public async Task UpdateCta(Cta cta)
        {
            var data = await _db.Cta.FindAsync(cta.Id);
            if (data != null)
            {
                data.Id = cta.Id;
                data.Image = cta.Image;
            
            _db.Cta.Update(data);
            _db.SaveChanges();
        }
    }

}
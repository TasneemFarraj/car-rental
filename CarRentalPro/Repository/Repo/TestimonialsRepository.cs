using CarRentalPro.Data.Models;
using CarRentalPro.Repository.IRepo;
using Microsoft.EntityFrameworkCore;

namespace CarRentalPro.Repository.Repo
{
    public class TestimonialsRepository: ITestimonialsRepository
    {
        
    private readonly AppDBContext _db;

        public TestimonialsRepository(AppDBContext db)
        {
            _db = db;
        }
        public async Task AddTestimonials(Testimonials testimonials)
        {
            await _db.Testimonials.AddAsync(testimonials);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteTestimonials(int Id)
        {
            var data = await _db.Testimonials.FindAsync(Id);
            if (data != null)
            {
                _db.Testimonials.Remove(data);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<Testimonials> GetAsync(int Id)
        {
            var data = await _db.Testimonials.FindAsync(Id);
            return data;
        }

        public async Task<List<Testimonials>> GetListAsync()
        {

            var data = await _db.Testimonials.ToListAsync();
            return data;
        }

        public async Task UpdateTestimonials(Testimonials testimonials)
        {
            var data = await _db.Testimonials.FindAsync(testimonials.Id);
            if (data != null)
            {
                data.Id = testimonials.Id;
                data.Image = testimonials.Image;
                data.Name = testimonials.Name;
                data.Job = testimonials.Job;
                data.Message= testimonials.Message;

            }
            _db.Testimonials.Update(data);
            _db.SaveChanges();
        }
    }
}

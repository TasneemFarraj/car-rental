using CarRentalPro.Data.Models;

namespace CarRentalPro.Repository.IRepo
{
    public interface ITestimonialsRepository
    {
        Task<Testimonials> GetAsync(int Id);
        Task<List<Testimonials>> GetListAsync();
        Task AddTestimonials(Testimonials testimonials);
        Task UpdateTestimonials(Testimonials testimonials);
        Task DeleteTestimonials(int Id);
    }
}


using CarRentalPro.Data.Models;

namespace CarRentalPro.Repository.IRepo
{
    public interface ICtaRepository
    {
        Task<Cta> GetAsync(int Id);
        Task<List<Cta>> GetListAsync();
        Task AddCta(Cta cta);
        Task UpdateCta(Cta cta);
        Task DeleteCta(int Id);
    }
}


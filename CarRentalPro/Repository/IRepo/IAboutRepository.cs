using CarRentalPro.Data.Models;

namespace CarRentalPro.Repository.IRepo
{
    public interface IAboutRepository
    {
       
            Task<About> GetAsync(int Id);
            Task<List<About>> GetListAsync();
            Task AddAbout(About about);
            Task UpdateAbout(About about);
            Task DeleteAbout(int Id);
        
    }
}

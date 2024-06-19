using CarRentalPro.Data.Models;

namespace CarRentalPro.Repository.IRepo
{
    public interface IWhyRepositoy
    {
            Task<Why> GetAsync(int Id);
            Task<List<Why>> GetListAsync();
            Task AddWhy(Why why);
            Task UpdateWhy(Why why);
            Task DeleteWhy(int Id);    
    }
}

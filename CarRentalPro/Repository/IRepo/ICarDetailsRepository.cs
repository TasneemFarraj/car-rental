using CarRentalPro.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRentalPro.Repository.IRepo
{
    public interface ICarDetailsRepository
    {
        Task<CarDetails> GetAsync(int Id);
        Task<List<CarDetails>> GetListAsync();
        Task<List<CarDetails>> GetListAsync(CarFilter filter); 
        Task AddCarDetails(CarDetails cardetails);
        Task UpdateCarDetails(CarDetails cardetails);
        Task DeleteCarDetails(int Id);
    }
}

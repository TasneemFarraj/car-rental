using CarRentalPro.Data.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CarRentalPro.Repository.IRepo
{
    public interface ICarFilterRepository
    {
        Task<List<CarDetails>> GetFilteredCarsAsync(CarFilter carFilter);
    }
}

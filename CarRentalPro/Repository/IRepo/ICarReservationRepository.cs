using CarRentalPro.Data.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CarRentalPro.Repository.IRepo
{
    public interface ICarReservationRepository
    {
        Task<CarReservation> GetAsync(int Id);
        Task<List<CarReservation>> GetListAsync();
        Task AddCarReservation(CarReservation reservation);
        Task UpdateCarReservation(CarReservation reservation);
        Task DeleteCarReservation(int Id);
    }
}

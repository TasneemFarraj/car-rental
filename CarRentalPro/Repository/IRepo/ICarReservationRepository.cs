using CarRentalPro.Data.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CarRentalPro.Repository.IRepo
{
    public interface ICarReservationRepository
    {
        Task<CarReservation> GetAsync(int Id);
        Task<List<CarReservation>> GetAllReservations();
        Task AddCarReservation(CarReservation reservation);
        Task ConfirmReservation(int Id);
        Task DeleteCarReservation(int Id);
    }
}

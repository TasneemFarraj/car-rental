using CarRentalPro.Data.Models;
using CarRentalPro.Repository.IRepo;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CarRentalPro.Repository.Repo
{
    public class CarReservationRepository : ICarReservationRepository
    {
        private readonly AppDBContext _db;

        public CarReservationRepository(AppDBContext db)
        {
            _db = db;
        }

        public async Task<CarReservation> GetAsync(int Id)
        {
            return await _db.CarReservation
                .Include(c => c.CarDetails)
                .Include(u => u.User)
                .FirstOrDefaultAsync(r => r.Id == Id);
        }

        public async Task<List<CarReservation>> GetListAsync()
        {
            return await _db.CarReservation
                .Include(c => c.CarDetails)
                .Include(u => u.User)
                .ToListAsync();
        }

        public async Task AddCarReservation(CarReservation reservation)
        {
            await _db.CarReservation.AddAsync(reservation);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateCarReservation(CarReservation reservation)
        {
            _db.CarReservation.Update(reservation);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteCarReservation(int Id)
        {
            var reservation = await _db.CarReservation.FindAsync(Id);
            if (reservation != null)
            {
                _db.CarReservation.Remove(reservation);
                await _db.SaveChangesAsync();
            }
        }
    }
}

using CarRentalPro.Data.Models;
using CarRentalPro.Repository.IRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRentalPro.Repository.Repo
{
    public class CarReservationRepository : ICarReservationRepository
    {
        private readonly AppDBContext _db;

        public CarReservationRepository(AppDBContext db)
        {
            _db = db;
        }

        public async Task AddCarReservation(CarReservation reservation)
        {
            reservation.ReservationCode = GenerateUniqueReservationCode();
            reservation.IsConfirmed = false;
            await _db.CarReservation.AddAsync(reservation);
            await _db.SaveChangesAsync();
        }

        public async Task ConfirmReservation(int carReservationId)
        {
            var reservation = await _db.CarReservation
                                       .Include(r => r.CarDetails)
                                       .Include(r => r.User)
                                       .FirstOrDefaultAsync(r => r.Id == carReservationId);

            if (reservation == null)
                throw new KeyNotFoundException("Reservation not found.");

            var paymentDetails = await _db.PaymentDetails
                                          .FirstOrDefaultAsync(p => p.CarReservationId == reservation.Id);

            if (paymentDetails != null && paymentDetails.IsPaid)
            {
                reservation.IsConfirmed = true;
                _db.CarReservation.Update(reservation);
                await _db.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Payment has not been made, reservation cannot be confirmed.");
            }
        }

        public async Task<CarReservation> GetAsync(int id)
        {
            return await _db.CarReservation
                            .Include(r => r.CarDetails)
                            .Include(r => r.User)
                            .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<CarReservation>> GetAllReservations()
        {
            return await _db.CarReservation
                            .Include(r => r.CarDetails)
                            .Include(r => r.User)
                            .ToListAsync();
        }

        private string GenerateUniqueReservationCode()
        {
            var guid = Guid.NewGuid();
            return guid.ToString().Replace("-", "").Substring(0, 8).ToUpper();
        }

        public async Task DeleteCarReservation(int id)
        {
            var reservation = await _db.CarReservation.FindAsync(id);
            if (reservation != null)
            {
                _db.CarReservation.Remove(reservation);
                await _db.SaveChangesAsync();
            }
        }
    }
}

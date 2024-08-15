using CarRentalPro.Data.Models;
using CarRentalPro.Repository.IRepo;
using Microsoft.EntityFrameworkCore;

namespace CarRentalPro.Repository.Repo
{
    public class CarDetailsRepository : ICarDetailsRepository
    {
        private readonly AppDBContext _db;

        public CarDetailsRepository(AppDBContext db)
        {
            _db = db;
        }

        public async Task AddCarDetails(CarDetails cardetails)
        {
            cardetails.IsAvailable = true;
            await _db.CarDetails.AddAsync(cardetails);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteCarDetails(int Id)
        {
            var data = await _db.CarDetails.FindAsync(Id);
            if (data != null)
            {
                _db.CarDetails.Remove(data);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<CarDetails> GetAsync(int Id)
        {
            var data = await _db.CarDetails.FindAsync(Id);
            return data;
        }

        public async Task<List<CarDetails>> GetListAsync()
        {
            var data = await _db.CarDetails.ToListAsync();
            return data;
        }

      

        public async Task UpdateCarDetails(CarDetails cardetails)
        {
            var data = await _db.CarDetails.FindAsync(cardetails.Id);
            if (data != null)
            {
                data.Id = cardetails.Id;
                data.Type = cardetails.Type;
                data.Name = cardetails.Name;
                data.Image = cardetails.Image;
                data.Description = cardetails.Description;
                data.PricePerDay = cardetails.PricePerDay;
                data.Stars = cardetails.Stars;
                data.Seat = cardetails.Seat;
                data.Petrol = cardetails.Petrol;
                data.Door = cardetails.Door;
                data.Transmission = cardetails.Transmission;
                data.SuitCase = cardetails.SuitCase;

                _db.CarDetails.Update(data);
                _db.SaveChanges();
            }
        }
    }
}

using CarRentalPro.Data.Models;
using CarRentalPro.Repository.IRepo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalPro.Repository.Repo
{
    public class CarFilterRepository : ICarFilterRepository
    {
        private readonly AppDBContext _db;

        public CarFilterRepository(AppDBContext db)
        {
            _db = db;
        }

        public async Task<List<CarDetails>> GetFilteredCarsAsync(CarFilter carFilter)
        {
            var query = _db.CarDetails.AsQueryable();

            if (carFilter.CityId > 0)
            {
                query = query.Where(car => car.CityId == carFilter.CityId);
            }

            if (carFilter.PickUpDate != default(DateTime))
            {
                query = query.Where(car => !car.CarReservations.Any(reservation =>
                    reservation.PickUpDate <= carFilter.DropOffDate &&
                    reservation.DropOffDate >= carFilter.PickUpDate));
            }

            return await query.ToListAsync();
        }
    }
}

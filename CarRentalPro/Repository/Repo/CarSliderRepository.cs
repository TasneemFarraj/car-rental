using CarRentalPro.Data.Models;
using CarRentalPro.Repository.IRepo;
using Microsoft.EntityFrameworkCore;

namespace CarRentalPro.Repository.Repo;

public class CarSliderRepository : ICarSliderRepository
{
    private readonly AppDBContext _db;

    public CarSliderRepository(AppDBContext db)
    {
        _db = db;
    }
    public async Task AddCarSlider(CarSlider carslider)
    {
        await _db.CarSlider.AddAsync(carslider);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteCarSlider(int Id)
    {
        var data = await _db.CarSlider.FindAsync(Id);
        if (data != null)
        {
            _db.CarSlider.Remove(data);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<CarSlider> GetAsync(int Id)
    {
        var data = await _db.CarSlider.FindAsync(Id);
        return data;
    }

    public async Task<List<CarSlider>> GetListAsync()
    {

        var data = await _db.CarSlider.ToListAsync();
        return data;
    }

    public async Task UpdateCarSlider(CarSlider carslider)
    {
        var data = await _db.CarSlider.FindAsync(carslider.Id);
        if (data != null)
        {
            data.Id = carslider.Id;
            data.Type = carslider.Type;
            data.Name = carslider.Name;
            data.Image = carslider.Image;
            data.Price = carslider.Price;
            data.Stars = carslider.Stars;
            data.Seat = carslider.Seat;
            data.Petrol = carslider.Petrol;
            data.Door = carslider.Door;
            data.Automatic = carslider.Automatic;
            data.SuitCase = carslider.SuitCase;

        }
        _db.CarSlider.Update(data);
        _db.SaveChanges();
    }
}

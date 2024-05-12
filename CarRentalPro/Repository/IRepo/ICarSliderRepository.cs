using CarRentalPro.Data.Models;
using CarRentalPro.Repository.Repo;

namespace CarRentalPro.Repository.IRepo
{
    public interface ICarSliderRepository
    {
        Task<CarSlider> GetAsync(int Id);
        Task<List<CarSlider>> GetListAsync();
        Task AddCarSlider(CarSlider carslider);
        Task UpdateCarSlider(CarSlider carslider);
        Task DeleteCarSlider(int Id);
    }

}


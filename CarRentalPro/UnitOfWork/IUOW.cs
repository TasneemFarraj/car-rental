using CarRentalPro.Repository.IRepo;
using CarRentalPro.Repository.Repo;

namespace CarRentalPro.UnitOfWork
{
    public interface IUOW 
    {
        public IUserRepository User { get; set; }
        public ICarSliderRepository CarSlider { get; set; }

    }
}

using CarRentalPro.Data.Models;
using CarRentalPro.Repository.IRepo;

namespace CarRentalPro.UnitOfWork;
public class UOW : IUOW

{
    public UOW(IUserRepository user, ICarSliderRepository carslider)
    {
        User = user;
        CarSlider = carslider;
    }

    public IUserRepository User { get; set; }
    public ICarSliderRepository CarSlider { get; set; }
}



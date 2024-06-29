using CarRentalPro.Repository.IRepo;

namespace CarRentalPro.UnitOfWork
{
    public interface IUOW
    {
        IUserRepository User { get; set; }
        ICarSliderRepository CarSlider { get; set; }
        IWhyRepositoy Why { get; set; }
        IAboutRepository About { get; set; }
        ITestimonialsRepository Testimonials { get; set; }
        IContactRepository Contact { get; set; }
        ICtaRepository Cta { get; set; }
        IHomeRepository Home { get; set; }
    }
}

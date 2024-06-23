using CarRentalPro.Repository.IRepo;
using CarRentalPro.Repository.Repo;

namespace CarRentalPro.UnitOfWork
{
    public interface IUOW 
    {
        public IUserRepository User { get; set; }
        public ICarSliderRepository CarSlider { get; set; }
        public IWhyRepositoy Why { get; set; }
        public IAboutRepository About { get; set; }
        public ITestimonialsRepository Testimonials { get; set; }
        public IContactRepository Contact { get; set; }
        public ICtaRepository Cta { get; set; }
        public IHomeRepository Home { get; set; }


    }
}

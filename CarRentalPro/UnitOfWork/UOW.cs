using CarRentalPro.Repository.IRepo;
using CarRentalPro.Repository.Repo;

namespace CarRentalPro.UnitOfWork
{
    public class UOW : IUOW
    {
        public UOW(IUserRepository user, ICarSliderRepository carslider, IWhyRepositoy why, IAboutRepository about,
                   ITestimonialsRepository testimonials, IContactRepository contact, ICtaRepository cta, IHomeRepository home, IPasswordResetRepository passwordReset)
        {
            User = user;
            CarSlider = carslider;
            Why = why;
            About = about;
            Testimonials = testimonials;
            Contact = contact;
            Cta = cta;
            Home = home;
            PasswordReset = passwordReset;
        }

        public IUserRepository User { get; set; }
        public ICarSliderRepository CarSlider { get; set; }
        public IWhyRepositoy Why { get; set; }
        public IAboutRepository About { get; set; }
        public ITestimonialsRepository Testimonials { get; set; }
        public IContactRepository Contact { get; set; }
        public ICtaRepository Cta { get; set; }
        public IHomeRepository Home { get; set; }
        public IPasswordResetRepository PasswordReset { get; set; }
    }
}

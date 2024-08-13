using CarRentalPro.Repository.IRepo;
using CarRentalPro.Repository.Repo;

namespace CarRentalPro.UnitOfWork
{
    public interface IUOW
    {
        IUserRepository User { get; set; }
        ICarDetailsRepository CarDetails { get; set; }
        IWhyRepositoy Why { get; set; }
        IAboutRepository About { get; set; }
        ITestimonialsRepository Testimonials { get; set; }
        IContactRepository Contact { get; set; }
        ICtaRepository Cta { get; set; }
        IHomeRepository Home { get; set; }
        IPasswordResetRepository PasswordReset { get; set; }
        ICarReservationRepository CarReservation { get; set; }
        IPaymentDetailsRepository PaymentDetails { get; set; }



        Task<int> SaveChangesAsync();
    }
}

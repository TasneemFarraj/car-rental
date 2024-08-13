using CarRentalPro.Data.Models;
using CarRentalPro.Repository.IRepo;
using CarRentalPro.Repository.Repo;
using Microsoft.EntityFrameworkCore;

namespace CarRentalPro.UnitOfWork
{
    public class UOW : IUOW
    {
        private readonly AppDBContext _db;

        public UOW(AppDBContext db, IUserRepository user, IWhyRepositoy why, IAboutRepository about,
                   ITestimonialsRepository testimonials, IContactRepository contact, ICtaRepository cta, IHomeRepository home, IPasswordResetRepository passwordReset,
                 ICarReservationRepository carReservation, ICarDetailsRepository cardetails, IPaymentDetailsRepository paymentDetails)
        {
            _db = db;
            User = user;
            CarDetails = cardetails;
            Why = why;
            About = about;
            Testimonials = testimonials;
            Contact = contact;
            Cta = cta;
            Home = home;
            PasswordReset = passwordReset;
            CarReservation = carReservation;
            PaymentDetails = paymentDetails;
        }

        public IUserRepository User { get; set; }
        public ICarDetailsRepository CarDetails { get; set; }
        public IWhyRepositoy Why { get; set; }
        public IAboutRepository About { get; set; }
        public ITestimonialsRepository Testimonials { get; set; }
        public IContactRepository Contact { get; set; }
        public ICtaRepository Cta { get; set; }
        public IHomeRepository Home { get; set; }
        public IPasswordResetRepository PasswordReset { get; set; }
        public ICarReservationRepository CarReservation { get ; set; }
        public IPaymentDetailsRepository PaymentDetails { get; set; }



        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }

   
    }

}

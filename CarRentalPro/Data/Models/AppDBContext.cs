using Microsoft.EntityFrameworkCore;

namespace CarRentalPro.Data.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)

        {
        }
        public DbSet<Home> Home { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<CarDetails> CarDetails { get; set; }
        public DbSet<Why> Why { get; set; }
        public DbSet<About> About { get; set; }
        public DbSet<Testimonials> Testimonials { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Cta> Cta { get; set; }
        public DbSet<PasswordReset> PasswordReset { get; set; }
        public DbSet<CarReservation> CarReservation { get; set; }
        public DbSet<PaymentDetails> PaymentDetails { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<CarFilter> CarFilter { get; set; }



    }
}

    




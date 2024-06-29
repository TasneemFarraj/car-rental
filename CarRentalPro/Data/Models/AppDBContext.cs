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
        public DbSet<CarSlider> CarSlider { get; set; }
        public DbSet<Why> Why { get; set; }
        public DbSet<About> About { get; set; }
        public DbSet<Testimonials> Testimonials { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Cta> Cta { get; set; }
    }
}


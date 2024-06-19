using System.ComponentModel.DataAnnotations;

namespace CarRentalPro.Data.Models
{
    public class About
    {
        [Key]
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int CarTypes { get; set; }
        public int RentalPoints { get; set; }
        public int Customers { get; set; }
        public int TotalCars { get; set; }
        public int Offers { get; set; }
    }
}

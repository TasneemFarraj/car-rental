using System.ComponentModel.DataAnnotations;

namespace CarRentalPro.Data.Models
{
    public class Testimonials
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
    }
}

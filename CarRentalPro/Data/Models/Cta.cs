using System.ComponentModel.DataAnnotations;

namespace CarRentalPro.Data.Models
{
    public class Cta
    {
        [Key]
        public int Id { get; set; }
        public string? Image { get; set; }
    }
}

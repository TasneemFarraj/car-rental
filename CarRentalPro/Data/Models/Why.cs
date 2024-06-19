using System.ComponentModel.DataAnnotations;

namespace CarRentalPro.Data.Models
{
    public class Why
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}

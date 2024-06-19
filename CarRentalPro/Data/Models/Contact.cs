using System.ComponentModel.DataAnnotations;

namespace CarRentalPro.Data.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }    
        public string Image { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string RentalPoints { get; set; }
        public string pages { get; set; }
        public string WorkingHours { get; set; }
        
    }
}

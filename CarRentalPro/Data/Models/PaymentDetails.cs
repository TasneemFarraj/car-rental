using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentalPro.Data.Models
{
    public class PaymentDetails
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(CarReservationId))]   
        public int CarReservationId { get; set; } 
        public CarReservation CarReservation { get; set; }
     
        public string CardNumber { get; set; }
        public string CardholderName { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string CVV { get; set; }
        public string? DriverLicense { get; set; } 
        public bool IsPaid { get; set; }
        public string Email { get; set; }

    }
}

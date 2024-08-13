using System.ComponentModel.DataAnnotations;

namespace CarRentalPro.Data.Models
{
    public class CarFilter
    {
        [Key]
        public int Id { get; set; }
        public int CityId { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime DropOffDate { get; set; }
    }
}

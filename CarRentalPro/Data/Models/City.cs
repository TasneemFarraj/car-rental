using CarRentalPro.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace CarRentalPro.Data.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }
        public CityEnum CityName { get; set; }
    }
}

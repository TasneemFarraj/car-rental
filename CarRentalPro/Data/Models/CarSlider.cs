using CarRentalPro.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace CarRentalPro.Data.Models
{
    public class CarSlider
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public float Stars { get; set; }
        public string Image { get; set; }
        public int Door { get; set; }
        public int Seat { get; set; }
        public string Petrol { get; set; }
        public TransmissionTypeEnum Transmission { get; set; }
        public int SuitCase { get; set; }
        public CountryEnum Country { get; set; }


    }
}

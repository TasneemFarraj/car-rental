using System.ComponentModel.DataAnnotations;

namespace CarRentalPro.Dtos.CredDtos
{
    public class WriteCarSliderDto
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public float Stars { get; set; }
        public IFormFile Image { get; set; }
        public int Door { get; set; }
        public int Seat { get; set; }
        public string Petrol { get; set; }
        public string Automatic { get; set; }
        public int SuitCase { get; set; }
    }
    public class WriteWhyDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
    public class WriteAboutDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public int CarTypes { get; set; }
        public int RentalPoints { get; set; }
        public int Customers { get; set; }
        public int TotalCars { get; set; }
        public int Offers { get; set; }

    }
    public class WriteTestimonialsDto
    {
        public string Message { get; set; }
        public IFormFile Image { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
    }
    public class WriteContactDto
    { 
        public IFormFile Image { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string RentalPoints { get; set; }
        public string pages { get; set; }
        public string WorkingHours { get; set; }
    }
    public class WriteCtaDto
    {
        public IFormFile Image { get; set; }
    }
    public class WriteHomeDto
    {
        public string UserName { get; set; }
        public string NavBar { get; set; }
        public IFormFile Image { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }

    }
}

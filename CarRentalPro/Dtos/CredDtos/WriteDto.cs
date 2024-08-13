using CarRentalPro.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace CarRentalPro.Dtos.CredDtos
{
    public class WriteCarDetailsDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PricePerDay { get; set; }
        public float Stars { get; set; }
        public IFormFile Image { get; set; }
        public int Door { get; set; }
        public int Seat { get; set; }
        public string Petrol { get; set; }
        public string Transmission { get; set; }
        public int SuitCase { get; set; }
    }

        public class WriteWhyDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
    public class WriteAboutDto
    {
        public int Id { get; set; }
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
        public int Id { get; set; }
        public string Message { get; set; }
        public IFormFile Image { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
    }
    public class WriteContactDto
    {
        public int Id { get; set; }
        public IFormFile Image { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string RentalPoints { get; set; }
        public string pages { get; set; }
        public string WorkingHours { get; set; }
    }
    public class WriteCtaDto
    {
        public int Id { get; set; }
        public IFormFile Image { get; set; }
    }
    public class WriteHomeDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string NavBar { get; set; }
        public IFormFile Image { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }

    }
    public class WriteCarReservationDto
    {
      
            public int Id { get; set; }
            public int UserId { get; set; }
            public int CarDetailsId { get; set; }
            public DateTime PickUpDate { get; set; }
            public DateTime DropOffDate { get; set; }
            public string ReservationCode { get; set; }
            public bool IsConfirmed { get; set; }
        


    }
   
        public class WritePaymentDetailsDto
        {
            public int Id { get; set; }
            public int ReservationId { get; set; }
            public string CardNumber { get; set; }
            public string CardholderName { get; set; }
            public DateTime ExpiryDate { get; set; }
            public string CVV { get; set; }
            public IFormFile DriverLicense { get; set; }
            public bool IsPaid { get; set; }
        }
    public class WriteCarFilterDto
    {
        public int CityId { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime DropOffDate { get; set; }
    }


}

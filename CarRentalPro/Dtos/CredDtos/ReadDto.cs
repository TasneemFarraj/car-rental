using CarRentalPro.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace CarRentalPro.Dtos.CredDtos
{
    public class ReadCarDetailsDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PricePerDay { get; set; }
        public float Stars { get; set; }
        public string Image { get; set; }
        public int Door { get; set; }
        public int Seat { get; set; }
        public string Petrol { get; set; }
        public string Transmission { get; set; }

        public int SuitCase { get; set; }
         public int CityId { get; set; }

    }
    public class ReadWhyDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
    public class ReadAboutDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int CarTypes { get; set; }
        public int RentalPoints { get; set; }
        public int Customers { get; set; }
        public int TotalCars { get; set; }
        public int Offers { get; set; }

    }
    public class ReadTestimonialsDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
    }
    public class ReadContactDto
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string RentalPoints { get; set; }
        public string pages { get; set; }
        public string WorkingHours { get; set; }

    }
    public class ReadCtaDto
    {
        public int Id { get; set; }
        public string Image { get; set; }

    }
    public class ReadHomeDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string NavBar { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }

    }


    public class ReadCarReservationDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CarDetailsId { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime DropOffDate { get; set; }
        public bool IsConfirmed { get; set; }

    }

    public class ReadPaymentDetailsDto
    {
        public int Id { get; set; }
        public int CarReservationId { get; set; }
        public string CardNumber { get; set; }
        public string CardholderName { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string CVV { get; set; }
        public string DriverLicense { get; set; }
        public bool IsPaid { get; set; }

    }
   

        public class ReadCarFilterDto
        {
            public int CityId { get; set; }
            public DateTime PickUpDate { get; set; }
            public DateTime DropOffDate { get; set; }
        }
    }







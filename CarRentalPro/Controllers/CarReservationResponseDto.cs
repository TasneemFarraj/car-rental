
namespace CarRentalPro.Controllers
{
    internal class CarReservationResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CarDetailsId { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime DropOffDate { get; set; }
        public string ReservationCode { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
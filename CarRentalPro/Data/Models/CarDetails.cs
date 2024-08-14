using CarRentalPro.Data.Models;

public class CarDetails
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
    public bool IsAvailable { get; set; }
    public int CityId { get; set; }
    public City City { get; set; }  

    public ICollection<CarReservation> CarReservations { get; set; }
}

using System.ComponentModel.DataAnnotations;

public class CarFilter
{
    [Key]
    public int Id { get; set; }
    public CityEnum? City { get; set; }

    public DateTime? PickUpDate { get; set; }
    public DateTime? DropOffDate { get; set; }
}

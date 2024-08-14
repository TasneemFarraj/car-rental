using System.ComponentModel.DataAnnotations;

public class CarFilter
{
    [Key]
    public int Id { get; set; }
    public int CityId { get; set; }
    public City City { get; set; }    
    public DateTime PickUpDate { get; set; }
    public DateTime DropOffDate { get; set; }
}

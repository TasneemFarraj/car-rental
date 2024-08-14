using System.ComponentModel.DataAnnotations;

public class City
{
    [Key]
    public int CityId { get; set; }
    public CityEnum CityName { get; set; }
    public ICollection<CarDetails> Cars { get; set; } 
}

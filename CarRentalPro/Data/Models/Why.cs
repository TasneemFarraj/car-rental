using System.ComponentModel.DataAnnotations;

namespace CarRentalPro.Data.Models
{
    public class Why
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        /*  //For Feature 
          public string Image1 { get; set; }
          public string? item1 { get; set; }

          public string Image { get; set; }
          public string? item2 { get; set; }

          public string Image3 { get; set; }
          public string? item3 { get; set; }

          */
    }
}

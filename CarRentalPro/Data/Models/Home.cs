using System.ComponentModel.DataAnnotations;

namespace CarRentalPro.Data.Models
{
    public class Home
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string NavBar { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
      
    }
}

namespace CarRentalPro.Data.Models
{
    public class PasswordReset
    {
            public int Id { get; set; }
            public int UserId { get; set; }
            public string ResetPin { get; set; }
            public DateTime ResetPinExpiration { get; set; }
 
    }
}

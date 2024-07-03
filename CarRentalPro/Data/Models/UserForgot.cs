namespace CarRentalPro.Data.Models
{
    public class UserForgot
    {
            public int Id { get; set; }
            public string Email { get; set; }
            public string PasswordHash { get; set; }
            public string ResetPin { get; set; }
            public DateTime? ResetPinExpiration { get; set; }
       
        }

    }

using CarRentalPro.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace CarRentalPro.Dtos.UserDtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public UserTypeEnum UserType { get; set; }
        public string Token { get; set; }
    }

    public class WriteUserDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "UserName is required")]
        [StringLength(20, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z0-9._]+$", ErrorMessage = "Usernames can only contain letters, numbers, underscores, and periods (dots).")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "FirstName is required")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@(gmail\.com|outlook\.com)$", ErrorMessage = "Email must be a valid Gmail or Outlook address.")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(059|056)\d{7}$", ErrorMessage = "Phone number must start with 059 or 056 and be 10 digits in total.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{6,}$",
        ErrorMessage = "Password must contain at least one lowercase character, one uppercase character, one digit, and one special character.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string PasswordConfirm { get; set; }

        [Required]
        public UserTypeEnum UserType { get; set; }    
}

    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

}
public class UpdateUserDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "UserName is required")]
    [StringLength(20, MinimumLength = 3)]
    [RegularExpression(@"^[a-zA-Z0-9._]+$", ErrorMessage = "Usernames can only contain letters, numbers, underscores, and periods (dots).")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "FirstName is required")]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "LastName is required")]
    [StringLength(50)]
    public string LastName { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }

    [Required]
    [RegularExpression(@"^(059|056)\d{7}$", ErrorMessage = "Phone number must start with 059 or 056 and be 10 digits in total.")]
    public string PhoneNumber { get; set; }

    public UserTypeEnum UserType { get; set; }
}




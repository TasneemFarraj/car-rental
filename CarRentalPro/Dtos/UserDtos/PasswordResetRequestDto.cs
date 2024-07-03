using System.ComponentModel.DataAnnotations;

namespace CarRentalPro.Dtos.UserDtos
{
    public class PasswordResetRequestDto
    {
   
            [Required]
            [EmailAddress(ErrorMessage = "Invalid Email Address")]
            public string Email { get; set; }
        }

        public class VerifyPinDto
        {
            [Required]
            public string Email { get; set; }
            public string Pin { get; set; }
        }

        public class ResetPasswordDto
        {
            [Required]
            public string Email { get; set; }
            public string Pin { get; set; }

            [Required(ErrorMessage = "Password is required")]
            [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
            [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{6,}$",
            ErrorMessage = "Password must contain at least one lowercase character, one uppercase character, one digit, and one special character.")]
            public string NewPassword { get; set; }

            [Required(ErrorMessage = "Confirm password is required")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }
    }


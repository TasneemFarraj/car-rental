using System.ComponentModel.DataAnnotations;

namespace CarRentalPro.Dtos
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

        [Required]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "PIN must be 4 digits.")]
        public string Pin { get; set; }
    }

    public class ResetPasswordDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "PIN must be 4 digits.")]
        public string Pin { get; set; }

        [Required(ErrorMessage = "New Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 8 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,}$", ErrorMessage = "Password must contain at least one lowercase character, one uppercase character, one digit, and one special character.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}

using CarRentalPro.Dtos.UserDtos;
using CarRentalPro.Repository.IRepo;
using CarRentalPro.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CarRentalPro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordResetController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly MailService _mailService;

        public PasswordResetController(IUserRepository userRepository, MailService mailService)
        {
            _userRepository = userRepository;
            _mailService = mailService;
        }

        [HttpPost("request-reset")]
        public async Task<IActionResult> RequestReset([FromForm] PasswordResetRequestDto request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

          
            var pin = new Random().Next(100000, 999999).ToString();
            user.ResetPin = pin;
            user.ResetPinExpiration = DateTime.UtcNow.AddMinutes(15);

            await _userRepository.UpdateUser(user);

            _mailService.SendEmail(user.Email, "Password Reset PIN", $"Your password reset PIN is: {pin}");

            return Ok(new { Message = "Password reset PIN sent to your email" });
        }

        [HttpPost("verify-pin")]
        public async Task<IActionResult> VerifyPin([FromForm] VerifyPinDto verifyPinDto)
        {
            var user = await _userRepository.GetByEmailAsync(verifyPinDto.Email);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            if (user.ResetPin != verifyPinDto.Pin || user.ResetPinExpiration < DateTime.UtcNow)
            {
                return BadRequest(new { Message = "Invalid or expired PIN" });
            }

            return Ok(new { Message = "PIN verified successfully" });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordDto resetPasswordDto)
        {
            var user = await _userRepository.GetByEmailAsync(resetPasswordDto.Email);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            if (user.ResetPin != resetPasswordDto.Pin || user.ResetPinExpiration < DateTime.UtcNow)
            {
                return BadRequest(new { Message = "Invalid or expired PIN" });
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(resetPasswordDto.NewPassword);
            user.ResetPin = null;
            user.ResetPinExpiration = null;

            await _userRepository.UpdateUser(user);

            return Ok(new { Message = "Password reset successfully" });
        }
    }
}

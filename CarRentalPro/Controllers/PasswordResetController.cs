using CarRentalPro.Data.Models;
using CarRentalPro.Dtos;
using CarRentalPro.Repository.IRepo;
using CarRentalPro.Services;
using CarRentalPro.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CarRentalPro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordResetController : ControllerBase
    {
        private readonly IUOW _uow;
        private readonly MailService _mailService;

        public PasswordResetController(IUOW uow, MailService mailService)
        {
            _uow = uow;
            _mailService = mailService;
        }

        [HttpPost("request-reset")]
        public async Task<IActionResult> RequestReset([FromForm] PasswordResetRequestDto request)
        {
            var user = await _uow.User.GetByEmailAsync(request.Email);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            var pin = new Random().Next(1000, 9999).ToString();
            var passwordReset = new PasswordReset
            {
                UserId = user.Id,
                ResetPin = pin,
                ResetPinExpiration = DateTime.UtcNow.AddMinutes(15)
            };

            await _uow.PasswordReset.AddPasswordReset(passwordReset);

            _mailService.SendEmail(user.Email, "Password Reset PIN", $"Your password reset PIN is: {pin}");

            return Ok(new { Message = "Password reset PIN sent to your email" });
        }

        [HttpPost("verify-pin")]
        public async Task<IActionResult> VerifyPin([FromForm] VerifyPinDto verifyPinDto)
        {
            var user = await _uow.User.GetByEmailAsync(verifyPinDto.Email);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            var passwordReset = await _uow.PasswordReset.GetByUserIdAsync(user.Id);
            if (passwordReset == null || passwordReset.ResetPin != verifyPinDto.Pin || passwordReset.ResetPinExpiration < DateTime.UtcNow)
            {
                return BadRequest(new { Message = "Invalid or expired PIN" });
            }

            return Ok(new { Message = "PIN verified successfully" });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordDto resetPasswordDto)
        {
            var user = await _uow.User.GetByEmailAsync(resetPasswordDto.Email);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            var passwordReset = await _uow.PasswordReset.GetByUserIdAsync(user.Id);
            if (passwordReset == null || passwordReset.ResetPin != resetPasswordDto.Pin || passwordReset.ResetPinExpiration < DateTime.UtcNow)
            {
                return BadRequest(new { Message = "Invalid or expired PIN" });
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(resetPasswordDto.NewPassword);
            await _uow.User.UpdateUser(user);

            await _uow.PasswordReset.DeletePasswordReset(passwordReset.Id);

            return Ok(new { Message = "Password reset successfully" });
        }
    }
}

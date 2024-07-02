using CarRentalPro.Authorization;
using CarRentalPro.AutoMapper;
using CarRentalPro.Data.Models;
using CarRentalPro.Dtos.CredDtos;
using CarRentalPro.Dtos.UserDtos;
using CarRentalPro.Repository.IRepo;
using CarRentalPro.Response;
using CarRentalPro.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CarRentalPro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUOW _uOW;
        private readonly IEmailService _emailService;
        private readonly IAuthentication<UserDto> _jwt;

        public UserController(IUOW uOW, IEmailService emailService, IAuthentication<UserDto> jwt)
        {
            _uOW = uOW;
            _emailService = emailService;
            _jwt = jwt;
        }

        [HttpPost(nameof(AddUser))]
        public async Task<ResponseStandardJsonApi> AddUser([FromForm] WriteUserDto dto)
        {
            var apiResponse = new ResponseStandardJsonApi();

            try
            {
                var mapper = AutoMapperConfig.CreateMapper();
                var data = mapper.Map<User>(dto);
                if (data != null)
                {
                    await _uOW.User.AddUser(data);
                    apiResponse.Message = "User created successfully";
                    apiResponse.Code = Ok().StatusCode;
                    apiResponse.Success = true;
                    apiResponse.Result = data;
                }
                else
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "No Data";
                    apiResponse.Code = NotFound().StatusCode;
                    apiResponse.Result = new NullColumns[] { };
                }
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
                apiResponse.Code = BadRequest().StatusCode;
                apiResponse.Result = new NullColumns[] { };
            }

            return apiResponse;
        }

        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var apiResponse = new ResponseStandardJsonApi();
            try
            {
                var mapper = AutoMapperConfig.CreateMapper();
                var user = mapper.Map<User>(model);

                var authenticatedUser = await _uOW.User.LoginUser(model.UserName, model.Password);
                if (authenticatedUser != null)
                {
                    var userData = mapper.Map<UserDto>(authenticatedUser);
                    userData.Token = _jwt.GetJsonWebToken(userData);

                    apiResponse.Message = "Login successful";
                    apiResponse.Code = StatusCodes.Status200OK;
                    apiResponse.Success = true;
                    apiResponse.Result = userData;
                    return Ok(apiResponse);
                }
                else
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "Invalid email or password";
                    apiResponse.Code = StatusCodes.Status404NotFound;
                    apiResponse.Result = new NullColumns[] { };
                    return NotFound(apiResponse);
                }
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
                apiResponse.Code = StatusCodes.Status400BadRequest;
                apiResponse.Result = new NullColumns[] { };
                return BadRequest(apiResponse);
            }
        }

        [HttpPost(nameof(ForgotPassword))]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto model)
        {
            var apiResponse = new ResponseStandardJsonApi();
            try
            {
                var user = await _uOW.User.GetUserByEmailAsync(model.Email);

                if (user == null)
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "User not found";
                    apiResponse.Code = StatusCodes.Status404NotFound;
                    return NotFound(apiResponse);
                }

                string resetPin = GenerateResetPin();

                user.ResetPin = resetPin;
                user.ResetPinExpiration = DateTime.UtcNow.AddMinutes(3);

                await _uOW.User.UpdateUser(user);

                string subject = "Password Reset Instructions";
                string message = $"Your password reset PIN is: {resetPin}";

                await _emailService.SendEmailAsync(user.Email, subject, message);

                apiResponse.Success = true;
                apiResponse.Message = "Password reset PIN sent to your email";
                apiResponse.Code = StatusCodes.Status200OK;
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
                apiResponse.Code = StatusCodes.Status500InternalServerError;
                return StatusCode(apiResponse.Code, apiResponse);
            }
        }

        private string GenerateResetPin()
        {
            return new Random().Next(1000, 9999).ToString();
        }
    }
}

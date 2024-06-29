using CarRentalPro.Authorization;
using CarRentalPro.AutoMapper;
using CarRentalPro.Data;
using CarRentalPro.Data.Enum;
using CarRentalPro.Data.Models;
using CarRentalPro.Dtos.CredDtos;
using CarRentalPro.Dtos.UserDtos;
using CarRentalPro.Repository.IRepo;
using CarRentalPro.Response;
using CarRentalPro.UnitOfWork;
using CarRentalPro.Upload;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalPro.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUOW _uOW;
        private readonly IEmailService _emailService;
        private readonly ILogger<UserController> _logger;
        public IAuthentication<UserDto> Jwt { get; }

        public UserController(IUOW uOW, IEmailService emailService, IAuthentication<UserDto> jwt, ILogger<UserController> logger)
        {
            _uOW = uOW;
            _emailService = emailService;
            Jwt = jwt;
            _logger = logger;
        }




            [HttpPost("AddUser")]
            public async Task<IActionResult> AddUser([FromForm] WriteUserDto dto)
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                        );

                    var errorResponse = new
                    {
                        Message = "Validation failed",
                        Errors = errors
                    };

                    return BadRequest(errorResponse);
                }

                var apiResponse = new ResponseStandardJsonApi();

                try
                {
                    var mapper = AutoMapperConfig.CreateMapper();
                    var data = mapper.Map<User>(dto);

                    if (data != null)
                    {
                        await _uOW.User.AddUser(data);
                        apiResponse.Message = "User added successfully";
                        apiResponse.Code = Ok().StatusCode;
                        apiResponse.Success = true;
                        apiResponse.Result = data;
                    }
                    else
                    {
                        apiResponse.Success = false;
                        apiResponse.Message = "Mapping failed, no data";
                        apiResponse.Code = NotFound().StatusCode;
                        apiResponse.Result = new NullColumns[] { };
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while adding the user.");
                    apiResponse.Success = false;
                    apiResponse.Message = ex.InnerException?.Message ?? ex.Message;
                    apiResponse.Code = BadRequest().StatusCode;
                    apiResponse.Result = new NullColumns[] { };
                }

                return Ok(apiResponse);
            }

            // Other methods...
        
    


    [HttpPost(nameof(Login))]
        public async Task<ResponseStandardJsonApi> Login(LoginDto model)
        {
            var apiResponse = new ResponseStandardJsonApi();
            try
            {
                var Mapper = AutoMapperConfig.CreateMapper();
                var Data = Mapper.Map<User>(model);

                var Obj = await _uOW.User.LoginUser(Data);
                var NewData = Mapper.Map<UserDto>(Obj);
                if (Obj != null)
                {
                    var _userModel = Mapper.Map<UserDto>(Obj);
                    _userModel.Token = Jwt.GetJsonWebToken(_userModel);
                    NewData.Token = _userModel.Token;
                    apiResponse.Message = "Show Row";
                    apiResponse.Code = Ok().StatusCode;
                    apiResponse.Success = true;
                    apiResponse.Result = NewData;
                }
                else
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "Username or Password incorrect";
                    apiResponse.Code = NotFound().StatusCode;
                    apiResponse.Result = new NullColumns[] { };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging in.");
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
                apiResponse.Code = BadRequest().StatusCode;
                apiResponse.Result = new NullColumns[] { };
            }
            return apiResponse;
        }

      [HttpPost("ForgetPassword")]
        public async Task<ResponseStandardJsonApi> ForgetPassword(ForgetPasswordDto model)
        {
            var apiResponse = new ResponseStandardJsonApi();

            try
            {
                // Assuming the logic to generate and store the reset PIN

                // Send the reset PIN to the user's email
                await _emailService.SendEmailAsync(model.Email, "Reset Password", $"Your reset PIN is {model.PinNumber}");

                apiResponse.Message = "Reset PIN sent to your email";
                apiResponse.Code = Ok().StatusCode;
                apiResponse.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the password reset request.");
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
                apiResponse.Code = BadRequest().StatusCode;
            }

            return apiResponse;
        }
    }
}

using CarRentalPro.Authorization;
using CarRentalPro.AutoMapper;
using CarRentalPro.Data.Models;
using CarRentalPro.Dtos.CredDtos;
using CarRentalPro.Dtos.UserDtos;
using CarRentalPro.Repository.IRepo;
using CarRentalPro.Response;
using CarRentalPro.Services;
using CarRentalPro.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CarRentalPro.Controllers
{
    
    public class UserController : ControllerBase
    {
        private readonly IUOW _uOW;
        public IAuthentication<UserDto> Jwt { get; }
        public UserController(IUOW uOW, IAuthentication<UserDto> jwt)
        {
            _uOW = uOW;
            Jwt = jwt;
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
            catch (DbUpdateException ex)
            {
                var innerExceptionMessage = ex.InnerException?.Message ?? ex.Message;
                apiResponse.Success = false;
                apiResponse.Message = innerExceptionMessage;
                apiResponse.Code = BadRequest().StatusCode;
                apiResponse.Result = new NullColumns[] { };
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
                apiResponse.Code = BadRequest().StatusCode;
                apiResponse.Result = new NullColumns[] { };
            }

            return Ok(apiResponse);
        }


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
                    apiResponse.Message = "UserName or Password incorrect ";
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

    }

   
}



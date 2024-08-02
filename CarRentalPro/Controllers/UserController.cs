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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalPro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUOW _uOW;
        private readonly IAuthentication<UserDto> _jwt;
        private readonly AccountManagementMailService _accountManagementMailService;

        public UserController(IUOW uOW, IAuthentication<UserDto> jwt, AccountManagementMailService accountManagementMailService)
        {
            _uOW = uOW;
            _jwt = jwt;
            _accountManagementMailService = accountManagementMailService;
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

     
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromForm] UpdateUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                return BadRequest(new { Message = "Validation failed", Errors = errors });
            }

            try
            {
                var mapper = AutoMapperConfig.CreateMapper();
                var userToUpdate = mapper.Map<User>(dto);

                var existingUser = await _uOW.User.GetByIdAsync(userToUpdate.Id);
                var changes = new List<string>();

                if (existingUser != null)
                {
                    if (existingUser.UserName != userToUpdate.UserName)
                        changes.Add($"Username: {existingUser.UserName} -> {userToUpdate.UserName}");

                    if (existingUser.FirstName != userToUpdate.FirstName)
                        changes.Add($"First Name: {existingUser.FirstName} -> {userToUpdate.FirstName}");

                    if (existingUser.LastName != userToUpdate.LastName)
                        changes.Add($"Last Name: {existingUser.LastName} -> {userToUpdate.LastName}");

                    if (existingUser.Email != userToUpdate.Email)
                        changes.Add($"Email: {existingUser.Email} -> {userToUpdate.Email}");

                    if (existingUser.PhoneNumber != userToUpdate.PhoneNumber)
                        changes.Add($"Phone Number: {existingUser.PhoneNumber} -> {userToUpdate.PhoneNumber}");

                    if (existingUser.UserType != userToUpdate.UserType)
                        changes.Add($"User Type: {existingUser.UserType} -> {userToUpdate.UserType}");
                }

                await _uOW.User.UpdateUser(userToUpdate);

                _accountManagementMailService.SendAccountModificationEmail(
                    userToUpdate.Email,
                    $"{userToUpdate.FirstName} {userToUpdate.LastName}",
                    changes
                );

                return Ok(new { Message = "User updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred", Error = ex.Message });
            }
        }

        [HttpPost(nameof(Login))]
        public async Task<ResponseStandardJsonApi> Login(LoginDto model)
        {
            var apiResponse = new ResponseStandardJsonApi();
            try
            {
                var mapper = AutoMapperConfig.CreateMapper();
                var data = mapper.Map<User>(model);

                var user = await _uOW.User.LoginUser(data);
                var userDto = mapper.Map<UserDto>(user);
                if (user != null)
                {
                    userDto.Token = _jwt.GetJsonWebToken(userDto);
                    apiResponse.Message = "Login successful";
                    apiResponse.Code = Ok().StatusCode;
                    apiResponse.Success = true;
                    apiResponse.Result = userDto;
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
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
                apiResponse.Code = BadRequest().StatusCode;
                apiResponse.Result = new NullColumns[] { };
            }
            return apiResponse;
        }
    }
}

using CarRentalPro.Data.Models;
using CarRentalPro.Dtos.CredDtos;
using CarRentalPro.Response;
using CarRentalPro.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CarRentalPro.Dtos;
using CarRentalPro.AutoMapper;

namespace CarRentalPro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarReservationController : ControllerBase
    {
        private readonly IUOW _uow;

        public CarReservationController(IUOW uow)
        {
            _uow = uow;
        }

        [HttpGet(nameof(GetAllReservations))]
        public async Task<ResponseStandardJsonApi> GetAllReservations()
        {
            var response = new ResponseStandardJsonApi();

            var result = await _uow.CarReservation.GetAllReservations();
            if (result.Count > 0)
            {
                var reservationsDto = result.Select(r => new CarReservationResponseDto
                {
                    Id = r.Id,
                    UserId = r.UserId,
                    CarDetailsId = r.CarDetailsId,
                    PickUpDate = r.PickUpDate,
                    DropOffDate = r.DropOffDate,
                    ReservationCode = r.ReservationCode,
                    IsConfirmed = r.IsConfirmed
                }).ToList();

                response.Success = true;
                response.Message = "Data retrieved successfully";
                response.Code = Ok().StatusCode;
                response.Result = reservationsDto;
            }
            else
            {
                response.Success = false;
                response.Message = "No data found";
                response.Code = NotFound().StatusCode;
                response.Result = Array.Empty<NullColumns>();
            }

            return response;
        }


        [HttpGet(nameof(GetReservationById))]
        public async Task<ResponseStandardJsonApi> GetReservationById(int Id)
        {
            var apiResponse = new ResponseStandardJsonApi();

            try
            {
                var mapper = AutoMapperConfig.CreateMapper();
                var result = await _uow.CarReservation.GetAsync(Id);
                var data = mapper.Map<ReadCarReservationDto>(result);
                if (data is not null)
                {
                    apiResponse.Message = "Show Rows";
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

        [HttpPost(nameof(AddCarReservation))]
        public async Task<ResponseStandardJsonApi> AddCarReservation([FromForm] WriteCarReservationDto carReservationDto)
        {
            var response = new ResponseStandardJsonApi();

            if (carReservationDto.CarDetailsId <= 0)
            {
                response.Success = false;
                response.Message = "Invalid CarDetailsId. It must be greater than zero.";
                response.Code = BadRequest().StatusCode;
                response.Result = Array.Empty<NullColumns>();
                return response;
            }

            if (carReservationDto.UserId <= 0)
            {
                response.Success = false;
                response.Message = "Invalid UserId. It must be greater than zero.";
                response.Code = BadRequest().StatusCode;
                response.Result = Array.Empty<NullColumns>();
                return response;
            }


            var carReservation = new CarReservation
            {
                UserId = carReservationDto.UserId,
                CarDetailsId = carReservationDto.CarDetailsId,
                PickUpDate = carReservationDto.PickUpDate,
                DropOffDate = carReservationDto.DropOffDate
            };

            await _uow.CarReservation.AddCarReservation(carReservation);
            response.Success = true;
            response.Message = "Car reservation added successfully";
            response.Code = (int)CreatedAtAction(nameof(GetReservationById), new { id = carReservation.Id }).StatusCode;
            response.Result = carReservation; 

            return response;
        }

        [HttpPut(nameof(ConfirmReservation))]
        public async Task<ResponseStandardJsonApi> ConfirmReservation(int id)
        {
            var response = new ResponseStandardJsonApi();

            try
            {
                await _uow.CarReservation.ConfirmReservation(id);
                response.Success = true;
                response.Message = "Reservation confirmed successfully";
                response.Code = Ok().StatusCode;
                response.Result = Array.Empty<NullColumns>();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Code = BadRequest().StatusCode;
                response.Result = Array.Empty<NullColumns>();
            }

            return response;
        }

        [HttpDelete(nameof(DeleteReservation))]
        public async Task<ResponseStandardJsonApi> DeleteReservation(int id)
        {
            var response = new ResponseStandardJsonApi();

            await _uow.CarReservation.DeleteCarReservation(id);
            response.Success = true;
            response.Message = "Reservation deleted successfully";
            response.Code = Ok().StatusCode;
            response.Result = Array.Empty<NullColumns>();

            return response;
        }
    }
}

using CarRentalPro.AutoMapper;
using CarRentalPro.Data.Models;
using CarRentalPro.Dtos;
using CarRentalPro.Dtos.CredDtos;
using CarRentalPro.Repository.IRepo;
using CarRentalPro.Response;
using CarRentalPro.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        [HttpPost(nameof(AddReservation))]
        public async Task<ResponseStandardJsonApi> AddReservation([FromForm] WriteCarReservationDto dto)
        {
            var response = new ResponseStandardJsonApi();

            try
            {
                var mapper = AutoMapperConfig.CreateMapper();
                var reservation = mapper.Map<CarReservation>(dto);

                await _uow.CarReservation.AddCarReservation(reservation);

                response.Message = "Reservation added successfully";
                response.Code = Ok().StatusCode;
                response.Success = true;
                response.Result = reservation;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Code = BadRequest().StatusCode;
                response.Success = false;
            }

            return response;
        }

        [HttpGet(nameof(GetReservationById))]
        public async Task<ResponseStandardJsonApi> GetReservationById(int Id)
        {
            var response = new ResponseStandardJsonApi();

            try
            {
                var reservation = await _uow.CarReservation.GetAsync(Id);
                var mapper = AutoMapperConfig.CreateMapper();
                var dto = mapper.Map<ReadCarReservationDto>(reservation);

                response.Message = "Reservation details retrieved";
                response.Code = Ok().StatusCode;
                response.Success = true;
                response.Result = dto;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Code = BadRequest().StatusCode;
                response.Success = false;
            }

            return response;
        }
    }
}

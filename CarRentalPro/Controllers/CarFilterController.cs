using CarRentalPro.AutoMapper;
using CarRentalPro.Dtos.CredDtos;
using CarRentalPro.Response;
using CarRentalPro.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRentalPro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarFilterController : ControllerBase
    {
        private readonly IUOW _uOW;

        public CarFilterController(IUOW uOW)
        {
            _uOW = uOW;
        }

        [HttpGet("FilterCars")]
        public async Task<ResponseStandardJsonApi> FilterCars([FromQuery] CarFilter carFilterDto)
        {
            var apiResponse = new ResponseStandardJsonApi();

            try
            {
                var mapper = AutoMapperConfig.CreateMapper();
                var cars = await _uOW.CarFilter.GetFilteredCarsAsync(carFilterDto);
                var data = mapper.Map<List<ReadCarDetailsDto>>(cars);

                apiResponse.Message = "Filtered Cars";
                apiResponse.Code = Ok().StatusCode;
                apiResponse.Success = true;
                apiResponse.Result = data;
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
                apiResponse.Code = BadRequest().StatusCode;
                apiResponse.Result = new List<ReadCarDetailsDto>();
            }

            return apiResponse;
        }
    }
}

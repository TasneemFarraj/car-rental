using CarRentalPro.AutoMapper;
using CarRentalPro.Data.Enum;
using CarRentalPro.Data.Models;
using CarRentalPro.Dtos.CredDtos;
using CarRentalPro.Response;
using CarRentalPro.UnitOfWork;
using CarRentalPro.Upload;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalPro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarDetailsController : ControllerBase
    {
        private readonly AppDBContext _db;
        private readonly IUOW _uOW;
        public CarDetailsController(AppDBContext db, IUOW uOW)
        {
            _db = db;
            _uOW = uOW;
        }


        [HttpGet(nameof(GetCarDetails))]
        public async Task<ResponseStandardJsonApi> GetCarDetails()
        {
            var apiResponse = new ResponseStandardJsonApi();

            try
            {
                var mapper = AutoMapperConfig.CreateMapper();
                var result = await _uOW.CarDetails.GetListAsync();
                var data = mapper.Map<List<ReadCarDetailsDto>>(result);


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


        [HttpGet(nameof(GetCarDetailsById))]
        public async Task<ResponseStandardJsonApi> GetCarDetailsById(int Id)
        {
            var apiResponse = new ResponseStandardJsonApi();

            try
            {
                var mapper = AutoMapperConfig.CreateMapper();
                var result = await _uOW.CarDetails.GetAsync(Id);
                var data = mapper.Map<ReadCarDetailsDto>(result);
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

        [HttpGet("Filter")]
        public async Task<ResponseStandardJsonApi> GetFilteredCars([FromQuery] ReadCarFilterDto filterDto)
        {
            var apiResponse = new ResponseStandardJsonApi();

            try
            {
                var filter = new CarFilter
                {
                    CityId = filterDto.CityId,
                    PickUpDate = filterDto.PickUpDate,
                    DropOffDate = filterDto.DropOffDate
                };

                var result = await _uOW.CarDetails.GetListAsync(filter);
                var mapper = AutoMapperConfig.CreateMapper();
                var data = mapper.Map<List<ReadCarDetailsDto>>(result);

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


        [HttpPost(nameof(AddCarDetails))]
        public async Task<ResponseStandardJsonApi> AddCarDetails([FromForm] WriteCarDetailsDto dto)
        {
            var apiResponse = new ResponseStandardJsonApi();

            try
            {
                var mapper = AutoMapperConfig.CreateMapper();
                var data = mapper.Map<CarDetails>(dto);
                if (dto.Image is not null)
                    data.Image = ImageUploader.Upload(dto.Image, FileTypeEnum.Image, "https://localhost:5000/");

                if (data is not null)
                {
                    await _uOW.CarDetails.AddCarDetails(data);
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



        [HttpPut(nameof(UpdateCarDetails))]
        public async Task<ResponseStandardJsonApi> UpdateCarDetails([FromForm] WriteCarDetailsDto dto)
        {
            var apiResponse = new ResponseStandardJsonApi();

            try
            {
                var mapper = AutoMapperConfig.CreateMapper();
                var data = mapper.Map<CarDetails>(dto);
                if (dto.Image is not null)
                    data.Image = ImageUploader.Upload(dto.Image, FileTypeEnum.Image, "https://localhost:5000/");
                await _uOW.CarDetails.UpdateCarDetails(data);

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


        [HttpDelete(nameof(RemoveCarDetails))]
        public async Task<ResponseStandardJsonApi> RemoveCarDetails(int Id)
        {
            var apiResponse = new ResponseStandardJsonApi();

            try
            {
                await _uOW.CarDetails.DeleteCarDetails(Id);
                if (Id != 0)
                {
                    apiResponse.Message = "Show Rows";
                    apiResponse.Code = Ok().StatusCode;
                    apiResponse.Success = true;
                    apiResponse.Result = Ok("Delete");
                }
                else
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "No Data";
                    apiResponse.Code = NotFound().StatusCode;
                    apiResponse.Result = null;
                }
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
                apiResponse.Code = BadRequest().StatusCode;
                apiResponse.Result = null;
            }

            return apiResponse;

        }
    }

}
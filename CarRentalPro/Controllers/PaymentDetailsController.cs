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
    public class PaymentDetailsController : ControllerBase
    {
        private readonly AppDBContext _db;
        private readonly IUOW _uOW;
        public PaymentDetailsController(AppDBContext db, IUOW uOW)
        {
            _db = db;
            _uOW = uOW;
        }


        [HttpGet(nameof(GetPaymentDetailsById))]
        public async Task<ResponseStandardJsonApi> GetPaymentDetailsById(int Id)
        {
            var apiResponse = new ResponseStandardJsonApi();

            try
            {
                var mapper = AutoMapperConfig.CreateMapper();
                var result = await _uOW.PaymentDetails.GetAsync(Id);
                var data = mapper.Map<ReadPaymentDetailsDto>(result);
                if (data is not null)
                {
                    apiResponse.Message = "Payment details retrieved";
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


        [HttpPost(nameof(AddPaymentDetails))]
        public async Task<ResponseStandardJsonApi> AddPaymentDetails([FromForm] WritePaymentDetailsDto dto)
        {
            var apiResponse = new ResponseStandardJsonApi();

            try
            {
                var mapper = AutoMapperConfig.CreateMapper();
                var data = mapper.Map<PaymentDetails>(dto);
                if (dto.DriverLicense is not null)
                    data.DriverLicense = ImageUploader.Upload(dto.DriverLicense, FileTypeEnum.Image, "https://localhost:5000/");

                if (data is not null)
                {
                    await _uOW.PaymentDetails.AddPaymentDetails(data);
                    apiResponse.Message = "Payment details added successfully";
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



    }
}
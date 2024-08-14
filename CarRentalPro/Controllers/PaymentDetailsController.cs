using CarRentalPro.AutoMapper;
using CarRentalPro.Data.Enum;
using CarRentalPro.Data.Models;
using CarRentalPro.Dtos.CredDtos;
using CarRentalPro.Response;
using CarRentalPro.UnitOfWork;
using CarRentalPro.Upload;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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

                ReadPaymentDetailsDto data = null;
                try
                {
                    data = mapper.Map<ReadPaymentDetailsDto>(result);
                }
                catch (Exception ex)
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "Error mapping PaymentDetails to ReadPaymentDetailsDto: " + ex.Message;
                    apiResponse.Code = BadRequest().StatusCode;
                    apiResponse.Result = new NullColumns[] { };
                    return apiResponse;
                }

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

        [HttpPost(nameof(ConfirmPayment))]
        public async Task<ResponseStandardJsonApi> ConfirmPayment([FromForm] WritePaymentDetailsDto paymentDetailsDto)
        {
            var response = new ResponseStandardJsonApi();

            // Validate input
            if (string.IsNullOrWhiteSpace(paymentDetailsDto.CardNumber) ||
                string.IsNullOrWhiteSpace(paymentDetailsDto.CardholderName) ||
                paymentDetailsDto.ExpiryDate == default ||
                string.IsNullOrWhiteSpace(paymentDetailsDto.CVV) ||
                paymentDetailsDto.CarReservationId <= 0)
            {
                response.Success = false;
                response.Message = "Invalid payment details. Please fill in all required fields.";
                response.Code = BadRequest().StatusCode;
                response.Result = Array.Empty<NullColumns>();
                return response;
            }

            var paymentDetails = new PaymentDetails
            {
                CarReservationId = paymentDetailsDto.CarReservationId,
                CardNumber = paymentDetailsDto.CardNumber,
                CardholderName = paymentDetailsDto.CardholderName,
                ExpiryDate = paymentDetailsDto.ExpiryDate,
                CVV = paymentDetailsDto.CVV,
                IsPaid = true
            };

            // Handle DriverLicense upload
            if (paymentDetailsDto.DriverLicense != null)
            {
                paymentDetails.DriverLicense = ImageUploader.Upload(paymentDetailsDto.DriverLicense, FileTypeEnum.Image, "https://localhost:5000/");
            }

            await _uOW.PaymentDetails.AddPaymentDetails(paymentDetails);

            response.Success = true;
            response.Message = "Payment details added successfully";
            response.Code = Ok().StatusCode;
            response.Result = paymentDetails;

            return response;
        }

    }
}

using CarRentalPro.AutoMapper;
using CarRentalPro.Data.Enum;
using CarRentalPro.Data.Models;
using CarRentalPro.Dtos.CredDtos;
using CarRentalPro.Response;
using CarRentalPro.Services;
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
        private readonly IConfiguration _configuration;

        public PaymentDetailsController(AppDBContext db, IUOW uOW, IConfiguration configuration)
        {
            _db = db;
            _uOW = uOW;
            _configuration = configuration;
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

            if (string.IsNullOrWhiteSpace(paymentDetailsDto.CardNumber) ||
                string.IsNullOrWhiteSpace(paymentDetailsDto.CardholderName) ||
                paymentDetailsDto.ExpiryDate == default ||
                string.IsNullOrWhiteSpace(paymentDetailsDto.CVV) ||
                paymentDetailsDto.CarReservationId <= 0 ||
                string.IsNullOrWhiteSpace(paymentDetailsDto.Email))
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
                Email = paymentDetailsDto.Email
            };

            await _uOW.PaymentDetails.AddPaymentDetails(paymentDetails);

            var verificationEmailService = new SendVerificationCodeEmailService(_configuration);
            var verificationCode = verificationEmailService.SendVerificationCode(paymentDetailsDto.Email);

            response.Success = true;
            response.Message = "Payment details added successfully, verification code sent to email.";
            response.Code = Ok().StatusCode;
            response.Result = paymentDetails;

            return response;
        }

        [HttpPost(nameof(VerifyPayment))]
        public async Task<ResponseStandardJsonApi> VerifyPayment([FromBody] VerifyPaymentDto verifyPaymentDto)
        {
            var response = new ResponseStandardJsonApi();

            if (string.IsNullOrWhiteSpace(verifyPaymentDto.VerificationCode) ||
                verifyPaymentDto.CarReservationId <= 0)
            {
                response.Success = false;
                response.Message = "Invalid input. Please provide a verification code and a valid reservation ID.";
                response.Code = BadRequest().StatusCode;
                response.Result = Array.Empty<NullColumns>();
                return response;
            }

            var paymentDetails = await _uOW.PaymentDetails.GetAsync(verifyPaymentDto.CarReservationId);

            if (paymentDetails == null)
            {
                response.Success = false;
                response.Message = "No payment details found for the provided reservation ID.";
                response.Code = NotFound().StatusCode;
                response.Result = Array.Empty<NullColumns>();
                return response;
            }

            var isVerificationCodeValid = CheckVerificationCode(paymentDetails.Email, verifyPaymentDto.VerificationCode);
            if (!isVerificationCodeValid)
            {
                response.Success = false;
                response.Message = "Invalid verification code.";
                response.Code = BadRequest().StatusCode;
                response.Result = Array.Empty<NullColumns>();
                return response;
            }

            paymentDetails.IsPaid = true;
            await _uOW.PaymentDetails.UpdatePaymentDetails(paymentDetails);

            response.Success = true;
            response.Message = "Payment verified successfully.";
            response.Code = Ok().StatusCode;
            response.Result = paymentDetails;

            return response;
        }

        [HttpPost(nameof(UploadDriverLicense))]
        public async Task<ResponseStandardJsonApi> UploadDriverLicense([FromForm] WriteUploadDriverLicenseDto dto)
        {
            var response = new ResponseStandardJsonApi();

            if (dto.CarReservationId <= 0 || dto.DriverLicense == null)
            {
                response.Success = false;
                response.Message = "Invalid input. Please provide a valid reservation ID and a driver license file.";
                response.Code = BadRequest().StatusCode;
                response.Result = Array.Empty<NullColumns>();
                return response;
            }

            var paymentDetails = await _uOW.PaymentDetails.GetAsync(dto.CarReservationId);
            if (paymentDetails == null)
            {
                response.Success = false;
                response.Message = "No payment details found for the provided reservation ID.";
                response.Code = NotFound().StatusCode;
                response.Result = Array.Empty<NullColumns>();
                return response;
            }

            paymentDetails.DriverLicense = ImageUploader.Upload(dto.DriverLicense, FileTypeEnum.Image, "https://localhost:5000/");
            await _uOW.PaymentDetails.UpdatePaymentDetails(paymentDetails);

            response.Success = true;
            response.Message = "Driver's license uploaded successfully.";
            response.Code = Ok().StatusCode;
            response.Result = paymentDetails;

            return response;
        }

        private bool CheckVerificationCode(string email, string verificationCode)
        {
           
            return true;
        }
    }
}

using AutoMapper;
using CarRentalPro.Dtos.UserDtos;
using CarRentalPro.Data.Models;
using CarRentalPro.Dtos.CredDtos;
using CarRentalPro.Dtos;
using CarRentalPro.Repository.IRepo;

namespace CarRentalPro.AutoMapper
{
    public class AutoMapperConfig
    {
        public static IMapper CreateMapper()
        {
            var MapConfig = new MapperConfiguration(M =>
            {

                M.CreateMap<User, UserDto>().ReverseMap();
                M.CreateMap<User, LoginDto>().ReverseMap();
                M.CreateMap<User, UpdateUserDto>().ReverseMap();
                M.CreateMap<User, WriteUserDto>().ReverseMap();
                M.CreateMap<PasswordReset, PasswordResetRequestDto>().ReverseMap();

                M.CreateMap<CarDetails, WriteCarDetailsDto>().ReverseMap();
                M.CreateMap<CarDetails, ReadCarDetailsDto>().ReverseMap();

                M.CreateMap<Why, WriteWhyDto>().ReverseMap();
                M.CreateMap<Why, ReadWhyDto>().ReverseMap();

                M.CreateMap<About, WriteAboutDto>().ReverseMap();
                M.CreateMap<About, ReadAboutDto>().ReverseMap();

                M.CreateMap<Testimonials, WriteTestimonialsDto>().ReverseMap();
                M.CreateMap<Testimonials, ReadTestimonialsDto>().ReverseMap();

                M.CreateMap<Contact, WriteContactDto>().ReverseMap();
                M.CreateMap<Contact, ReadContactDto>().ReverseMap();

                M.CreateMap<Cta, WriteCtaDto>().ReverseMap();
                M.CreateMap<Cta, ReadCtaDto>().ReverseMap();

                M.CreateMap<Home, WriteHomeDto>().ReverseMap();
                M.CreateMap<Home, ReadHomeDto>().ReverseMap();

                M.CreateMap<CarReservation, WriteCarReservationDto>().ReverseMap();
                M.CreateMap<CarReservation, ReadCarReservationDto>().ReverseMap();

                M.CreateMap<PaymentDetails, WritePaymentDetailsDto>().ReverseMap();
                M.CreateMap<PaymentDetails, ReadPaymentDetailsDto>().ReverseMap();

                M.CreateMap<CarFilter, WriteCarFilterDto>().ReverseMap();
                M.CreateMap<CarFilter, ReadCarFilterDto>().ReverseMap();

            });
            return MapConfig.CreateMapper();
        }
    }
}


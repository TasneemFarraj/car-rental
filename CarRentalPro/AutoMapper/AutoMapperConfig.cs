using AutoMapper;
using CarRentalPro.Dtos.UserDtos;
using CarRentalPro.Data.Models;
using CarRentalPro.Dtos.CredDtos;

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
                M.CreateMap<User, WriteUserDto>().ReverseMap();
                M.CreateMap<User, ForgetPasswordDto>().ReverseMap();


                M.CreateMap<CarSlider, WriteCarSliderDto>().ReverseMap();
                M.CreateMap<CarSlider, ReadCarSliderDto>().ReverseMap();

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


            });
            return MapConfig.CreateMapper();
        }
    }
}


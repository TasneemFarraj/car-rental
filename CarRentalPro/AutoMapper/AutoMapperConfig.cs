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
                M.CreateMap<CarSlider, WriteCarSliderDto>().ReverseMap();
                M.CreateMap<CarSlider, ReadCarSliderDto>().ReverseMap();

            });
            return MapConfig.CreateMapper();
        }
    }
}


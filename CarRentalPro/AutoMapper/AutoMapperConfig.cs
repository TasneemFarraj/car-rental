using AutoMapper;
using CarRentalPro.Dtos.UserDtos;
using CarRentalPro.Data.Models;

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

            });
            return MapConfig.CreateMapper();
        }
    }
}


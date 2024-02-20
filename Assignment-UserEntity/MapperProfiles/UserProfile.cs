using Assignment_UserEntity.Models;
using Assignment_UserEntity.Dtos;
using AutoMapper;

namespace Assignment_UserEntity.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<RegisterDto, User>().ReverseMap();
        }
    }
}

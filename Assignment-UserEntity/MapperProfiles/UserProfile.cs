using Assignment_UserEntity.Model;
using Assignment_UserEntity.ResponseDTO;
using AutoMapper;

namespace Assignment_UserEntity.MapperProfiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}

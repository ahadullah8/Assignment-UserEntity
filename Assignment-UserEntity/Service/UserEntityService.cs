using Assignment_UserEntity.Model;
using Assignment_UserEntity.Dtos;
using Assignment_UserEntity.Service.Contract;
using Assignment_UserEntity.ServiceResponder;
using AutoMapper;
using System.Diagnostics.Contracts;
using System.Reflection;

namespace Assignment_UserEntity.Service
{
    public class UserEntityService : IUserEntityService
    {
        private readonly IMapper _mapper;
        public UserEntityService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public ServiceResponse<UserDto> AddUser(UserDto? newUser)
        {

            ServiceResponse<UserDto> response = new();
            if (newUser == null)
            {
                response.Success = false;
                response.Message = "Object is null";
                return response;
            }
            var toAdd = _mapper.Map<User>(newUser);
            toAdd.Id = UsersData.UsersList.Last().Id + 1;
            UsersData.UsersList.Add(toAdd);
            response.Success = true;
            response.Message = "User Added Successfully";
            response.Data = newUser;
            return response;

        }

        public ServiceResponse<string> DeleteUser(int id)
        {
            ServiceResponse<string> response = new();

            //find user by id
            var user = UsersData.UsersList.Find(x => x.Id == id);
            if (user != null)
            {
                UsersData.UsersList.Remove(user);
                response.Success = true;
                response.Data = $"User with id: {id} is removed successfully";
                response.Message = "User Removed";
                return response;
            }
            response.Success = false;
            response.Message = "User not found";
            return response;

        }

        public ServiceResponse<UserDto> GetUser(int id)
        {
            ServiceResponse<UserDto> response = new();
            var userDetails = UsersData.UsersList.Find(x => x.Id == id);
            //check if user found or not
            if (userDetails is object)
            {
                response.Success = true;
                response.Data = _mapper.Map<UserDto>(userDetails);
                response.Message = "User found";
                return response;
            }
            response.Success = false;
            response.Message = "User not found!";
            return response;
        }

        public ServiceResponse<UserDto> UpdateUser(int id, UserDto updateUser)
        {
            ServiceResponse<UserDto> response = new();

            //get the matching user and update its data
            foreach (var item in UsersData.UsersList)
            {
                if (item.Id == id)
                {
                    item.Email = updateUser.Email;
                    item.FirstName = updateUser.FirstName;
                    item.LastName = updateUser.LastName;
                    item.UserName = updateUser.UserName;
                    response.Success = true;
                    response.Message = "User updated";
                    response.Data = updateUser;
                    return response;
                }
            }

            response.Success = false;
            response.Message = "User not found";
            return response;
        }
    }
}

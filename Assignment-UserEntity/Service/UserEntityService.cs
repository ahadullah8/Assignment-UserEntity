using Assignment_UserEntity.Models;
using Assignment_UserEntity.Dtos;
using Assignment_UserEntity.Service.Contract;
using Assignment_UserEntity.ServiceResponder;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Assignment_UserEntity.Service
{
    public class UserEntityService : IUserEntityService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public UserEntityService(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ServiceResponse<UserDto>> AddUserAsync(UserDto? newUser)
        {

            ServiceResponse<UserDto> response = new();
            if (newUser == null)
            {
                response.Success = false;
                response.Message = "Object is null";
                return response;
            }
            var toAdd = _mapper.Map<User>(newUser);
            await _context.Users.AddAsync(toAdd);
            await Save();
            response.Success = true;
            response.Message = "User Added Successfully";
            response.Data = newUser;
            return response;

        }

        public async Task<ServiceResponse<string>> DeleteUserAsync(int id)
        {
            ServiceResponse<string> response = new();
            var user = await _context.Users.FindAsync(id);
            //find user by id
            if (user is object)
            {
                _context.Users.Remove(user);
                if (await Save())
                {
                    response.Success = true;
                    response.Data = $"User with id: {id} is removed successfully";
                    response.Message = "User Removed";
                    return response;
                }
            }
            response.Success = false;
            response.Message = "User not found";
            return response;

        }

        public async Task<ServiceResponse<UserDto>> GetUserAsync(int id)
        {
            ServiceResponse<UserDto> response = new();
            var userDetails = await _context.Users.FindAsync(id);
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

        public async Task<ServiceResponse<UserDto>> UpdateUserAsync(int id, UserDto updateUser)
        {
            ServiceResponse<UserDto> response = new();
            //get the matching user and update its data
            var toUpdate = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (toUpdate is object)
            {
                toUpdate.Email = updateUser.Email;
                toUpdate.UserName = updateUser.UserName;
                toUpdate.FirstName = updateUser.FirstName;
                toUpdate.LastName = updateUser.LastName;
                _context.Users.Update(toUpdate);
                await Save();
                response.Success = true;
                response.Message = "User updated";
                response.Data = updateUser;
                return response;
            }
            response.Success = false;
            response.Message = "User not found";
            return response;
        }

        private async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }
    }
}

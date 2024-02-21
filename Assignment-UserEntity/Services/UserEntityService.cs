using Assignment_UserEntity.Dtos;
using Assignment_UserEntity.Models;
using Assignment_UserEntity.Services.Contract;
using AutoMapper;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Assignment_UserEntity.Services
{
    public class UserEntityService : IUserEntityService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        public UserEntityService(IMapper mapper, AppDbContext context, UserManager<User> userManager)
        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
        }
        public async Task<UserDto> AddUserAsync(UserDto newUser)
        {
            // check if username or email already exists or not
            if (await UserExists(newUser))
            {
                throw new Exception("User already exists");
            }
            // map the input to user entity object and save it to db
            var toAdd = _mapper.Map<User>(newUser);
            await _context.Users.AddAsync(toAdd);
            if (!await Save())
            {
                throw new Exception("Unable to save changes to the database");
            }
            return newUser;

        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            //find user by id if found remove it from db
            var user = await FindUser(id);
            if (user is object)
            {
                _context.Users.Remove(user);
                if (!await Save())
                {
                    throw new Exception("Unable to Delete user");
                }
                return true;
            }
            throw new Exception("User not found");

        }

        public async Task<UserDto> GetUserAsync(string id)
        {
            //find user by id and check if user found or not
            var userDetails = await FindUser(id);
            if (userDetails is object)
            {
                return _mapper.Map<UserDto>(userDetails);
            }
            throw new Exception("User not found");
        }

        public async Task<UserDto> UpdateUserAsync(string id, UserDto updateUser)
        {
            //get the matching user and update its data
            var toUpdate = await FindUser(id);
            if (toUpdate is object)
            {
                toUpdate.Email = updateUser.Email;
                toUpdate.UserName = updateUser.UserName;
                toUpdate.FullName = updateUser.FullName;
                _context.Users.Update(toUpdate);
                //check if changes are made successfully
                if (!await Save())
                {
                    throw new Exception("Unable to save changes to db");
                }
                //everything went smoothly
                return updateUser;
            }
            throw new Exception("User not found");
        }
        //gets the user by id and return the user object
        private async Task<User> FindUser(string id)
        {
            return await _context.Users.FindAsync(id);
            
        }
        //save changes to the database
        private async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }
        private async Task<bool> UserExists(UserDto dto)
        {
            var user = await _context.Users.Where(x=>x.UserName == dto.UserName || x.Email==dto.Email).FirstOrDefaultAsync();
            if (user is null)
            {
                return false;
            }
            return true;
        }
    }
}

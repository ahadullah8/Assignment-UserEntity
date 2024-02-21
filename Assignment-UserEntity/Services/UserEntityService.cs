using Assignment_UserEntity.Dtos;
using Assignment_UserEntity.Models;
using Assignment_UserEntity.Repositories.Contrat;
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
        private readonly IUserRepository _userRepo;
        public UserEntityService(IMapper mapper, IUserRepository userRepo)
        {
            _mapper = mapper;
            _userRepo = userRepo;
        }
        public UserDto AddUser(UserDto newUser)
        {
            // map the input to user entity
            var toAdd = _mapper.Map<User>(newUser);
            // check if user already exists or not
            if (_userRepo.UserExists(toAdd))
            {
                throw new Exception("User already exists");
            }
            _userRepo.Add(toAdd);
            _userRepo.Save();
            return newUser;

        }

        public bool DeleteUser(string id)
        {
            //find user by id if found remove it from db
            var user = _userRepo.GetById(id);
            if (user is object)
            {
                _userRepo.Delete(user);
                _userRepo.Save();
                return true;
            }
            throw new Exception("User not found");

        }

        public UserDto GetUser(string id)
        {
            //find user by id and check if user found or not
            var userDetails = _userRepo.GetById(id);
            if (userDetails is object)
            {
                return _mapper.Map<UserDto>(userDetails);
            }
            throw new Exception("User not found");
        }

        public UserDto UpdateUser(string id, UserDto updateUser)
        {
            //get the matching user and update its data
            var toUpdate = _userRepo.GetById(id);
            if (toUpdate is object)
            {
                toUpdate.Email = updateUser.Email;
                toUpdate.UserName = updateUser.UserName;
                toUpdate.FullName = updateUser.FullName;
                _userRepo.Update(toUpdate);
                _userRepo.Save();
                //everything went smoothly
                return updateUser;
            }
            throw new Exception("User not found");
        }
        
    }
}

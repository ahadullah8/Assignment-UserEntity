﻿using Assignment_UserEntity.Model;
using Assignment_UserEntity.ResponseDTO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_UserEntity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //static user list to contain the user in memory
        private static List<User> users = new List<User>()
        {
            new User()
            {
                Id = 1,
                FirstName = "Inni",
                LastName = "O",
                Email = "innio@gmail.com",
                UserName="innio"
            },
            new User()
            {
                Id = 2,
                FirstName = "mini",
                LastName = "O",
                Email = "minio@gmail.com",
                UserName="minio"
            },
            new User()
            {
                Id = 3,
                FirstName = "tini",
                LastName = "O",
                Email = "tinio@gmail.com",
                UserName="tinio"
            }

        };
        //IMapper to map the user object on userDTO 
        private readonly IMapper _mapper;

        //injected to IMapper instance using dependency injection
        public UsersController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetUser")] //defined route using attribute routing
        public IActionResult Get(int id)
        {
            // generic response set to send generic response to the user.
            var response = new GenericResponse<UserDTO>();
            var user = users.Where(x=>x.Id==id).FirstOrDefault(); //searching from the given list of users
            if (user == null)
            {
                response.Status = false;
                response.Message = "User Not Found";
                return BadRequest(response);
            }
            var userDTO = _mapper.Map<UserDTO>(user);
            response.Status = true;
            response.Body = userDTO;
            response.Message = "Success";
            return Ok(response);
        }
        [HttpPost]
        [Route("AddUser")]
        public IActionResult Add(User user)
        {
            var response = new GenericResponse<string>();
            if (user == null)
            {
                response.Status = false;
                response.Message = "Object is null or Empty";
                return BadRequest(response);
            }
            users.Add(user);
            response.Status = true;
            response.Body = "User Added successfully";
            response.Message = string.Empty;
            return Ok(response);
        }
        [HttpDelete]
        [Route("RemoveUser")]
        public IActionResult Delete(int id)
        {
            var response = new GenericResponse<string>();
            var user = users.Where(x=>x.Id==id).FirstOrDefault();
            if (user==null)
            {
                response.Status = false;
                response.Body= string.Empty;
                response.Message = $"User with id: {id} not found";
                return BadRequest(response);
            }
            users.Remove(user);
            response.Status = true;
            response.Body = "User removed Successfully!";
            response.Message= string.Empty;
            return Ok(response);
        }

        [HttpPut]
        [Route("UpdateUser")]
        public IActionResult Update(User user)
        {
            var response = new GenericResponse<UserDTO>();
            for (int i = 0;i< users.Count; i++)
            {
                if (users[i].Id==user.Id)
                {
                    users[i].FirstName = user.FirstName;
                    users[i].LastName = user.LastName;
                    users[i].Email = user.Email;
                    users[i].UserName = user.UserName;
                    response.Status= true;
                    response.Body = _mapper.Map<UserDTO>(users[i]);
                    response.Message = string.Empty;
                    return Ok(response);
                }
            }
            response.Status = false;
            response.Body = null;
            response.Message = $"User with id: {user.Id} not found!";
            return BadRequest(response);
        }

    }
}

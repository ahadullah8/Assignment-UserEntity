using Assignment_UserEntity.Model;
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

        //injected to IMapper instance using dependency injection in constructor
        public UsersController(IMapper mapper)
        {
            _mapper = mapper;
        }
        /// <summary>
        /// Find user by id and send user info in response
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUser")] //defined route using attribute routing
        public IActionResult GetUser(int id)
        {
            // generic response set to send generic response to the user.
            var response = new GenericResponse<UserDTO>();
            var user = users.Where(x=>x.Id==id).FirstOrDefault(); //searching from the given list of users
            if (user == null)
            {
                response.Status = false;
                response.ErrorMessage = "User Not Found";
                return BadRequest(response);
            }
            // map the user object to userDTO object
            var userDTO = _mapper.Map<UserDTO>(user);
            response.Status = true;
            response.Body = userDTO;
            response.ErrorMessage = "Success";
            return Ok(response);
        }
        /// <summary>
        /// Add new user to the list
        /// </summary>
        /// <param name="user">User type</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddUser")]
        public IActionResult AddUser(User user)
        {
            var response = new GenericResponse<string>();
            if (user == null)
            {
                response.Status = false;
                response.ErrorMessage = "Object is null or Empty";
                return BadRequest(response);
            }
            users.Add(user);
            response.Status = true;
            response.Body = "User Added successfully";
            response.ErrorMessage = string.Empty;
            return Ok(response);
        }
        /// <summary>
        /// Deletes the user whoes id 
        /// </summary>
        /// <param name="id">Id of the user to be deleted</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("RemoveUser")]
        public IActionResult DeleteUser(int id)
        {
            var response = new GenericResponse<string>();
            var user = users.Where(x=>x.Id==id).FirstOrDefault();
            if (user==null)
            {
                response.Status = false;
                response.Body= string.Empty;
                response.ErrorMessage = $"User with id: {id} not found";
                return BadRequest(response);
            }
            users.Remove(user);
            response.Status = true;
            response.Body = "User removed Successfully!";
            response.ErrorMessage= string.Empty;
            return Ok(response);
        }
        /// <summary>
        /// Updates the already existing user in the list
        /// </summary>
        /// <param name="user">User object along with id, to be updated</param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateUser")]
        public IActionResult UpdateUser(User user)
        {
            var response = new GenericResponse<UserDTO>();
            //itrates over users list and find the matching user and update it 
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
                    response.ErrorMessage = string.Empty;
                    return Ok(response);
                }
            }
            response.Status = false;
            response.Body = null;
            response.ErrorMessage = $"User with id: {user.Id} not found!";
            return BadRequest(response);
        }

    }
}

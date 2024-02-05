using Assignment_UserEntity.Model;
using Assignment_UserEntity.ResponseDTO;
using Assignment_UserEntity.Service.Contract;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_UserEntity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserEntityService _userEntityService;

        //injected to IMapper instance using dependency injection in constructor
        public UsersController(IUserEntityService userEntityService)
        {
            _userEntityService = userEntityService;
        }
        /// <summary>
        /// Find user by id and send user info in response
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUser/{id}")] //defined route using attribute routing
        public IActionResult GetUser(int id)
        {
            return Ok(_userEntityService.GetUser(id));
        }
        /// <summary>
        /// Add new user to the list
        /// </summary>
        /// <param name="user">User type</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddUser")]
        public IActionResult AddUser(UserDto? newUser)
        {
            return Ok(_userEntityService.AddUser(newUser));
        }

        [HttpDelete]
        [Route("RemoveUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var res = _userEntityService.DeleteUser(id);
            if (res.Message == "Not found")
            {
                return NotFound(res);
            }
            return Ok(res);
        }
        /// <summary>
        /// Updates the already existing user in the list
        /// </summary>
        /// <param name="user">User object along with id, to be updated</param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateUser/{id}")]
        public IActionResult UpdateUser(int id, UserDto userDTO)
        {
            return Ok(_userEntityService.UpdateUser(id, userDTO));
        }


    }
}

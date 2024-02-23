using Assignment_UserEntity.Dtos;
using Assignment_UserEntity.Middlewares.CustomAuthFilter;
using Assignment_UserEntity.Middlewares.Validator;
using Assignment_UserEntity.Models;
using Assignment_UserEntity.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Assignment_UserEntity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthFilter]
    public class UsersController : BaseController
    {

        private readonly IUserEntityService _userEntityService;

        //userEntitySerivce is injected
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
        [ValidateModelState]
        public IActionResult GetUserAsync(string id)
        {
            try
            {
                return Ok(_userEntityService.GetUser(id));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Add new user to the list
        /// </summary>
        /// <param name="user">User type</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddUser")]
        [ValidateModelState]
        public IActionResult AddUser(UserDto newUser)
        {
            try
            {
                return Ok(_userEntityService.AddUser(newUser));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Find and Deletes the user whoes id is given
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("RemoveUser/{id}")]
        [ValidateModelState]
        public IActionResult DeleteUser(string id)
        {
            try
            {
                return Ok(_userEntityService.DeleteUser(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Updates the already existing user in the list
        /// </summary>
        /// <param name="user">User object along with id, to be updated</param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateUser/{id}")]
        [ValidateModelState]
        public IActionResult UpdateUser(string id, UserDto userDTO)
        {
            try
            {
                return Ok(_userEntityService.UpdateUser(id, userDTO));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetUsers")]
        public async Task<IActionResult> GetUsers([FromQuery] UserListParameters parameters)
        {
            try
            {
                var result = await _userEntityService.GetAllUserAsync(parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}

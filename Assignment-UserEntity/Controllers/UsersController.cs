using Assignment_UserEntity.Dtos;
using Assignment_UserEntity.Middlewares.CustomAuthFilter;
using Assignment_UserEntity.Middlewares.Validator;
using Assignment_UserEntity.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetUserAsync(string id)
        {
            try
            {
                return Ok(await _userEntityService.GetUserAsync(id));

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
        public async Task<IActionResult> AddUserAsync(UserDto newUser)
        {
            try
            {
                return Ok(await _userEntityService.AddUserAsync(newUser));
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
        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            try
            {
                return Ok(await _userEntityService.DeleteUserAsync(id));
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
        public async Task<IActionResult> UpdateUserAsync(string id, UserDto userDTO)
        {
            var a = User.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            try
            {
                return Ok(await _userEntityService.UpdateUserAsync(id, userDTO));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

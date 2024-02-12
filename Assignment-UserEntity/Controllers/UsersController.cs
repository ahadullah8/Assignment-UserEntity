using Assignment_UserEntity.Dtos;
using Assignment_UserEntity.Service.Contract;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_UserEntity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> GetUserAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest(id + " is not a valid id");
            }
            try
            {
                var res = await _userEntityService.GetUserAsync(id);
                if (res.Success)
                {
                    return Ok(res.Data, res.Message);
                }
                return BadRequest(res.Message);

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
        public async Task<IActionResult> AddUserAsync(UserDto newUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("One or more validations failed");
            }
            try
            {
                var res = await _userEntityService.AddUserAsync(newUser);
                if (!res.Success)
                {
                    return BadRequest(res.Message);
                }
                return Ok(res.Data, res.Message);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Find Deletes the user whoes id is given
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("RemoveUser/{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest(id + " is not a valid id");
            }
            try
            {
                var res = await _userEntityService.DeleteUserAsync(id);
                if (!res.Success)
                {
                    return BadRequest(res.Message);
                }
                return Ok(res.Data, res.Message);

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
        public async Task<IActionResult> UpdateUserAsync(int id, UserDto userDTO)
        {
            if (id <= 0)
            {
                return BadRequest(id + " is not a valid id");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("One or more validations failed");
            }
            try
            {
                var res = await _userEntityService.UpdateUserAsync(id, userDTO);
                if (!res.Success)
                {
                    return BadRequest(res.Message);
                }
                return Ok(res.Data, res.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

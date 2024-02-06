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
        public IActionResult GetUser(int id)
        {
            try
            {
                var res = _userEntityService.GetUser(id);
                if (res.Success)
                {
                    return Ok(res.Data);
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
        public IActionResult AddUser(UserDto newUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("One or more validations failed");
            }
            try
            {
                var res = _userEntityService.AddUser(newUser);
                if (!res.Success)
                {
                    return BadRequest(res.Message);
                }
                return Ok(res.Data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("RemoveUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var res = _userEntityService.DeleteUser(id);
                if (!res.Success)
                {
                    return BadRequest(res.Message);
                }
                return Ok(res.Data);

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
        public IActionResult UpdateUser(int id, UserDto userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("One or more validations failed");
            }
            try
            {
                var res = _userEntityService.UpdateUser(id, userDTO);
                if (!res.Success)
                {
                    return BadRequest(res.Message);
                }
                return Ok(res.Data,res.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

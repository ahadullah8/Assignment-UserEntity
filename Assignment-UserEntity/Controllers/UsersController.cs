using Assignment_UserEntity.Model;
using Assignment_UserEntity.Model.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Assignment_UserEntity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //list to contain users in memory
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
        /// <summary>
        /// Find user by id and send user info in response
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUser/{id}")]
        public IActionResult Get(int id)
        {
            var user = users.Where(x => x.Id == id).FirstOrDefault();
            if (user == null)
            {
                return NotFound(JsonConvert.SerializeObject($"User with id: {id} not found"));
            }
            return Ok(JsonConvert.SerializeObject(user, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }));
        }
        /// <summary>
        /// Add new user to the list
        /// </summary>
        /// <param name="user">User type</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddUser")]
        public IActionResult Add(UserDTO? newUser)
        {
            if (newUser == null)
            {
                return BadRequest(JsonConvert.SerializeObject("Object is null"));
            }
            var user = new User()
            {
                Id = users.Last().Id + 1,
                Email = newUser.Email,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                UserName = newUser.Email,
            };
            users.Add(user);
            return Ok(JsonConvert.SerializeObject("New user added!"));
        }
        /// <summary>
        /// Deletes the user whoes id 
        /// </summary>
        /// <param name="id">Id of the user to be deleted</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("RemoveUser/{id}")]
        public IActionResult Delete(int id)
        {
            var user = users.Where(x => x.Id == id).FirstOrDefault();
            if (user == null)
            {
                return NotFound(JsonConvert.SerializeObject($"User not found"));
            }

            users.Remove(user);
            return Ok(JsonConvert.SerializeObject("User removed successfully!"));
        }

        /// <summary>
        /// Updates the already existing user in the list
        /// </summary>
        /// <param name="user">User object along with id, to be updated</param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateUser/{id}")]
        public IActionResult Update(int id, User? user)
        {
            //try
            //{
            //    for (int i = 0; i < users.Count; i++)
            //    {
            //        if (users[i].Id == user.Id)
            //        {
            //            users[i].FirstName = user.FirstName;
            //            users[i].LastName = user.LastName;
            //            users[i].Email = user.Email;
            //            users[i].UserName = user.UserName;
            //            return Ok("User info updated successfully");
            //        }
            //    }
            //    return NotFound($"User with id: {user.Id} not found!");
            //}
            //catch (Exception e)
            //{
            //    return StatusCode(500,e.Message);
            //}
            if (user==null)
            {
                return BadRequest(JsonConvert.SerializeObject("User is null"));
            }

            foreach(var item in users)
            {
                if (item.Id == id)
                {
                    item.Email = user.Email;
                    item.FirstName = user.FirstName;
                    item.LastName = user.LastName;
                    item.UserName = user.UserName;
                    return Ok(JsonConvert.SerializeObject("User Updated Successfully"));
                }
            }
            return NotFound(JsonConvert.SerializeObject("User not found"));
            
        }

    }
}

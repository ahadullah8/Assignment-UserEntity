using Assignment_UserEntity.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [Route("GetUser")]
        public string Get(int id)
        {
            var user = users.Where(x=>x.Id==id).FirstOrDefault();
            if (user == null)
            {
                return $"User with id: {id} not found";
            }
            return $"Id: {user.Id}\nUser Name: {user.UserName}\nFirst Name: {user.FirstName}\nLast Name: {user.LastName}\nEmail: {user.Email}";
        }
        /// <summary>
        /// Add new user to the list
        /// </summary>
        /// <param name="user">User type</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddUser")]
        public string Add(User user)
        {
            if (user == null)
            {
                return "Error";
            }
            users.Add(user);
            return "User Added Successfully";
        }
        /// <summary>
        /// Deletes the user whoes id 
        /// </summary>
        /// <param name="id">Id of the user to be deleted</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("RemoveUser")]
        public string Delete(int id)
        {
            var user = users.Where(x=>x.Id==id).FirstOrDefault();
            if (user==null)
            {
                return $"User with id: {id} not found";
            }
            users.Remove(user);
            return "User removed successfully!";
        }

        /// <summary>
        /// Updates the already existing user in the list
        /// </summary>
        /// <param name="user">User object along with id, to be updated</param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateUser")]
        public string Update(User user)
        {
            for (int i = 0;i< users.Count; i++)
            {
                if (users[i].Id==user.Id)
                {
                    users[i].FirstName = user.FirstName;
                    users[i].LastName = user.LastName;
                    users[i].Email = user.Email;
                    users[i].UserName = user.UserName;
                    return "User info updated successfully";
                }
            }
            return $"User with id: {user.Id} not found!";
        }

    }
}

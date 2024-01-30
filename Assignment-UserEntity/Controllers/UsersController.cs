using Assignment_UserEntity.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_UserEntity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static List<User> users = new List<User>();

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

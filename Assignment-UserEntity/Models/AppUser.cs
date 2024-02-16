using Microsoft.AspNetCore.Identity;

namespace Assignment_UserEntity.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}

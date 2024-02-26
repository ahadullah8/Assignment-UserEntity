using Microsoft.AspNetCore.Identity;

namespace Assignment_UserEntity.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public string? Address { get; set; }
    }
}

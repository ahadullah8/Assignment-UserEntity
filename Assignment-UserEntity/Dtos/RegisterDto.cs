using System.ComponentModel.DataAnnotations;

namespace Assignment_UserEntity.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

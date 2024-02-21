using System.ComponentModel.DataAnnotations;

namespace Assignment_UserEntity.Dtos
{
    public class UserDto
    {
        [Required(ErrorMessage = "Full name Required")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Email Required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Username Required")]
        public string UserName { get; set; }
        public string Address { get; set; }
    }
}

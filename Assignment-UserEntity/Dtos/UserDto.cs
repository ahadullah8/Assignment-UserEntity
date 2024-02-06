using System.ComponentModel.DataAnnotations;

namespace Assignment_UserEntity.Dtos
{
    public class UserDto
    {
        [Required(ErrorMessage = "First name Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name Required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email Required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Username Required")]
        public string UserName { get; set; }
    }
}

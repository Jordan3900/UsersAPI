using System.ComponentModel.DataAnnotations;

namespace UsersAPI.DtoModels
{
    public class LoginInput
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(3)]
        public string Password { get; set; }
    }
}

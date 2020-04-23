using System.ComponentModel.DataAnnotations;

namespace UsersAPI.DtoModels
{
    public class RegisterInput
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [MinLength(3)]
        public string Password { get; set; }

        public string Picture { get; set; }
    }
}

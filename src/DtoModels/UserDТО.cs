using System.ComponentModel.DataAnnotations;

namespace UsersAPI.DtoModels
{
    public class UserDТО
    {
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Picture { get; set; }
    }
}

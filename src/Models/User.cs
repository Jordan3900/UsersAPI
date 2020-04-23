using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UsersAPI.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
       
        public string Picture { get; set; }

    }
}

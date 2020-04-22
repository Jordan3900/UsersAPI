using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersAPI.Data;
using UsersAPI.DtoModels;

namespace UsersAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public UsersController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDТО>> GetUsers()
        {
            var users = context.Users.Select(x => new UserDТО
            {
                FirstName = x.FirstName ?? "",
                LastName = x.LastName ?? "",
                Picture = x.Picture,
                Id = x.Id
            }).ToArray();

            return users;
        }

        [HttpGet]
        public ActionResult<UserDТО> GetUser(string id)
        {
            var user = context.Users.Select(x => new UserDТО
            {
                FirstName = x.FirstName ?? "",
                LastName = x.LastName ?? "",
                Picture = x.Picture,
                Id = x.Id
            }).FirstOrDefault(x => x.Id == id);

            return user;
        }
    }
}
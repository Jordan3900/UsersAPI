using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersAPI.DtoModels;
using UsersAPI.Services.conracts;

namespace UsersAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDТО>> GetUsers(int page = 1, int perPage = 6)
        {
            var users = usersService.GetAllUsersDTO().Skip((page - 1) * perPage)
              .Take(perPage)
              .ToList();

            return users;
        }

        [HttpGet]
        public ActionResult<UserDТО> GetUser(string id)
        {
            var user = this.usersService.GetUserDTO(id);

            return user;
        }

        [HttpPost]
        async public Task<ActionResult> AddUser(UserDТО user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var succeed = await this.usersService.AddUser(user);

            if (!succeed)
            {
                return BadRequest();
            }

            return Ok(new
            {
                firstName = user.FirstName,
                lastName = user.LastName,
                createAt = DateTime.UtcNow
            });
        }

        [HttpPut]
        async public Task<ActionResult> EditUser(string id, UserDТО user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await this.usersService.EditUser(id, user);

            return Ok(new
            {
                firstName = user.FirstName,
                lastName = user.LastName,
                updateAt = DateTime.UtcNow
            });
        }

        [HttpDelete]
        public void DeleteUser(string id)
        {
            var user = this.usersService.GetUser(id);

            this.usersService.DeleteUser(user);
        }
    }
}
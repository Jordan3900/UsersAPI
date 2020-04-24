using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersAPI.DtoModels;
using UsersAPI.Services.Conracts;

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
        async public Task<IEnumerable<UserDТО>> GetUsers(int page = 1, int perPage = 6)
        {
            var users = await usersService.GetAllUsersDTO(page, perPage);

            return users;
        }

        [HttpGet]
        async public Task<ActionResult<UserDТО>> GetUser(string id)
        {
            var user = await this.usersService.GetUserDTO(id);

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
        async public Task<ActionResult> DeleteUser(string id)
        {
            var user = await this.usersService.GetUser(id);

            await this.usersService.DeleteUser(user);

            return Ok(new
            {
                deletedAt = DateTime.UtcNow
            });
        }
    }
}
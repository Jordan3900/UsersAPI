using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersAPI.DtoModels;
using UsersAPI.Filters;
using UsersAPI.Services.Contracts;

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
        public async Task<ActionResult<IEnumerable<UserDТО>>> GetUsers(int page = 1, int perPage = 6)
        {
            var users = await usersService.GetAllUsersDTO(page, perPage);

            return Ok(users);
        }

        [HttpGet]
        public async Task<ActionResult<UserDТО>> GetUser(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }

            var user = await this.usersService.GetUserDTO(id);

            return user;
        }

        [HttpPost]
        [ModelStateValidationFilter]
        public async Task<ActionResult> AddUser(UserDТО userDTO)
        {
            var user = await this.usersService.AddUser(userDTO);

            return CreatedAtAction("AddUser", user);
        }

        [HttpPut]
        [ModelStateValidationFilter]
        public async Task<ActionResult> EditUser(string id, UserDТО user)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }

            var editedUser = await this.usersService.EditUser(id, user);

            return Ok(editedUser);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUser(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }

            var user = await this.usersService.GetUser(id);

            await this.usersService.DeleteUser(user);

            return Ok(new
            {
                deletedAt = DateTime.UtcNow
            });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult<IEnumerable<UserDТО>> GetUsers()
        {
            var users = usersService.GetAllUsers()
                .ToList();

            return users;
        }

        [HttpGet]
        public ActionResult<UserDТО> GetUser(string id)
        {
            var user = usersService.GetUser(id);

            return user;
        }

        [HttpPut]
        public ActionResult EditUser(string id, UserDТО user)
        {
            usersService.EditUser(id, user);

            return Ok(new
            {
                firstName = user.FirstName,
                lastName = user.LastName,
                updateAt = DateTime.UtcNow
            });
        }
    }
}
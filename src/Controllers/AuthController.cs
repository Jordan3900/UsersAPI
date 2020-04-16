using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsersAPI.Data;
using UsersAPI.Models;

namespace UsersAPI.Controllers
{

    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly ApplicationDbContext context;

        public AuthController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            ApplicationDbContext context)
        {
            this.signInManager = signInManager;
            this.context = context;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                user.UserName = user.Email;
                var result = await this.userManager.CreateAsync(user, user.Password);

                if (result.Succeeded)
                {
                    return Ok(new JsonResult("Successful"));
                }
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult> Login(User user)
        {
            if (ModelState.IsValid)
            {
                var result = await this.signInManager.PasswordSignInAsync(user.Email, user.Password, true, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    return Ok(new JsonResult("Successful"));
                }
            }
        
            return BadRequest(new JsonResult("Failed"));
        }
    }
}
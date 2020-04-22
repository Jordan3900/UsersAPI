using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UsersAPI.DtoModels;
using UsersAPI.Models;

namespace UsersAPI.Controllers
{

    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;

        public AuthController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterInput input)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    FirstName = input.FirstName,
                    LastName = input.LastName,
                    UserName = input.Email,
                    Email = input.Email,
                    Picture = input.Picture
                };

                var result = await userManager.CreateAsync(user, input.Password);

                if (result.Succeeded)
                {
                    return Ok(new JsonResult("Successful"));
                }
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginInput input)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(input.Email, input.Password, true, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]{
                             new Claim(ClaimTypes.Name, input.Email)
                     }),
                        Issuer = configuration["JwtToken:Issuer"],
                        Audience = configuration["JwtToken:Issuer"],
                        Expires = DateTime.Now.AddDays(7),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtToken:SecretKey"])), SecurityAlgorithms.HmacSha512Signature),
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);

                    return Ok(new
                    {
                        token = tokenHandler.WriteToken(token),
                        expiration = token.ValidTo
                    });

                }
            }

            return BadRequest();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swagger_API.Infrastructure.Repositories.Abstraction;
using Swagger_API.Models;

namespace Swagger_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _repo;
        public AccountController(IUserRepository userRepository) => _repo = userRepository;

        [HttpPost("login")]
        public IActionResult LogIn([FromBody] AppUser appUser)
        {
            var user = _repo.Authentication(appUser.UserName, appUser.Password);
            if (user==null)
            {
                return BadRequest(new { message = "User name of password is incorrent..!" });
            }
            return Ok(appUser);
        }


        [HttpPost("register")]
        public IActionResult Register([FromBody] AppUser appUser) 
        {
            bool ifUserUnique = _repo.IsUniqueUser(appUser.UserName);
            if (!ifUserUnique)
            {
                return BadRequest(new {message = "User name already is exsist" });
            }
            var user = _repo.Register(appUser.UserName, appUser.Password);

            if (user == null)
            {
                return BadRequest(new { message = "Something goes wrong" });
            }
            return Ok();
        }
        
    }
}

using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using server.Models;
using BCrypt.Net;
using MongoDB.Driver;

namespace server.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService) => _userService = userService;

        [HttpPost]
        public async Task<ActionResult<User>> Create([FromBody] JsonUser jsonUser)
        {
            var user = new User
            {
                Username = jsonUser.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(jsonUser.Password)
            };

            var userExists = await _userService.GetByUsernameAsync(jsonUser.Username);
            if (userExists != null)
            {
                return Conflict(new { message = "Username already taken" });
            }

            await _userService.Create(user);
            return Ok(new { message = "user successfully created!" });
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return Ok();
        }
    }
}
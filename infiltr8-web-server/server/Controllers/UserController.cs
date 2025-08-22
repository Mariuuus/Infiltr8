using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using server.Models;

namespace server.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService) {
            _userService = userService;
        }

        [HttpPost]
        public async ActionResult<User> Create([FromBody] JsonUser jsonUser)
        {
            var user = new User
            {
                Username = jsonUser.Username,
                // TODO: add hashing! (probably with bcrypt)
                Password = jsonUser.Password
            };

            await _userService.Create(user);
            return CreatedAtAction(nameof(Create), new { id = user.UserId }, user);
        }
    }
}
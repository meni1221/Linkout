using Linkout.DTO;
using Linkout.Models;
using Linkout.Services;
using Microsoft.AspNetCore.Mvc;

namespace Linkout.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;
        private readonly JtWService jtWService;
        public UserController (UserService _userService, JtWService _jtWService) 
        { 
            userService = _userService;
            jtWService = _jtWService;
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task <ActionResult <SingleUserResponseDTO>>  getUser(int id)
        {
            UserModel user = await userService.getUserById(id);
            return user != null? Ok(user) : NotFound();
        }        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task <ActionResult <string>>  register([FromBody] UserModel user)
        {
            int userId = await userService.register(user);
            if (userId !=0)
            {
                await Response.WriteAsync(userId.ToString());
                return Created();
            }
            else
            {
               return BadRequest();
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> login([FromBody] UserModel user)
        {
            UserModel userFromDb = await userService.getUserByUserNameAndPassword (user.username, user.UNHASHEDpassword);
            if (userFromDb == null)
            {
                return Unauthorized("Invalid user name or password");
            }
            string token = jtWService.genJWToken(userFromDb);
            return Ok(token);
        }
    }
}

using HienTangToc.Interface;
using HienTangToc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HienTangToc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserInterface _userService;

        public UserController(UserInterface userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> GetAllUsers(int pageNumber = 1, int pageSize = 10)
        {
            var users = await _userService.GetAllUsersAsync(pageNumber, pageSize);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(user);
        }
        

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (createdUser, message) = await _userService.CreateUserAsync(userModel);
            if (createdUser == null)
            {
                return BadRequest(new { Message = message });
            }
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }



        [HttpPost]
        public async Task<ActionResult> CreateUser(UserModel user)
        {
            var (createdUser, message) = await _userService.CreateUserAsync(user);
            if (createdUser == null)
            {
                return BadRequest(message);
            }
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, UserModel userUpdate)
        {
            var (originalUser, updatedUser, message) = await _userService.UpdateUserAsync(id, userUpdate);
            if (updatedUser == null)
            {
                return BadRequest(message);
            }
            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var (success, message) = await _userService.DeleteUserAsync(id);
            if (!success)
            {
                return NotFound(message);
            }
            return NoContent();
        }


        
    }
}

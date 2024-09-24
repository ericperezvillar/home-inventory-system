using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.API.DTOs;
using UserManagement.API.Interfaces;
using WebApi.Controllers;

namespace UserManagement.API.Controllers
{
    [ApiController]    
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;

        public UsersController(IUserService userService, ILogger<AuthController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var user = await _userService.RegisterUser(userDto);
                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected exception on Register user");
                return BadRequest(ex.Message);
            }           
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                if (user == null) return NotFound();
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected exception on Get user");
                return BadRequest(ex.Message);
            }           
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected exception on Get All user");
                return BadRequest(ex.Message);
            }            
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _userService.UpdateUser(id, userDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected exception on Update user");
                return BadRequest(ex.Message);
            }           
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _userService.DeleteUser(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected exception on Delete user");
                return BadRequest(ex.Message);
            }            
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using UserManagement.API.DTOs;
using UserManagement.API.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;
        private readonly IJwtTokenHelper _jwtTokenHelper;

        public AuthController(IUserService userService, IJwtTokenHelper jwtTokenHelper, ILogger<AuthController> logger)
        {
            _userService = userService;
            _jwtTokenHelper = jwtTokenHelper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthRequestDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = await _userService.AuthenticateUser(request.Username, request.Password);
                if (user == null) return Unauthorized();

                var token = _jwtTokenHelper.GenerateToken(user);

                return Ok(new { Token = token });
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Unexpected exception on Auth user");
                return BadRequest(exception.Message);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using prosjekt_webapp2.Data.Repositories;

namespace Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User model)
        {
            if (string.IsNullOrEmpty(model.Email))
            {
                return BadRequest(new { message = "Email is required." });
            }

            var success = await _authRepository.RegisterUserAsync(model);

            if (!success)
            {
                return BadRequest(new { message = "Username already exists." });
            }

            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User model)
        {
            var user = await _authRepository.LoginUserAsync(model.Username, model.Password);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid username or password." });
            }

            var token = "mock-jwt-token";
            return Ok(new { token });
        }

        [HttpDelete("delete/{username}")]
        public async Task<IActionResult> DeleteAccount(string username)
        {
            var success = await _authRepository.DeleteUserAsync(username);
            if (!success)
            {
                return BadRequest(new { message = "Account deletion failed." });
            }

            return Ok(new { message = "Account deleted successfully." });
        }

    }
}

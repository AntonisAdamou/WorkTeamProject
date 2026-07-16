using Microsoft.AspNetCore.Mvc;
using WorkTeamProject.API.DTOs.AuthDTO;
using WorkTeamProject.API.Repositories.AuthRepo;

namespace WorkTeamProject.API.Controllers
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

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO register)
        {
            var user = await _authRepository.Register(register);
            return Ok(user);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO login)
        {
            var success = await _authRepository.Login(login);
            if (!success)
                return BadRequest(new {message = "Email or password is incorrect." });

            var token = await _authRepository.GenerateToken(login.UserEmail);
            return Ok(new { token });
        }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkTeamProject.API.Models;

namespace WorkTeamProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("/Register")]
        public async Task<IActionResult> Register([FromBody] User user, string password)
        {
            return Ok();
        }

        [HttpPost("/Login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            return Ok();
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkTeamProject.API.Data;
using WorkTeamProject.API.DTOs;
using WorkTeamProject.API.Models;
using WorkTeamProject.API.Repositories;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    // GET: api/User
    [HttpGet("GetAllUsers")]
    public async Task<ActionResult<IEnumerable<UserResponseDTO>>> GetUsers()
    {
        return Ok(await _userRepository.GetUsers());
    }

    // GET: api/User/1
    [HttpGet("GetUserById/{userId}")]
    public async Task<ActionResult<UserResponseDTO>> GetUser(int userId)
    {
        var user = await _userRepository.GetUser(userId);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    // PUT: api/User/1
    [HttpPut("UpdateUser/{userId}")]
    public async Task<IActionResult> PutUser(int userId, [FromBody] UserRequestDTO user)
    {
        var existingUser = await _userRepository.PutUser(userId, user);
        if (!existingUser)
        {
            return NotFound();
        }

        return NoContent();
    }

    // POST: api/User
    [HttpPost("CreateUser")]
    public async Task<ActionResult<User>> PostUser([FromBody] UserRequestDTO user)
    {
        var newUser = await _userRepository.PostUser(user);

        return CreatedAtAction("GetUser", new { userId = newUser.UserId }, newUser);
    }

    // DELETE: api/User/1
    [HttpDelete("DeleteUser/{userId}")]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        var user = await _userRepository.DeleteUser(userId);
        if (!user)
        {
            return NotFound();
        }

        return NoContent();
    }
}

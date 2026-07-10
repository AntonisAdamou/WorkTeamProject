using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkTeamProject.API.Data;
using WorkTeamProject.API.DTOs;
using WorkTeamProject.API.Models;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;
    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/User
    [HttpGet("GetAllUsers")]
    public async Task<ActionResult<IEnumerable<UserResponseDTO>>> GetUsers()
    {
        //return await _context.Users.ToListAsync();

        var users = await _context.Users.Include(u => u.UserRoles!).ThenInclude(ur => ur.Role).ToListAsync();

        var results = new List<UserResponseDTO>();
        foreach(var user in users)
        {
            results.Add(new UserResponseDTO
            {
                UserId = user.UserId,
                UserName = user.UserName,
                UserEmail = user.UserEmail,
                UserPassword = user.UserPassword,
                Roles = user.UserRoles!.Select(ur => ur.Role!.RoleName).ToList()
            });
        }

        return results;
    }

    // GET: api/User/1
    [HttpGet("GetUserById/{userId}")]
    public async Task<ActionResult<UserResponseDTO>> GetUser(int userId)
    {
        var user = await _context.Users.Include(u => u.UserRoles!).ThenInclude(ur => ur.Role).FirstOrDefaultAsync(u => u.UserId == userId);

        if (user == null)
        {
            return NotFound();
        }

        return new UserResponseDTO
        {
            UserId = user.UserId,
            UserName = user.UserName,
            UserEmail = user.UserEmail,
            UserPassword = user.UserPassword,
            Roles = user.UserRoles!.Select(ur => ur.Role!.RoleName).ToList()
        };
    }

    // PUT: api/User/1
    [HttpPut("UpdateUser/{userId}")]
    public async Task<IActionResult> PutUser(int userId, [FromBody] UserRequestDTO user)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        if (existingUser == null)
        {
            return NotFound();
        }

        existingUser.UserName = user.UserName;
        existingUser.UserEmail = user.UserEmail;
        existingUser.UserPassword = user.UserPassword;

        _context.Users.Update(existingUser);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/User
    [HttpPost("CreateUser")]
    public async Task<ActionResult<User>> PostUser([FromBody] UserRequestDTO user)
    {
        var newUser = new User
        {
            UserName = user.UserName,
            UserEmail = user.UserEmail,
            UserPassword = user.UserPassword,
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetUser", new { userId = newUser.UserId }, newUser);
    }

    // DELETE: api/User/1
    [HttpDelete("DeleteUser/{userId}")]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

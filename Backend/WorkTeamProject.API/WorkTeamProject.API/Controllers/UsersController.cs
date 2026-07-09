using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkTeamProject.API.Models;
using WorkTeamProject.API.Data;

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
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }

    // GET: api/User/5
    [HttpGet("GetUserById/{userid}")]
    public async Task<ActionResult<User>> GetUser(int userId)
    {
        var user = await _context.Users.FindAsync(userId);

        if (user == null)
        {
            return NotFound();
        }

        return user;
    }

    // PUT: api/User/5
    [HttpPut("UpdateUser/{userid}")]
    public async Task<IActionResult> PutUser(int userId, User user)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        if (existingUser == null)
        {
            return NotFound();
        }

        existingUser.UserName = user.UserName;
        existingUser.UserEmail = user.UserEmail;
        existingUser.UserPassword = user.UserPassword;
        existingUser.UserRoles = user.UserRoles;

        _context.Users.Add(existingUser);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/User
    [HttpPost("CreateUser")]
    public async Task<ActionResult<User>> PostUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetUser", new { userId = user.UserId }, user);
    }

    // DELETE: api/User/5
    [HttpDelete("DeleteUser/{userid}")]
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

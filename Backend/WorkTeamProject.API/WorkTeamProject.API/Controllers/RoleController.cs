using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkTeamProject.API.Models;
using WorkTeamProject.API.Data;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly AppDbContext _context;
    public RoleController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("GetRoles")]
    public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
    {
        return Ok(await _context.Roles.ToListAsync());
    }

    [HttpGet("GetRoleById/{roleid}")]
    public async Task<ActionResult<Role>> GetRoleById(int roleid)
    {
        var role = await _context.Roles.FindAsync(roleid);

        if (role == null)
        {
            return NotFound();
        }

        return Ok(role);
    }

    [HttpPut("UpdateRole/{roleid}")]
    public async Task<IActionResult> UpdateRole(int? roleid, Role role)
    {
        var existingRole = await _context.Roles.FindAsync(roleid);
        if(existingRole == null)
        {
            return NotFound();
        }
        _context.Update(existingRole);
        await _context.SaveChangesAsync();
        return Ok(existingRole);
    }

    [HttpPost("CreateRole")]
    public async Task<ActionResult<Role>> AddRole(Role role)
    {
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetRoleById", new { roleid = role.RoleId }, role);
    }

    [HttpDelete("DeleteRole/{roleid}")]
    public async Task<IActionResult> DeleteRole(int? roleid)
    {
        var role = await _context.Roles.FindAsync(roleid);
        if (role == null)
        {
            return NotFound();
        }

        _context.Roles.Remove(role);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

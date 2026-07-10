using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkTeamProject.API.Models;
using WorkTeamProject.API.Data;
using WorkTeamProject.API.Repositories;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IRoleRepository _roleRepository;

    public RoleController(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    [HttpGet("GetRoles")]
    public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
    {
        return Ok(await _roleRepository.GetRoles());
    }

    [HttpGet("GetRoleById/{roleid}")]
    public async Task<ActionResult<Role>> GetRoleById(int roleid)
    {
        var role = await _roleRepository.GetRoleById(roleid);

        if (role == null)
        {
            return NotFound();
        }

        return Ok(role);
    }

    [HttpPut("UpdateRole/{roleid}")]
    public async Task<IActionResult> UpdateRole(int? roleid, Role role)
    {
        var existingRole = await _roleRepository.UpdateRole(roleid, role);
        if(existingRole)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost("CreateRole")]
    public async Task<ActionResult<Role>> AddRole(Role role)
    {
        await _roleRepository.AddRole(role);

        return CreatedAtAction("GetRoleById", new { roleid = role.RoleId }, role);
    }

    [HttpDelete("DeleteRole/{roleid}")]
    public async Task<IActionResult> DeleteRole(int? roleid)
    {
        var role = await _roleRepository.DeleteRole(roleid);
        if (role)
        {
            return NotFound();
        }

        return NoContent();
    }
}

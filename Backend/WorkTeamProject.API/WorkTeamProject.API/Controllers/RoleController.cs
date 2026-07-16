using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkTeamProject.API.Models;
using WorkTeamProject.API.Data;
using Microsoft.AspNetCore.Authorization;
using WorkTeamProject.API.DTOs.Role;
using WorkTeamProject.API.Repositories.Role;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IRoleRepository _roleRepository;

    public RoleController(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    [Authorize(Roles = "Admin,Manager,User")]
    [HttpGet("GetRoles")]
    public async Task<ActionResult<IEnumerable<RoleResponseDTO>>> GetRoles()
    {
        return Ok(await _roleRepository.GetRoles());
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpGet("GetRoleById/{roleid}")]
    public async Task<ActionResult<RoleResponseDTO>> GetRoleById(int roleid)
    {
        var role = await _roleRepository.GetRoleById(roleid);

        if (role == null)
        {
            return NotFound();
        }

        return Ok(role);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("UpdateRole/{roleid}")]
    public async Task<IActionResult> UpdateRole(int? roleid, RoleRequestDTO role)
    {
        var existingRole = await _roleRepository.UpdateRole(roleid, role);
        if(existingRole)
        {
            return NotFound();
        }

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("CreateRole")]
    public async Task<ActionResult<RoleResponseDTO>> AddRole(RoleRequestDTO role)
    {
        var newRole = await _roleRepository.AddRole(role);

        return CreatedAtAction("GetRoleById", new { roleid = newRole.RoleId }, newRole);
    }

    [Authorize(Roles = "Admin")]
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

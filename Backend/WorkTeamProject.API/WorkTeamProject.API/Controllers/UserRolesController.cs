using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkTeamProject.API.DTOs.User;
using WorkTeamProject.API.DTOs.UserRole;
using WorkTeamProject.API.Models;
using WorkTeamProject.API.Repositories.UserRole;

namespace WorkTeamProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRolesController(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        [Authorize(Roles = "Admin,Manager")]
        // GET: api/UserRole
        [HttpGet("GetAllUserRoles")]
        public async Task<ActionResult<IEnumerable<UserResponseDTO>>> GetUsers()
        {
            return Ok(await _userRoleRepository.GetUserRoles());
        }

        [Authorize(Roles = "Admin,Manager")]
        // GET: api/UserRole/1/1
        [HttpGet("GetUserRoleById/{userId}/{roleId}")]
        public async Task<ActionResult<UserResponseDTO>> GetUserRole(int userId, int roleId)
        {
            var user = await _userRoleRepository.GetUserRole(userId, roleId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [Authorize(Roles = "Admin")]
        // PUT: api/UserRole/1
        [HttpPut("UpdateUserRole/{userId}/{roleId}")]
        public async Task<IActionResult> PutUserRole(int userId, int roleId, [FromBody] UserRoleRequestDTO userRole)
        {
            var existingUserRole = await _userRoleRepository.PutUserRole(userId, roleId, userRole);
            if (!existingUserRole)
            {
                return NotFound();
            }

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        // POST: api/UserRole
        [HttpPost("CreateUserRole")]
        public async Task<ActionResult<User>> PostUserRole([FromBody] UserRoleRequestDTO userRole)
        {
            var newUserRole = await _userRoleRepository.PostUserRole(userRole);

            return CreatedAtAction("GetUserRole", new { userId = newUserRole.UserId }, newUserRole);
        }

        [Authorize(Roles = "Admin")]
        // DELETE: api/UserRole/1
        [HttpDelete("DeleteUserRole/{userId}/{roleId}")]
        public async Task<IActionResult> DeleteUserRole(int userId, int roleId)
        {
            var userRole = await _userRoleRepository.DeleteUserRole(userId, roleId);
            if (!userRole)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

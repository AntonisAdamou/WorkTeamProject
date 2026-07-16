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
        // GET: api/UserRole/1
        [HttpGet("GetUserRoleById/{userId}")]
        public async Task<ActionResult<UserResponseDTO>> GetUserRole(int userId)
        {
            var user = await _userRoleRepository.GetUserRole(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [Authorize(Roles = "Admin")]
        // PUT: api/UserRole/1
        [HttpPut("UpdateUserRole/{userId}")]
        public async Task<IActionResult> PutUserRole(int userId, [FromBody] UserRoleRequestDTO userRole)
        {
            var existingUserRole = await _userRoleRepository.PutUserRole(userId, userRole);
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
        [HttpDelete("DeleteUserRole/{userId}")]
        public async Task<IActionResult> DeleteUserRole(int userId)
        {
            var userRole = await _userRoleRepository.DeleteUserRole(userId);
            if (!userRole)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

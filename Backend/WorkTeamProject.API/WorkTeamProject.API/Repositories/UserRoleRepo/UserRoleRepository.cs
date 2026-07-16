using Microsoft.EntityFrameworkCore;
using WorkTeamProject.API.Data;
using WorkTeamProject.API.DTOs.UserRoleDTO;
using WorkTeamProject.API.Models;

namespace WorkTeamProject.API.Repositories.UserRoleRepo
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly AppDbContext _context;
        public UserRoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserRoleResponseDTO>> GetUserRoles()
        {
            var userRoles = await _context.UserRoles.Include(ur => ur.User).Include(ur => ur.Role).ToListAsync();

            var results = new List<UserRoleResponseDTO>();
            foreach (var userRole in userRoles)
            {
                results.Add(new UserRoleResponseDTO
                {
                    UserId = userRole.UserId,
                    UserName = userRole.User!.UserName,
                    RoleId = userRole.RoleId,
                    RoleName = userRole.Role!.RoleName
                });
            }

            return results;
        }

        public async Task<UserRoleResponseDTO?> GetUserRole(int userId, int roleId)
        {
            var userRole = await _context.UserRoles.Include(ur => ur.User).Include(ur => ur.Role).FirstOrDefaultAsync(u => u.UserId == userId && u.RoleId == roleId);

            return userRole is null ? null : new UserRoleResponseDTO
            {
                UserId = userRole.UserId,
                UserName = userRole.User!.UserName,
                RoleId = userRole.RoleId,
                RoleName = userRole.Role!.RoleName
            };
        }

        public async Task<bool> PutUserRole(int userId, int roleId, UserRoleRequestDTO userRole)
        {
            var existingUserRole = await _context.UserRoles.FirstOrDefaultAsync(u => u.UserId == userId && u.RoleId == roleId);
            if (existingUserRole == null)
            {
                return false;
            }

            existingUserRole.UserId = userRole.UserId;
            existingUserRole.RoleId = userRole.RoleId;

            _context.UserRoles.Update(existingUserRole);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<UserRole> PostUserRole(UserRoleRequestDTO userRole)
        {
            var newUserRole = new UserRole
            {
                UserId = userRole.UserId,
                RoleId = userRole.RoleId
            };

            _context.UserRoles.Add(newUserRole);
            await _context.SaveChangesAsync();

            return newUserRole;
        }

        public async Task<bool> DeleteUserRole(int userId, int roleId)
        {
            var userRole = await _context.UserRoles.FirstOrDefaultAsync(u => u.UserId == userId && u.RoleId == roleId);
            if (userRole == null)
            {
                return false;
            }

            _context.UserRoles.Remove(userRole);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

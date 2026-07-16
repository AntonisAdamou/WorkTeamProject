
using Microsoft.EntityFrameworkCore;
using WorkTeamProject.API.Data;
using WorkTeamProject.API.DTOs.Role;
using WorkTeamProject.API.Models;

namespace WorkTeamProject.API.Repositories.Role
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;
        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoleResponseDTO>> GetRoles()
        {
            var roles = await _context.Roles.Include(r=> r.UserRoles!).ThenInclude(un=> un.User).ToListAsync();
            var results = new List<RoleResponseDTO>();
            foreach (var role in roles)
            {
                results.Add(new RoleResponseDTO{
                    RoleId= role.RoleId,
                    RoleName= role.RoleName,
                    UserName= role.UserRoles?.Select(ur => ur.User!.UserName).ToList()
                });
            }
            return results;
        }

        public async Task<RoleResponseDTO> GetRoleById(int roleid)
        {
            var role = await _context.Roles.Include(r => r.UserRoles!).ThenInclude(un => un.User).FirstOrDefaultAsync(ur=>ur.RoleId == roleid);

            if (role == null)
            {
                return null!;
            }

            return new RoleResponseDTO 
            { 
                RoleId = role.RoleId,
                RoleName = role.RoleName,
                UserName = role.UserRoles?.Select(ur => ur.User!.UserName).ToList()
            };
        }

        public async Task<bool> UpdateRole(int? roleid, RoleRequestDTO role)
        {
            var isNull = false;
            var existingRole = await _context.Roles.FindAsync(roleid);
            if (existingRole == null)
            {
                isNull = true;
                return isNull;
            }

            existingRole.RoleName = role.RoleName;

            _context.Update(existingRole);
            await _context.SaveChangesAsync();
            return isNull;
        }

        public async Task<RoleResponseDTO> AddRole(RoleRequestDTO role)
        {
            var newRole = new Role
            {
                RoleName = role.RoleName
            };
            _context.Roles.Add(newRole);
            await _context.SaveChangesAsync();

            return new RoleResponseDTO 
            {
                RoleId = newRole.RoleId,
                RoleName = newRole.RoleName
            };
        }

        public async Task<bool> DeleteRole(int? roleid)
        {
            var isNull = false;
            var role = await _context.Roles.FindAsync(roleid);
            if (role == null)
            {
                isNull = true;
                return isNull;
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return isNull;
        }
    }
}


using Microsoft.EntityFrameworkCore;
using WorkTeamProject.API.Data;
using WorkTeamProject.API.Models;

namespace WorkTeamProject.API.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;
        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetRoleById(int roleid)
        {
            var role = await _context.Roles.FindAsync(roleid);

            if(role == null)
            {
                return null!;
            }

            return role;
        }

        public async Task<bool> UpdateRole(int? roleid, Role role)
        {
            var isNull = false;
            var existingRole = await _context.Roles.FindAsync(roleid);
            if (existingRole == null)
            {
                isNull = true;
                return isNull;
            }

            existingRole.RoleName = role.RoleName;
            existingRole.UserRoles = role.UserRoles;

            _context.Update(existingRole);
            await _context.SaveChangesAsync();
            return isNull;
        }

        public async Task<Role> AddRole(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return role;
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

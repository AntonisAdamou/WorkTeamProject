
using WorkTeamProject.API.Models;

namespace WorkTeamProject.API.Repositories
{
    public interface IRoleRepository
    {
        public  Task<IEnumerable<Role>> GetRoles();
        public  Task<Role> GetRoleById(int roleid);
        public  Task<bool> UpdateRole(int? roleid, Role role);
        public  Task<Role> AddRole(Role role);
        public  Task<bool> DeleteRole(int? roleid);
    }
}

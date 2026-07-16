using WorkTeamProject.API.DTOs.Role;
using WorkTeamProject.API.Models;

namespace WorkTeamProject.API.Repositories.Role
{
    public interface IRoleRepository
    {
        public  Task<IEnumerable<RoleResponseDTO>> GetRoles();
        public  Task<RoleResponseDTO> GetRoleById(int roleid);
        public  Task<bool> UpdateRole(int? roleid, RoleRequestDTO role);
        public  Task<RoleResponseDTO> AddRole(RoleRequestDTO role);
        public  Task<bool> DeleteRole(int? roleid);
    }
}

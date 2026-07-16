using WorkTeamProject.API.DTOs.RoleDTO;

namespace WorkTeamProject.API.Repositories.RoleRepo
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

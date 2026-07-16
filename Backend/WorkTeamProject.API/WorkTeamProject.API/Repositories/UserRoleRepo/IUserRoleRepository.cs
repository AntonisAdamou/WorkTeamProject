using WorkTeamProject.API.DTOs.UserRoleDTO;
using WorkTeamProject.API.Models;

namespace WorkTeamProject.API.Repositories.UserRoleRepo
{
    public interface IUserRoleRepository
    {
        Task<IEnumerable<UserRoleResponseDTO>> GetUserRoles();
        Task<UserRoleResponseDTO?> GetUserRole(int userId, int roleId);
        Task<bool> PutUserRole(int userId, int roleId, UserRoleRequestDTO user);
        Task<UserRole> PostUserRole(UserRoleRequestDTO user);
        Task<bool> DeleteUserRole(int userId, int roleId);
    }
}

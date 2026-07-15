using WorkTeamProject.API.DTOs;
using WorkTeamProject.API.Models;

namespace WorkTeamProject.API.Repositories
{
    public interface IUserRoleRepository
    {
        Task<IEnumerable<UserRoleResponseDTO>> GetUserRoles();
        Task<UserRoleResponseDTO?> GetUserRole(int userId);
        Task<bool> PutUserRole(int userId, UserRoleRequestDTO user);
        Task<UserRole> PostUserRole(UserRoleRequestDTO user);
        Task<bool> DeleteUserRole(int userId);
    }
}

using WorkTeamProject.API.DTOs.User;
using WorkTeamProject.API.Models;

namespace WorkTeamProject.API.Repositories.User
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserResponseDTO>> GetUsers();
        Task<UserResponseDTO?> GetUser(int userId);
        Task<bool> PutUser(int userId, UserRequestDTO user);
        Task<User> PostUser(UserRequestDTO user);
        Task<bool> DeleteUser(int userId);
    }
}

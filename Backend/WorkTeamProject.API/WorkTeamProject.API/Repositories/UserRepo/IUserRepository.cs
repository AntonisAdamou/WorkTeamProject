using WorkTeamProject.API.DTOs.UserDTO;
using WorkTeamProject.API.Models;

namespace WorkTeamProject.API.Repositories.UserRepo
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

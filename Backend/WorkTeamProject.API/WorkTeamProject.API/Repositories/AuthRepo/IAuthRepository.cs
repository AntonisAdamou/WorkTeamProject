using WorkTeamProject.API.DTOs.AuthDTO;
using WorkTeamProject.API.Models;

namespace WorkTeamProject.API.Repositories.AuthRepo
{
    public interface IAuthRepository
    {
        Task<User> Register(RegisterRequestDTO register);
        Task<bool> Login(LoginRequestDTO login);
        Task<string> GenerateToken(string username);
    }
}

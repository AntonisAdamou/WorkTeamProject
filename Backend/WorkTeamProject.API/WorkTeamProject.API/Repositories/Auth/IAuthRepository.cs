using WorkTeamProject.API.DTOs.Auth;
using WorkTeamProject.API.Models;

namespace WorkTeamProject.API.Repositories.Auth
{
    public interface IAuthRepository
    {
        Task<User> Register(RegisterRequestDTO register);
        Task<bool> Login(LoginRequestDTO login);
        Task<string> GenerateToken(string username);
    }
}

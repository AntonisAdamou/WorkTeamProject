using WorkTeamProject.API.DTOs;
using WorkTeamProject.API.Models;

namespace WorkTeamProject.API.Repositories
{
    public interface IAuthRepository
    {
        Task<User> Register(RegisterRequestDTO register);
        Task<bool> Login(LoginRequestDTO login);
        Task<string> GenerateToken(string username);
    }
}

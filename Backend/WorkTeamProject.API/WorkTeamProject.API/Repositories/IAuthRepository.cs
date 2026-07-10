using WorkTeamProject.API.Models;

namespace WorkTeamProject.API.Repositories
{
    public interface IAuthRepository
    {
        public Task<string> Register(User user, string password);
        public Task<string> Login(string username, string password);
    }
}

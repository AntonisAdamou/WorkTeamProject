using WorkTeamProject.API.Data;
using WorkTeamProject.API.Models;

namespace WorkTeamProject.API.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;
        public AuthRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<string> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Register(User user, string password)
        {
            throw new NotImplementedException();
        }
    }
}

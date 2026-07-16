using Microsoft.EntityFrameworkCore;
using WorkTeamProject.API.Data;
using WorkTeamProject.API.DTOs.UserDTO;
using WorkTeamProject.API.Models;

namespace WorkTeamProject.API.Repositories.UserRepo
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserResponseDTO>> GetUsers()
        {
            var users = await _context.Users.Include(u => u.UserRoles!).ThenInclude(ur => ur.Role).ToListAsync();

            var results = new List<UserResponseDTO>();
            foreach (var user in users)
            {
                results.Add(new UserResponseDTO
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    UserEmail = user.UserEmail,
                    UserPassword = user.UserPassword,
                    Roles = user.UserRoles!.Select(ur => ur.Role!.RoleName).ToList()
                });
            }

            return results;
        }

        public async Task<UserResponseDTO?> GetUser(int userId)
        {
            var user = await _context.Users.Include(u => u.UserRoles!).ThenInclude(ur => ur.Role).FirstOrDefaultAsync(u => u.UserId == userId);

            return user is null ? null : new UserResponseDTO
            {
                UserId = user.UserId,
                UserName = user.UserName,
                UserEmail = user.UserEmail,
                UserPassword = user.UserPassword,
                Roles = user.UserRoles!.Select(ur => ur.Role!.RoleName).ToList()
            };
        }

        public async Task<bool> PutUser(int userId, UserRequestDTO user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (existingUser == null)
            {
                return false;
            }

            existingUser.UserName = user.UserName;
            existingUser.UserEmail = user.UserEmail;
            existingUser.UserPassword = user.UserPassword;

            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<User> PostUser(UserRequestDTO user)
        {
            var newUser = new User
            {
                UserName = user.UserName,
                UserEmail = user.UserEmail,
                UserPassword = user.UserPassword,
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return newUser;
        }

        public async Task<bool> DeleteUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

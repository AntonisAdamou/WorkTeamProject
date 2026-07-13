using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WorkTeamProject.API.Data;
using WorkTeamProject.API.DTOs;
using WorkTeamProject.API.Models;

namespace WorkTeamProject.API.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<User> Register(RegisterRequestDTO register)
        {
            var newUser = new User
            {
                UserName = register.UserName,
                UserEmail = register.UserEmail,
                UserPassword = register.UserPassword,
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return newUser;
        }

        public async Task<bool> Login(LoginRequestDTO login)
        {
            var user = await GetUser(login.UserEmail);
            if (user == null || user.UserPassword != login.UserPassword)
                return false;

            return true;
        }

        private async Task<UserResponseDTO?> GetUser(string userEmail)
        {
            var user = await _context.Users.Include(u => u.UserRoles!).ThenInclude(ur => ur.Role).FirstOrDefaultAsync(u => u.UserEmail == userEmail);

            return user is null ? null : new UserResponseDTO
            {
                UserId = user.UserId,
                UserName = user.UserName,
                UserEmail = user.UserEmail,
                UserPassword = user.UserPassword,
                Roles = user.UserRoles!.Select(ur => ur.Role!.RoleName).ToList()
            };
        }

        public async Task<string> GenerateToken(string username)
        {
            var user = await GetUser(username);
            if (user == null)
                return "";

            var role = user.Roles.First();

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

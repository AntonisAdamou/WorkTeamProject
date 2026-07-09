using WorkTeamProject.API.Models;

namespace WorkTeamProject.API.DTOs
{
    public class UserResponseDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new();
    }
}

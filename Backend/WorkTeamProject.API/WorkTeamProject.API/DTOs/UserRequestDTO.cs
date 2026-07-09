using WorkTeamProject.API.Models;

namespace WorkTeamProject.API.DTOs
{
    public class UserRequestDTO
    {
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;
    }
}

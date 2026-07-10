using WorkTeamProject.API.Models;

namespace WorkTeamProject.API.DTOs
{
    public class RoleResponseDTO
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public List<string>? UserName { get; set; }
    }
}
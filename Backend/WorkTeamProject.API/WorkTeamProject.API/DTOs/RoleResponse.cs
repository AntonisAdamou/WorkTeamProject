using WorkTeamProject.API.Models;

namespace WorkTeamProject.API.DTOs
{
    public class RoleResponse
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public ICollection<UserRole>? UserRoles { get; set; }
    }
}
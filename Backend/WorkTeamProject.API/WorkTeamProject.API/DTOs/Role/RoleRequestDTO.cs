using System.ComponentModel.DataAnnotations;

namespace WorkTeamProject.API.DTOs.Role
{
    public class RoleRequestDTO
    {
        [Required(ErrorMessage = "RoleName is required")]
        [Length(3, 15, ErrorMessage = "RoleName must be between 3 and 15 characters")]
        public string RoleName { get; set; } = string.Empty;
    }
}

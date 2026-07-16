using System.ComponentModel.DataAnnotations;

namespace WorkTeamProject.API.DTOs
{
    public class UserRoleRequestDTO
    {
        [Required(ErrorMessage = "UserId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be a positive integer.")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "RoleId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "RoleId must be a positive integer.")]
        public int RoleId { get; set; }
    }
}

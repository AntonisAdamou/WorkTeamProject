using System.ComponentModel.DataAnnotations;

namespace WorkTeamProject.API.DTOs
{
    public class UserRoleRequestDTO
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}

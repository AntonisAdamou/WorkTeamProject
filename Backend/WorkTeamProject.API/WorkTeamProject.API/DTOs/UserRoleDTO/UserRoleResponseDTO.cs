namespace WorkTeamProject.API.DTOs.UserRoleDTO
{
    public class UserRoleResponseDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
    }
}

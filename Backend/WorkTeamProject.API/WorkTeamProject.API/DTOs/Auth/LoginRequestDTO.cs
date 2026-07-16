using System.ComponentModel.DataAnnotations;

namespace WorkTeamProject.API.DTOs.Auth
{
    public class LoginRequestDTO
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "{0} is not a valid email address")]
        public string UserEmail { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required.")]
        [Length(5, 30, ErrorMessage = "Password length must be between {1} and {2}")]
        public string UserPassword { get; set; } = string.Empty;
    }
}

using System.ComponentModel.DataAnnotations;
using WorkTeamProject.API.Models;

namespace WorkTeamProject.API.DTOs
{
    public class UserRequestDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "{0} is not a valid email address")]
        public string UserEmail { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required.")]
        [Range(5, 30,ErrorMessage = "Password value({0}) must be between {1} {2}")]
        public string UserPassword { get; set; } = string.Empty;
    }
}

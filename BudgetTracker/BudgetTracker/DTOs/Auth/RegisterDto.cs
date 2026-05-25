using System.ComponentModel.DataAnnotations;

namespace BudgetTracker.DTOs.Auth
{
    public class RegisterDto
    {
        [Required]
        [StringLength(50, MinimumLength =2)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; } = string.Empty;
    }
}

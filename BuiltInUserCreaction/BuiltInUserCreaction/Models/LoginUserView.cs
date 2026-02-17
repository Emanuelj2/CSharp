using System.ComponentModel.DataAnnotations;

namespace BuiltInUserCreaction.Models
{
    public class LoginUserView
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}

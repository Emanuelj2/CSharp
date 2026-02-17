using System.ComponentModel.DataAnnotations;

namespace BuiltInUserCreaction.Models
{
    public class RegisterUserView
    {
        [Required]
        [EmailAddress]
        public string Email {  get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

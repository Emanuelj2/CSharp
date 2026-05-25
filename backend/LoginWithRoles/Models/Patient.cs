using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

namespace LoginWithRoles.Models
{
    public class Patient
    {

        public Guid Id { get; set; }
        public DateOnly DateOfBirth { get; set; }
        
        public string AppUserId { get; set; } = string.Empty;
        public AppUser AppUser { get; set; } = null!;
    }
}

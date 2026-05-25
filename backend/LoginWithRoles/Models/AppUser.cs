using Microsoft.AspNetCore.Identity;

namespace LoginWithRoles.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }



        public Patient Patient { get; set; }
        public InsuranceProvider InsuranceProvider { get; set; }

    }
}

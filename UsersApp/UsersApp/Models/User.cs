using Microsoft.AspNetCore.Identity;

namespace UsersApp.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }

    }
}

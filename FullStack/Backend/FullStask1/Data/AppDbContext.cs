using FullStask1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FullStask1.Data
{
    public class AppDbContext : IdentityDbContext<User> //note the change that is made if you add more feilds to the Identity class
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
    }
}

using LoginWithRoles.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace LoginWithRoles.Data
{
    public class ApplicationDbContext : IdentityDbContext <AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option) { }

        public DbSet<InsuranceProvider> insuranceProviders { get; set; }
        public DbSet<Patient> patients { get; set; }


        ///make the model connections

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // one user is one patient
            builder.Entity<AppUser>()
                .HasOne(u => u.Patient)
                .WithOne(p => p.AppUser)
                .HasForeignKey<Patient>(p => p.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            //one user to one insuranceprovider
            builder.Entity<AppUser>()
                .HasOne(u => u.InsuranceProvider)
                .WithOne(i => i.AppUser)
                .HasForeignKey<InsuranceProvider>(i => i.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);


        }
        
    }
}

using api_practice_1.Models.Organization;
using api_practice_1.Models.People;
using Microsoft.EntityFrameworkCore;

namespace api_practice_1.Data
{
    public class HospitalDbContext : DbContext
    {

        //this is the constructor for the HospitalDbContext class, which inherits from DbContext.
        //It takes in DbContextOptions<HospitalDbContext> as a parameter and passes it to the base
        //class constructor. This allows for configuration of the database context, such as
        //specifying the database provider and connection string.
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Department> Departments { get; set; }

    }
}

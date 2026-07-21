using api_practice_1.Models.People;

namespace api_practice_1.Models.Organization
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        public int? HeadEmployeeId { get; set; } // Nullable foreign key to the Employee table
        public Employee? Head { get; set; } // Navigation property to the Employee table

        public List<Employee> Employees { get; set; } = new List<Employee>(); // Navigation property to the Employee table

    }
}
 
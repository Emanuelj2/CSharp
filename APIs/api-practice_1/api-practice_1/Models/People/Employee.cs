using api_practice_1.Models.Organization;
using api_practice_1.Models.People.Enums;
using Microsoft.AspNetCore.Identity;

namespace api_practice_1.Models.People
{
    public class Employee : User
    {
        public int EmployeeId { get; set; }
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
        public string Gender { get; set; } = string.Empty;


        public  JobTitle Job { get; set; } = JobTitle.None;
        public EmploymentType EmploymentType { get; set; } = EmploymentType.FullTime;
        public AccessLevelType AccessLevel { get; set; } = AccessLevelType.Basic;


        //department relationship
        public int DepartmentId { get; set; } // Foreign key to the Department table
        public Department? Department { get; set; } // Navigation property to the Department table

        //optional properties
        public string Race { get; set; } = string.Empty;
        public string Ethnicity { get; set; } = string.Empty;
        public bool VeteranStatus { get; set; } = false;
        public Pronouns Pronoun { get; set; } = Pronouns.None;


    }
}

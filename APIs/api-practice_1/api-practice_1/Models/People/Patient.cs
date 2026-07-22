using api_practice_1.Models.Organization;
using api_practice_1.Models.People.PeopleEnums;
using Microsoft.AspNetCore.Routing.Constraints;

namespace api_practice_1.Models.People
{
    public class Patient : User
    {
        public int PatientId { get; set; }
        public string MedicalRecordNumber { get; set; } = string.Empty;


        //admision info
        public DateTime? AdmissionDate { get; set; }
        public DateTime? DischargeDate { get; set; }
        public PatientStatus Status { get; set; } = PatientStatus.None;


        //assign care
        public int? AssignedDoctorId { get; set; } // Nullable foreign key to the Employee table
        public Employee? AssignedDoctor { get; set; } // Navigation property to the Employee table


        public int? DepartmentId { get; set; } // Nullable foreign key to the Department table
        public Department? Department { get; set; }

        public string? RoomNumber { get; set; }


        //medical info
        public BloodType BloodType { get; set; } = BloodType.Unknown;
        public List<String> Allergies { get; set; } = new List<string>();
        public string? PrimaryDiagnosis { get; set; }


        //insruance
        public string InsuranceProvider { get; set; } = string.Empty;
        public string InsurancePolicyNumber { get; set; } = string.Empty;


        
        public List<Visitor> Visitors { get; set; } = new List<Visitor>();
    }
}

using api_practice_1.Models.People.PeopleEnums;

namespace api_practice_1.Models.People
{
    public class Visitor
    {
        public int VisitorId { get; set; }
        public RelationshipType Relationship { get; set; } = RelationshipType.None;// "Spouse", "Parent", "Friend"

        // Which patient(s) this visitor is associated with
        public List<Patient> VisitingPatients { get; set; } = new();

        // Visit tracking
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }

        // Emergency contact flag
        public bool IsEmergencyContact { get; set; } = false;
    }
}

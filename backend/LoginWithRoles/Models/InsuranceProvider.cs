namespace LoginWithRoles.Models
{
    public class InsuranceProvider
    {
        public Guid Id { get; set; }
        public string Speciality { get; set; }
        public string CompanyName { get; set; }


        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

    }
}

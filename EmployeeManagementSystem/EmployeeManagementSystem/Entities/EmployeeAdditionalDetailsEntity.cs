using EmployeeManagementSystem.Common;

namespace EmployeeManagementSystem.Entities
{
    public class EmployeeAdditionalDetailsEntity:BaseEntity
    {
        public string EmployeeBasicDetailsUId { get; set; }
        public string AlternateEmail { get; set; }
        public string AlternateMobile { get; set; }
        public WorkInfo_ WorkInformation { get; set; }
        public PersonalDetails_ PersonalDetails { get; set; }
        public IdentityInfo_ IdentityInformation { get; set; }
    }
}

using EmployeeManagementSystem.Entities;
using Newtonsoft.Json;

namespace EmployeeManagementSystem.DTO
{
    public class EmployeeAdditionalDetailsDTO
    {
        [JsonProperty("employeeBasicDetailsUId")]
        public string EmployeeBasicDetailsUId { get; set; }

        [JsonProperty("alternateEmail")]
        public string AlternateEmail { get; set; }

        [JsonProperty("alternateMobile")]
        public string AlternateMobile { get; set; }

        [JsonProperty("workInformation")]
        public WorkInfo_ WorkInformation { get; set; }

        [JsonProperty("personalDetails")]
        public PersonalDetails_ PersonalDetails { get; set; }

        [JsonProperty("identityInformation")]
        public IdentityInfo_ IdentityInformation { get; set; }
    }

    public class AdvanceFilterCriteria
    {
        public AdvanceFilterCriteria() {
            Filters = new List<FIlterCriteria>();
            EmployeeAdditionalDetails = new List<EmployeeAdditionalDetailsDTO>();
        }
        public int Pange { get; set; }
        public int Count { get; set; }
        public int PageSize { get; set; }

        public List<FIlterCriteria> Filters;
        public List<EmployeeAdditionalDetailsDTO> EmployeeAdditionalDetails { get; set; }
    }

    public class FIlterCriteria
    {
        public string FieldName { get; set; }
        public List<string> FieldValue { get; set; }
    }
}

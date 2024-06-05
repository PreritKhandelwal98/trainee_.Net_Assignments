using Newtonsoft.Json;

namespace VisitorSecurityClearanceSystem.DTO
{
    public class VisitorDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("companyName")]
        public string CompanyName { get; set; }

        [JsonProperty("purpose")]
        public string Purpose { get; set; }

        [JsonProperty("entryTime")]
        public DateTime EntryTime { get; set; }

        [JsonProperty("exitTime")]
        public DateTime ExitTime { get; set; }

        [JsonProperty("passStatus")]
        public bool PassStatus { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }
    }
}

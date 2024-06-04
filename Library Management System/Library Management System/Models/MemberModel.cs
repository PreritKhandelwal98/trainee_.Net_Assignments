using Newtonsoft.Json;

namespace Library_Management_System.Models
{
    public class MemberModel
    {
        [JsonProperty("uId")]
        public string UId { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("contactNumber")]
        public int ContactNumber { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("dateOfBirth")]
        public DateTime DateOfBirth { get; set; }
    }
}

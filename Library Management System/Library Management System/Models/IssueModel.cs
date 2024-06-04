using Newtonsoft.Json;

namespace Library_Management_System.Models
{
    public class IssueModel
    {
        [JsonProperty("uId")]
        public string UId { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("bookId")]
        public string BookId { get; set; }

        [JsonProperty("memberId")]
        public string MemberId { get; set; }

        [JsonProperty("issueDate")]
        public DateTime IssueDate { get; set; }

        [JsonProperty("returnDate")]
        public DateTime ReturnDate { get; set; }

        [JsonProperty("isReturned")]
        public bool IsReturned { get; set; }

        public BookModel BookDetails { get; set; }


    }
}

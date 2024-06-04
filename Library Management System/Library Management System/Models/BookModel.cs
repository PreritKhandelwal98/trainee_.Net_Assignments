using Newtonsoft.Json;

namespace Library_Management_System.Models
{
    public class BookModel
    {
        [JsonProperty("uId")]
        public string UId { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("publishedDate")]
        public DateTime PublishedDate { get; set; }

        [JsonProperty("isbn")]
        public string ISBN { get; set; }

        [JsonProperty("isIssued")]
        public bool IsIssued { get; set; }
    }
}

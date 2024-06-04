using Newtonsoft.Json;

namespace Retail_Shop.DTO
{
    public class RetailShopDTO
    {
        [JsonProperty("uid")]
        public string UId { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("ownerName")]
        public string OwnerName { get; set; }

        [JsonProperty("contactNumber")]
        public string ContactNumber { get; set; }

    }
}

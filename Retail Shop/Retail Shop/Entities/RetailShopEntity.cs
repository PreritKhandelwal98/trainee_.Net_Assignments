using Newtonsoft.Json;
using Retail_Shop.Common;

namespace Retail_Shop.Entities
{
    public class RetailShopEntity : BaseEntity
    {
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "location", NullValueHandling = NullValueHandling.Ignore)]
        public string Location { get; set; }

        [JsonProperty(PropertyName = "ownerName", NullValueHandling = NullValueHandling.Ignore)]
        public string OwnerName { get; set; }

        [JsonProperty(PropertyName = "contactNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string ContactNumber { get; set; }

    }
}

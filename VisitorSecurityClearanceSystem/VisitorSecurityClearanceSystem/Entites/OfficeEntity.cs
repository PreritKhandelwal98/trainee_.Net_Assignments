﻿using Newtonsoft.Json;
using VisitorSecurityClearanceSystem.Common;

namespace VisitorSecurityClearanceSystem.Entites
{
    public class OfficeEntity:BaseEntity
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public int Name { get; set; }

        [JsonProperty(PropertyName = "phone", NullValueHandling = NullValueHandling.Ignore)]
        public int Phone { get; set; }

        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "role", NullValueHandling = NullValueHandling.Ignore)]
        public string Role { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Splitt.Api.Net.Data
{
    public class MerchantRequest
    {
        [JsonProperty("kvk")]
        public string KVK;
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("address")]
        public string Address;
        [JsonProperty("phone")]
        public string Phone;
        [JsonProperty("email")]
        public string Email;
    }
    public class MerchantResponse
    {
        [JsonProperty("id")]
        public string Id;
        [JsonProperty("kvk")]
        public string KVK;
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("address")]
        public string Address;
        [JsonProperty("phone")]
        public string Phone;
        [JsonProperty("email")]
        public string Email;
        [JsonProperty("app_id")]
        public string AppId;
        [JsonProperty("status")]
        public string Status;
        [JsonProperty("keys")]
        public List<string> Keys;
    }
}
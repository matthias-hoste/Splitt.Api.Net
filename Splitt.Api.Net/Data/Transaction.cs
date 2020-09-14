using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Splitt.Api.Net.Data
{
    public class TransactionRequest
    {
        [JsonProperty("amount")]
        public int Amount;//in euro cents
    }
    public class TransactionResponse
    {
        [JsonProperty("id")]
        public string Id;
        [JsonProperty("amount")]
        public int Amount;
        [JsonProperty("tip_amount")]
        public int TipAmount;
        [JsonProperty("code")]
        public string Code;
        [JsonProperty("status")]
        public string Status;
        [JsonProperty("qr")]
        public string QR;
        [JsonProperty("url")]
        public string Url;
    }
}
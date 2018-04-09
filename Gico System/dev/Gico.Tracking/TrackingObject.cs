using System;
using Newtonsoft.Json;

namespace Gico.Tracking
{
    public class TrackingObject
    {
        [JsonProperty("@timestamp")]
        public DateTime Timestamp { get; set; }
        [JsonProperty("level")]
        public string Level { get; set; }
        [JsonProperty("messageTemplate")]
        public string MessageTemplate { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
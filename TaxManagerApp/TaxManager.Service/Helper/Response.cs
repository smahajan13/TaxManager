using System.Collections.Generic;
using Newtonsoft.Json;

namespace TaxManager.Service
{
    public class Response
    {
        [JsonProperty(PropertyName = "data")]
        public object Data { get; set; }

        [JsonProperty(PropertyName = "status")]
        public int Status { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "errors")]
        public List<string> Errors { get; set; }
    }
}

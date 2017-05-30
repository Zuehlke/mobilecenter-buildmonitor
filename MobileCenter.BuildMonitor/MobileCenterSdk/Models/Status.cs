using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenterSdk.Models
{
    public class McStatus
    {
        [JsonProperty(PropertyName = "error")]
        public McError Error { get; set; }
    }

    public class McError
    {
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
    public class McMessage
    {
        [JsonProperty(PropertyName = "message")]
        public string Content { get; set; }
    }
}

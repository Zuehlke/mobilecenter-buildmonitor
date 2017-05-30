using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenterSdk.Models
{
    public class McPermissionData
    {
        [JsonProperty(PropertyName = "permissions")]
        public List<string> Permissions { get; set; }
    }
}

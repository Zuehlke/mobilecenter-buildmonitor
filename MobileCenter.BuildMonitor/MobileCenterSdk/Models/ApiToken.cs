using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileCenterSdk.Services;
using System.Threading;

namespace MobileCenterSdk.Models
{
    public class McApiToken : McApiTokenBase, IAccountServiceHolder
    {
        internal McApiToken() { }
        [JsonIgnore]
        AccountService IAccountServiceHolder.AccountService { get; set; }

        [JsonProperty(PropertyName = "api_token")]
        public string Token { get; set; }

        public async Task DeleteAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await (this as IAccountServiceHolder).AccountService.DeleteApiTokenAsync(Id, cancellationToken);
        }
    }
    public class McApiTokenBase : McApiTokenInformation
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }
    }
    public class McApiTokenInformation
    {
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "scope")]
        public List<string> Scope { get; set; }
    }
}

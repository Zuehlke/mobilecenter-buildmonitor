using MobileCenterSdk.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenterSdk.Models
{
    public class McRepositoryConfig
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "repo_url")]
        public string RepoUrl { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonIgnore]
        public McRepoState StateType
        {
            get { return StringToRepoStateTypeConverter.Convert(State); }
            set { State = StringToRepoStateTypeConverter.ConvertBack(value); }
        }
    }
    public class McRepositoryInfo
    {
        [JsonProperty(PropertyName = "repo_url")]
        public string RepoUrl { get; set; }
    }


    public class McSourceRepository
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public object Description { get; set; }

        [JsonProperty(PropertyName = "private")]
        public bool IsPrivate { get; set; }

        [JsonProperty(PropertyName = "owner")]
        public McOwner Owner { get; set; }

        [JsonProperty(PropertyName = "clone_url")]
        public string CloneUrl { get; set; }
    }

    public class McSourceRepositoryOwner
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "login")]
        public string Login { get; set; }
    }

    public class McXcodeVersion
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "current")]
        public bool IsCurrent { get; set; }
    }


    public enum McRepoState
    {
        Unauthorized,
        Inactive,
        Active,
        Unknown
    }
    public enum McSourceHost
    {
        GitHub,
        Bitbucket,
        Vsts,
        Unknown
    }
}

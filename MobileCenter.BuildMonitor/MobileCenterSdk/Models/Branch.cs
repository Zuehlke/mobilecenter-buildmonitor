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

    public class McBranchStatus
    {
        [JsonProperty(PropertyName = "branch")]
        public McBranch Branch { get; set; }

        [JsonProperty(PropertyName = "configured")]
        public bool IsConfigured { get; set; }

        [JsonProperty(PropertyName = "lastBuild")]
        public McBuild LastBuild { get; set; }

        [JsonProperty(PropertyName = "trigger")]
        public string Trigger { get; set; }
    }

    public class McBranch : IBuildServiceHolder, IAppDataHolder
    {
        BuildService IBuildServiceHolder.BuildService { get; set; }

        string IAppDataHolder.AppName { get; set; }
        string IAppDataHolder.AppOwnerName { get; set; }

        internal McBranch() { }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "commit")]
        public McCommitBase Commit { get; set; }

        public async Task<McBuild> CreateBuildAsync(McBuildParams buildParams, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await (this as IBuildServiceHolder).BuildService.CreateBranchBuildAsync(DataHolder().AppOwnerName, DataHolder().AppName, Name, buildParams, cancellationToken);
        }
        public async Task<List<McBuild>> GetBuildsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await (this as IBuildServiceHolder).BuildService.GetBranchBuildsAsync(DataHolder().AppOwnerName, DataHolder().AppName, Name, cancellationToken);
        }
        public async Task<McBranchConfiguration> ReconfigureForBuildAsync(McToolsetProjectConfiguration configuration, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await (this as IBuildServiceHolder).BuildService.ReconfigureBranchAsync(DataHolder().AppOwnerName, DataHolder().AppName, Name, configuration, cancellationToken);
        }
        public async Task<McBranchConfiguration> ConfigureForBuildAsync(McToolsetProjectConfiguration configuration, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await (this as IBuildServiceHolder).BuildService.ConfigureBranchAsync(DataHolder().AppOwnerName, DataHolder().AppName, Name, configuration, cancellationToken);
        }
        public async Task<McBranchConfiguration> GetConfigurationAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await (this as IBuildServiceHolder).BuildService.GetBranchConfigurationAsync(DataHolder().AppOwnerName, DataHolder().AppName, Name, cancellationToken);
        }
        public async Task<string> DeleteConfigurationAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await (this as IBuildServiceHolder).BuildService.DeleteBranchConfigurationAsync(DataHolder().AppOwnerName, DataHolder().AppName, Name, cancellationToken);
        }
        public async Task<McToolsetProjects> GetProjectsAsync(McAppOs os, McAppPlatform platform, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await (this as IBuildServiceHolder).BuildService.GetProjectsForBranchAsync(DataHolder().AppOwnerName, DataHolder().AppName, Name, platform, os, cancellationToken);
        }
        private IAppDataHolder DataHolder()
        {
            return this as IAppDataHolder;
        }
    }

    public class McCommitBase
    {
        [JsonProperty(PropertyName = "sha")]
        public string Sha { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }
    public class McCommitDetail : McCommitBase
    {
        [JsonProperty(PropertyName = "commit")]
        public McCommit Commit { get; set; }
    }

    public class McCommit
    {
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "author")]
        public McAuthor Author { get; set; }
    }

    public class McAuthor
    {
        [JsonProperty(PropertyName = "date")]
        public string CommitDate { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
    }


}

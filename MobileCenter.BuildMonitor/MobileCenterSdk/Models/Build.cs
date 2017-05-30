using MobileCenterSdk.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MobileCenterSdk.Models
{
    public class McBuild : IBuildServiceHolder, IAppDataHolder
    {
        BuildService IBuildServiceHolder.BuildService { get; set; }

        string IAppDataHolder.AppName { get; set; }
        string IAppDataHolder.AppOwnerName { get; set; }

        internal McBuild() { }
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "buildNumber")]
        public string BuildNumber { get; set; }

        [JsonProperty(PropertyName = "queueTime")]
        public DateTime QueueTime { get; set; }

        [JsonProperty(PropertyName = "startTime")]
        public DateTime StartTime { get; set; }

        [JsonProperty(PropertyName = "finishTime")]
        public DateTime FinishTime { get; set; }

        [JsonProperty(PropertyName = "lastChangedDate")]
        public DateTime LastChangedDate { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "result")]
        public string Result { get; set; }

        [JsonProperty(PropertyName = "reason")]
        public string Reason { get; set; }

        [JsonProperty(PropertyName = "sourceBranch")]
        public string SourceBranch { get; set; }

        [JsonProperty(PropertyName = "sourceVersion")]
        public string SourceVersion { get; set; }

        [JsonProperty(PropertyName = "tags")]
        public List<string> Tags { get; set; }

        public async Task<McBuild> CancelAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await (this as IBuildServiceHolder).BuildService.CancelBuildAsync(DataHolder().AppOwnerName, DataHolder().AppName, Id.ToString(), cancellationToken);
        }
        public async Task<McDistributionResponse> DistributeAsync(McDistributionInformation distributionInfo, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await (this as IBuildServiceHolder).BuildService.DistributeBuildAsync(DataHolder().AppOwnerName, DataHolder().AppName, Id.ToString(), distributionInfo, cancellationToken);
        }
        public async Task<McDownloadContainer> GetDownloadInformationAsync(McDownloadType downloadType, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await (this as IBuildServiceHolder).BuildService.GetBuildDownloadInformationAsync(DataHolder().AppOwnerName, DataHolder().AppName, Id.ToString(), downloadType, cancellationToken);
        }
        public async Task<McBuildLog> GetLogAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await (this as IBuildServiceHolder).BuildService.GetBuildLogsAsync(DataHolder().AppOwnerName, DataHolder().AppName, Id.ToString(), cancellationToken);
        }
        private IAppDataHolder DataHolder()
        {
            return this as IAppDataHolder;
        }
    }
    public class McBuildParams
    {
        [JsonProperty(PropertyName = "sourceVersion")]
        public string SourceVersion { get; set; }

        [JsonProperty(PropertyName = "debug")]
        public bool IsDebug { get; set; }

    }
    public class McBuildServiceStatusResponse
    {
        [JsonProperty(PropertyName = "service")]
        public string Service { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "valid_until")]
        public string ValidUntil { get; set; }
    }

    public class McDistributionInformation
    {
        [JsonProperty(PropertyName = "distributionGroupId")]
        public string DistributionGroupId { get; set; }

        [JsonProperty(PropertyName = "releaseNotes")]
        public string ReleaseNotes { get; set; }
    }

    public class McDistributionResponse
    {
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "upload_id")]
        public string UploadId { get; set; }
    }
    public class McDownloadContainer
    {
        [JsonProperty(PropertyName = "uri")]
        public string DownloadUri { get; set; }
    }
    public class McBuildLog
    {
        [JsonProperty(PropertyName = "value")]
        public List<string> Value;
    }

    public enum McDownloadType
    {
        Build,
        Symbols,
        Logs,
        TestReportPreview,
        Unknown
    }
}

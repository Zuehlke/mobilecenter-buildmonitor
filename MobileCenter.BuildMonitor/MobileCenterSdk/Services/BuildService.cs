using MobileCenterSdk.Models;
using MobileCenterSdk.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MobileCenterSdk.Services
{
    public class BuildService : ServiceBase
    {
        public BuildService(MobileCenterSdkClient mcsc) : base(mcsc) { }
        public async Task<List<McBranchStatus>> GetBranchesAsync(string ownerName, string appName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppBranchesEndpoint, ownerName, appName),
                HttpMethod.Get);
            var branches = await SendRequest<List<McBranchStatus>>(request, cancellationToken);
            branches.ForEach((branchStatus) => branchStatus.Branch = AddAppDataToT(branchStatus.Branch, ownerName, appName));
            return branches;
        }
        public async Task<List<McBuild>> GetBranchBuildsAsync(string ownerName, string appName, string branchName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppBranchBuildsEndpoint, ownerName, appName, branchName),
                HttpMethod.Get);
            var builds = await SendRequest<List<McBuild>>(request, cancellationToken);
            return builds.Select(build => AddAppDataToT(build, ownerName, appName)).ToList();
        }
        public async Task<McBuild> CreateBranchBuildAsync(string ownerName, string appName, string branchName, McBuildParams buildParams, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppBranchBuildsEndpoint, ownerName, appName, branchName),
                HttpMethod.Post,
                body: buildParams);
            return AddAppDataToT(await SendRequest<McBuild>(request, cancellationToken), ownerName, appName);
        }
        public async Task<McBranchConfiguration> ReconfigureBranchAsync(string ownerName, string appName, string branchName, McToolsetProjectConfiguration toolsetConfig, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppBranchConfigEndpoint, ownerName, appName, branchName),
                HttpMethod.Put,
                body: toolsetConfig);
            return await SendRequest<McBranchConfiguration>(request, cancellationToken);
        }
        public async Task<McBranchConfiguration> ConfigureBranchAsync(string ownerName, string appName, string branchName, McToolsetProjectConfiguration toolsetConfig, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppBranchConfigEndpoint, ownerName, appName, branchName),
                HttpMethod.Post,
                body: toolsetConfig);
            return await SendRequest<McBranchConfiguration>(request, cancellationToken);
        }
        public async Task<McBranchConfiguration> GetBranchConfigurationAsync(string ownerName, string appName, string branchName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppBranchConfigEndpoint, ownerName, appName, branchName),
                HttpMethod.Get);
            return await SendRequest<McBranchConfiguration>(request, cancellationToken);
        }
        public async Task<string> DeleteBranchConfigurationAsync(string ownerName, string appName, string branchName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppBranchConfigEndpoint, ownerName, appName, branchName),
                HttpMethod.Delete);
            var resultMessage = await SendRequest<McMessage>(request, cancellationToken);
            return resultMessage == null ? string.Empty : resultMessage.Content;
        }
        public async Task<McToolsetProjects> GetProjectsForBranchAsync(string ownerName, string appName, string branchName, McAppPlatform platform, McAppOs os, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppBranchToolsetEndpoint, ownerName, appName, branchName),
                HttpMethod.Get,
                new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("os", StringToOsTypeConverter.ConvertBack(os)),
                    new KeyValuePair<string, string>("platform", StringToPlatformTypeConverter.ConvertBack(platform))
                });
            return await SendRequest<McToolsetProjects>(request, cancellationToken);
        }
        public async Task<McBuildServiceStatusResponse> GetBuildServiceStatusAsync(string ownerName, string appName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppBuildServiceStatusEndpoint, ownerName, appName),
                HttpMethod.Get);
            return await SendRequest<McBuildServiceStatusResponse>(request, cancellationToken);
        }
        public async Task<McBuild> CancelBuildAsync(string ownerName, string appName, string buildId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppBuildEndpoint, ownerName, appName, buildId),
                ApiSettings.HttpMethodPatch);
            return AddAppDataToT(await SendRequest<McBuild>(request, cancellationToken), ownerName, appName);
        }
        public async Task<McBuild> GetBuildAsync(string ownerName, string appName, string buildId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppBuildEndpoint, ownerName, appName, buildId),
                HttpMethod.Get);
            return AddAppDataToT(await SendRequest<McBuild>(request, cancellationToken), ownerName, appName);
        }
        public async Task<McDistributionResponse> DistributeBuildAsync(string ownerName, string appName, string buildId, McDistributionInformation distributionInfo, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppBuildDistributeEndpoint, ownerName, appName, buildId),
                HttpMethod.Post,
                body: distributionInfo);
            return await SendRequest<McDistributionResponse>(request, cancellationToken);
        }
        public async Task<McDownloadContainer> GetBuildDownloadInformationAsync(string ownerName, string appName, string buildId, McDownloadType downloadType, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppBuildDownloadsEndpoint, ownerName, appName, buildId, StringToDownloadTypeConverter.ConvertBack(downloadType)),
                HttpMethod.Get);
            return await SendRequest<McDownloadContainer>(request, cancellationToken);
        }
        public async Task<McBuildLog> GetBuildLogsAsync(string ownerName, string appName, string buildId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppBuildLogEndpoint, ownerName, appName, buildId),
                HttpMethod.Get);
            return await SendRequest<McBuildLog>(request, cancellationToken);
        }
        public async Task<List<McCommitDetail>> GetCommitInformationForShasAsync(string ownerName, string appName, List<string> shaList, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppCommitsEndpoint, ownerName, appName),
                HttpMethod.Get, new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("hashes", string.Join(",", shaList))
                });
            return await SendRequest<List<McCommitDetail>>(request, cancellationToken);
        }
        public async Task<McMessage> ConfigureRepoForBuildAsync(string ownerName, string appName, string repoUrl, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppRepoConfigEndpoint, ownerName, appName),
                HttpMethod.Post,
                body: new McRepositoryInfo() { RepoUrl = repoUrl });
            return await SendRequest<McMessage>(request, cancellationToken);
        }
        public async Task<List<McRepositoryConfig>> GetRepoBuildConfigurationAsync(string ownerName, string appName, bool includeInactive, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppRepoConfigEndpoint, ownerName, appName),
                HttpMethod.Get,
                new List<KeyValuePair<string, string>>() {
                    new KeyValuePair<string, string>("includeInactive", includeInactive.ToString().ToLower())
                });
            return await SendRequest<List<McRepositoryConfig>>(request, cancellationToken);
        }
        public async Task<McMessage> RemoveRepoBuildConfigurationAsync(string ownerName, string appName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppRepoConfigEndpoint, ownerName, appName),
                HttpMethod.Delete);
            return await SendRequest<McMessage>(request, cancellationToken);
        }
        public async Task<List<McSourceRepository>> GetReposAvailableFromSourceControlAsync(string ownerName, string appName, McSourceHost sourceHost, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppSourceHostsEndpoint, ownerName, appName, StringToSourceHostConverter.ConvertBack(sourceHost)),
                HttpMethod.Get);
            return await SendRequest<List<McSourceRepository>>(request, cancellationToken);
        }
        public async Task<List<McXcodeVersion>> GetAvailableXcodeVersionsAsync(string ownerName, string appName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppXcodeVersionEndpoint, ownerName, appName),
                HttpMethod.Get);
            return await SendRequest<List<McXcodeVersion>>(request, cancellationToken);
        }
        private T AddAppDataToT<T>(T appInvite, string appOwnerName, string appName) where T : IAppDataHolder
        {
            if (appInvite == null)
                return default(T);
            appInvite.AppName = appName;
            appInvite.AppOwnerName = appOwnerName;
            if (appInvite is IBuildServiceHolder)
            {
                if ((appInvite as IBuildServiceHolder).BuildService == null)
                    (appInvite as IBuildServiceHolder).BuildService = this;
            }
            return appInvite;
        }
    }
}

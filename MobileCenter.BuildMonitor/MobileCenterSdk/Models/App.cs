using MobileCenterSdk.Services;
using MobileCenterSdk.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MobileCenterSdk.Models
{

    public class McApp : McAppSlim, IAccountServiceHolder, IBuildServiceHolder
    {
        internal McApp() { }
        [JsonIgnore]
        AccountService IAccountServiceHolder.AccountService { get; set; }
        BuildService IBuildServiceHolder.BuildService { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "app_secret")]
        public string AppSecret { get; set; }

        [JsonProperty(PropertyName = "azure_subscription_id")]
        public string AzureSubscriptionId { get; set; }

        [JsonProperty(PropertyName = "icon_url")]
        public string IconUrl { get; set; }
        
        [JsonProperty(PropertyName = "member_permissions")]
        public List<string> MemberPermissions { get; set; } = new List<string>();
        
        [JsonProperty(PropertyName = "owner")]
        public McOwner Owner { get; set; }
        
        [JsonProperty(PropertyName = "origin")]
        public string Origin { get; set; }


        [JsonIgnore]
        public McOrigin OriginType
        {
            get
            {
                return StringToOriginConverter.Convert(Origin);
            }
            set
            {
                Origin = StringToOriginConverter.ConvertBack(value);
            }
        }
        #region Account Methods
        public async Task<McApp> UpdateAsync(McAppBase appData, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await (this as IAccountServiceHolder).AccountService.UpdateAppAsync(Owner.Name, Name, appData, cancellationToken);
        }

        public async Task DeleteAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await (this as IAccountServiceHolder).AccountService.DeleteAppAsync(Owner.Name, Name, cancellationToken);
        }
        public async Task<McDistributionGroup> CreateDistributionGroupAsync(string dgname, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await (this as IAccountServiceHolder).AccountService.CreateDistributionGroup(Owner.Name, Name, dgname, cancellationToken);
        }
        public async Task<List<McDistributionGroup>> GetDistributionGroupsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await (this as IAccountServiceHolder).AccountService.GetDistributionGroups(Owner.Name, Name, cancellationToken);
        }
        public async Task<McDistributionGroup> GetDistributionGroupByNameAsync(string dgName, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await (this as IAccountServiceHolder).AccountService.GetDistributionGroup(Owner.Name, Name, dgName, cancellationToken);
        }
        public async Task InviteUserAsync(string email, CancellationToken cancellationToken = default(CancellationToken))
        {
            await (this as IAccountServiceHolder).AccountService.InviteUserToApp(Owner.Name, Name, email, cancellationToken);
        }
        public async Task<List<McAppInvitation>> GetPendingInvitationsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await (this as IAccountServiceHolder).AccountService.GetAppInvitations(Owner.Name, Name, cancellationToken);
        }
        public async Task<List<McUser>> GetTestersAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await (this as IAccountServiceHolder).AccountService.GetAppTesters(Owner.Name, Name, cancellationToken);
        }
        public async Task<McApp> TransferOwnershipAsync(string destinationOwnerName, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await (this as IAccountServiceHolder).AccountService.TransferAppOwnership(Owner.Name, Name, destinationOwnerName, cancellationToken);
        }
        public async Task<List<McAppUser>> GetUsersAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await (this as IAccountServiceHolder).AccountService.GetAppUsers(Owner.Name, Name, cancellationToken);
        }
        #endregion
        #region Build Methods
        public async Task<List<McBranchStatus>> GetBranchesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await (this as IBuildServiceHolder).BuildService.GetBranchesAsync(Owner.Name, Name, cancellationToken);
        }
        public async Task<McBuildServiceStatusResponse> GetBuildServiceStatusAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await (this as IBuildServiceHolder).BuildService.GetBuildServiceStatusAsync(Owner.Name, Name, cancellationToken);
        }
        public async Task<McMessage> ConfigureRespository(string repoUrl, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await (this as IBuildServiceHolder).BuildService.ConfigureRepoForBuildAsync(Owner.Name, Name, repoUrl, cancellationToken);
        }
        public async Task<List<McRepositoryConfig>> GetRepositoryBuildConfiguration(bool includeInactive, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await (this as IBuildServiceHolder).BuildService.GetRepoBuildConfigurationAsync(Owner.Name, Name, includeInactive, cancellationToken);
        }
        public async Task<McMessage> RemoveRepositoryBuildConfiguration(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await (this as IBuildServiceHolder).BuildService.RemoveRepoBuildConfigurationAsync(Owner.Name, Name, cancellationToken);
        }
        public async Task<List<McSourceRepository>> GetReposAvailableFromSourceControl(McSourceHost sourceHost, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await (this as IBuildServiceHolder).BuildService.GetReposAvailableFromSourceControlAsync(Owner.Name, Name, sourceHost, cancellationToken);
        }
        public async Task<List<McXcodeVersion>> GetAvailableXcodeVersions(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await (this as IBuildServiceHolder).BuildService.GetAvailableXcodeVersionsAsync(Owner.Name, Name, cancellationToken);
        }
        public async Task<McBuild> GetBuildByIdAsync(string id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await (this as IBuildServiceHolder).BuildService.GetBuildAsync(Owner.Name, Name, id, cancellationToken);
        }
        public async Task<List<McCommitDetail>> GetBuildByIdAsync(List<string> shas, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await (this as IBuildServiceHolder).BuildService.GetCommitInformationForShasAsync(Owner.Name, Name, shas, cancellationToken);
        }
        #endregion
    }
    public class McAppSlim : McAppBase
    {

        [JsonProperty(PropertyName = "os")]
        public string Os { get; set; }

        [JsonProperty(PropertyName = "platform")]
        public string Platform { get; set; }

        [JsonIgnore]
        public McAppOs OsType
        {
            get
            {
                return StringToOsTypeConverter.Convert(Os);
            }
            set
            {
                Os = StringToOsTypeConverter.ConvertBack(value);
            }
        }

        [JsonIgnore]
        public McAppPlatform PlatformType
        {
            get
            {
                return StringToPlatformTypeConverter.Convert(Platform);
            }
            set
            {
                Platform = StringToPlatformTypeConverter.ConvertBack(value);
            }
        }
        
    }
    public class McAppBase
    {
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
    public enum McAppPlatform
    {
        Cordova,
        Java,
        ObjectiveCSwift,
        ReactNative,
        Unity,
        UWP,
        Xamarin,
        Unknown
    }
    public enum McAppOs
    {
        Android,
        IOs,
        MacOs,
        Tizen,
        Windows,
        Custom,
        Unknown
    }
    internal interface IAppDataHolder
    {
        string AppName { get; set; }
        string AppOwnerName { get; set; }
    }
}

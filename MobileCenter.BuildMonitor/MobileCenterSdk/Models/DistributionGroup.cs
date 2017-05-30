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
    public class McDistributionGroup : McDistributionGroupBase, IAccountServiceHolder, IAppDataHolder
    {
        internal McDistributionGroup() { }
        [JsonIgnore]
        AccountService IAccountServiceHolder.AccountService { get; set; }

        [JsonIgnore]
        string IAppDataHolder.AppName { get; set; }

        [JsonIgnore]
        string IAppDataHolder.AppOwnerName { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

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

        private IAppDataHolder DataHolder()
        {
            return this as IAppDataHolder;
        }

        public async Task DeleteAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await (this as IAccountServiceHolder).AccountService.DeleteDistributionGroup(DataHolder().AppOwnerName, DataHolder().AppName, Name, cancellationToken);
        }
        public async Task<McDistributionGroup> UpdateAsync(string newName, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await (this as IAccountServiceHolder).AccountService.UpdateDistributionGroup(DataHolder().AppOwnerName, DataHolder().AppName, Name, newName, cancellationToken);
        }
        public async Task<List<McDistributionGroupInvitation>> InviteMembersAsync(McUsersWithEmailList users, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await (this as IAccountServiceHolder).AccountService.AddDistributionGroupMembers(DataHolder().AppOwnerName, DataHolder().AppName, Name, users, cancellationToken);
        }
        public async Task<List<McDistributionGroupUser>> GetMembersAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await (this as IAccountServiceHolder).AccountService.GetDistributionGroupMembers(DataHolder().AppOwnerName, DataHolder().AppName, Name, cancellationToken);
        }
        public async Task<List<McDistributionGroupInvitation>> DeleteMembersAsync(McUsersWithEmailList users, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await (this as IAccountServiceHolder).AccountService.RemoveDistributionGroupMembers(DataHolder().AppOwnerName, DataHolder().AppName, Name, users, cancellationToken);
        }
        
    }
    public class McDistributionGroupBase
    {

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}

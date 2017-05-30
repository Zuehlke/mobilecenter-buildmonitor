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
    public class McOrganizationInvitation : IAccountServiceHolder
    {
        internal McOrganizationInvitation() { }
        [JsonIgnore]
        AccountService IAccountServiceHolder.AccountService { get; set; }

        [JsonIgnore]
        internal string OrganizationName { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        public async Task ResendAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await (this as IAccountServiceHolder).AccountService.ResendInvitationToOrganizationAsync(OrganizationName, Email, cancellationToken);
        }
        public async Task DeleteAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await (this as IAccountServiceHolder).AccountService.RemoveInvitationToOrganization(OrganizationName, Email, cancellationToken);
        }
    }

    public class McDistributionGroupInvitation
    {
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "invite_pending")]
        public bool InvitePending { get; set; }

        [JsonProperty(PropertyName = "message")]
        public int Message { get; set; }

        [JsonProperty(PropertyName = "status")]
        public int Status { get; set; }

        [JsonProperty(PropertyName = "user_email")]
        public string UserEmail { get; set; }
    }
    public class McAppInvitation : IAccountServiceHolder, IAppDataHolder
    {
        internal McAppInvitation() { }
        [JsonIgnore]
        AccountService IAccountServiceHolder.AccountService { get; set; }

        [JsonIgnore]
        string IAppDataHolder.AppName { get; set; }

        [JsonIgnore]
        string IAppDataHolder.AppOwnerName { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "permissions")]
        public List<string> Permissions { get; set; }

        public async Task UpdateAsync(McPermissionData permissionData, CancellationToken cancellationToken = default(CancellationToken))
        {
            await (this as IAccountServiceHolder).AccountService.UpdateAppInvitationPermissions(DataHolder().AppOwnerName, DataHolder().AppName, Email, permissionData, cancellationToken);
        }
        public async Task DeleteAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await (this as IAccountServiceHolder).AccountService.DeleteAppInvitation(DataHolder().AppOwnerName, DataHolder().AppName, Email, cancellationToken);
        }

        private IAppDataHolder DataHolder()
        {
            return this as IAppDataHolder;
        }
    }

}

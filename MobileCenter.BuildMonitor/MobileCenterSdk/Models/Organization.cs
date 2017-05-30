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

    public class McOrganization : BaseOrganization, IAccountServiceHolder
    {
        [JsonIgnore]
        AccountService IAccountServiceHolder.AccountService { get; set; }


        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "collaborator_role")]
        public string CollaboratorRole { get; set; }

        [JsonProperty(PropertyName = "collaborator_count")]
        public int CollaboratorCount { get; set; }

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
        public async Task<McOrganization> UpdateAsync(BaseOrganization organization, CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckAccountService();
            return await (this as IAccountServiceHolder).AccountService.UpdateOrganizationAsync(Name, organization, cancellationToken);
        }
        public async Task DeleteAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckAccountService();
            await (this as IAccountServiceHolder).AccountService.DeleteOrganizationAsync(Name, cancellationToken);
        }
        public async Task<McApp> CreateAppAsync(McAppSlim app, CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckAccountService();
            return await (this as IAccountServiceHolder).AccountService.CreateAppInOrganizationAsync(Name, app, cancellationToken);
        }
        public async Task<List<McApp>> GetAppsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckAccountService();
            return await (this as IAccountServiceHolder).AccountService.GetAppsOfOrganizationAsync(Name, cancellationToken);
        }
        public async Task InviteUserAsync(string email, CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckAccountService();
            await (this as IAccountServiceHolder).AccountService.InviteUserToOrganizationAsync(Name, email, cancellationToken);
        }
        public async Task<List<McOrganizationInvitation>> GetPendingInvitationsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckAccountService();
            return await (this as IAccountServiceHolder).AccountService.GetPendingInvitationsOfOrganizationAsync(Name, cancellationToken);
        }
        public async Task DeleteInvitationAsync(string email, CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckAccountService();
            await (this as IAccountServiceHolder).AccountService.RemoveInvitationToOrganization(Name, email, cancellationToken);
        }
        public async Task ResendInvitationAsync(string email, CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckAccountService();
            await (this as IAccountServiceHolder).AccountService.ResendInvitationToOrganizationAsync(Name, email, cancellationToken);
        }
        public async Task<List<McOrganizationUser>> GetUsersAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckAccountService();
            return await (this as IAccountServiceHolder).AccountService.GetOrganizationUsersAsync(Name, cancellationToken);
        }
        private void CheckAccountService()
        {
            if ((this as IAccountServiceHolder).AccountService == null)
            {
                throw new UnauthorizedAccessException("Method can only be called on instances created by the SDK.");
            }
        }
    }

    public class BaseOrganization
    {
        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}

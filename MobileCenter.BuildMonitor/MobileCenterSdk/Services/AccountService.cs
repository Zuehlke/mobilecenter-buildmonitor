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
    public class AccountService : ServiceBase
    {
        public AccountService(MobileCenterSdkClient mcsc) : base(mcsc){}
        #region User
        /// <summary>
        /// Get the user profile data for the authenticated user as an <see cref="McUser"/> object asynchronously.
        /// </summary>
        /// <param name="cancellationToken">CancellationToken to cancel the request</param>
        /// <returns>Returns a <see cref="McCurrentUser"/> object with the profile data</returns>
        public async Task<McCurrentUser> GetUserAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(ApiSettings.UserEndpoint, HttpMethod.Get);
            return await SendRequest<McCurrentUser>(request, cancellationToken);
        }

        /// <summary>
        /// Updates the user profile (display name) asynchronously.
        /// </summary>
        /// <param name="user">The user data that should be updated</param>
        /// <param name="cancellationToken">CancellationToken to cancel the request</param>
        /// <returns>Returns a <see cref="McCurrentUser"/> object with the changed profile data</returns>
        public async Task<McCurrentUser> UpdateUserAsync(McUserWithDisplayName user, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(ApiSettings.UserEndpoint, ApiSettings.HttpMethodPatch, body: user);
            return await SendRequest<McCurrentUser>(request, cancellationToken);
        }

        /// <summary>
        /// Accepts a pending invitation to an organization asynchronously.
        /// </summary>
        /// <param name="invitationToken">The invitation token that was sent to the user</param>
        /// <param name="cancellationToken">CancellationToken to cancel the request</param>
        /// <returns></returns>
        public async Task AcceptOrganizationInvitationAsync(string invitationToken, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(string.Format(ApiSettings.OrganizationInvitationAcceptEndpoint, invitationToken), HttpMethod.Post);
            await SendRequest(request, cancellationToken);
        }
        public async Task RejectOrganizationInvitationAsync(string invitationToken, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(string.Format(ApiSettings.OrganizationInvitationRejectEndpoint, invitationToken), HttpMethod.Post);
            await SendRequest(request, cancellationToken);
        }
        public async Task AcceptAllDistributionGroupInvitationsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(ApiSettings.DistributionGroupInvitationAcceptEndpoint, HttpMethod.Post);
            await SendRequest(request, cancellationToken);
        }
        public async Task AcceptAppInvitationAsync(string invitationToken, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(string.Format(ApiSettings.AppInvitationAcceptEndpoint, invitationToken), HttpMethod.Post);
            await SendRequest(request, cancellationToken);
        }
        public async Task RejectAppInvitationAsync(string invitationToken, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(string.Format(ApiSettings.AppInvitationRejectEndpoint, invitationToken), HttpMethod.Post);
            await SendRequest(request, cancellationToken);
        }
        #endregion

        #region Organization
        public async Task RemoveOrganizationUserAsync(string organizationName, string userName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(string.Format(ApiSettings.OrganizationUserEndpoint, organizationName, userName), HttpMethod.Delete);
            await SendRequest(request, cancellationToken);
        }
        public async Task<McOrganizationUser> UpdateOrganizationUserAsync(string organizationName, string userName, McOrganizationUserWithRole organizationUser, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.OrganizationUserEndpoint, organizationName, userName),
                ApiSettings.HttpMethodPatch,
                body: organizationUser);
            var user = await SendRequest<McOrganizationUser>(request, cancellationToken);
            return AddOrganizationNameToUser(user, organizationName);
        }
        public async Task<List<McOrganizationUser>> GetOrganizationUsersAsync(string organizationName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(string.Format(ApiSettings.OrganizationUsersEndpoint, organizationName), HttpMethod.Get);
            var users = await SendRequest<List<McOrganizationUser>>(request, cancellationToken);
            return users.Select(user => AddOrganizationNameToUser(user, organizationName)).ToList();
        }
        private McOrganizationUser AddOrganizationNameToUser(McOrganizationUser user, string organizationName)
        {
            if (user == null)
                return null;
            user.OrganizationName = organizationName;
            if(user is IAccountServiceHolder)
            {
                if ((user as IAccountServiceHolder).AccountService == null)
                    (user as IAccountServiceHolder).AccountService = this;
            }
            return user;
        }
        public async Task InviteUserToOrganizationAsync(string organizationName, string email, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.OrganizationUserInvitationEndpoint, organizationName),
                HttpMethod.Post,
                body: new McUserWithEmail() { Email = email });
            await SendRequest(request, cancellationToken);
        }
        public async Task<List<McOrganizationInvitation>> GetPendingInvitationsOfOrganizationAsync(string organizationName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.OrganizationUserInvitationEndpoint, organizationName),
                HttpMethod.Get);
            var invitations = await SendRequest<List<McOrganizationInvitation>>(request, cancellationToken);
            return invitations.Select(invite => AddOrganizationNameToInvite(invite, organizationName)).ToList();
        }
        private McOrganizationInvitation AddOrganizationNameToInvite(McOrganizationInvitation invite, string organizationName)
        {
            if (invite == null)
                return null;
            invite.OrganizationName = organizationName;
            if (invite is IAccountServiceHolder)
            {
                if ((invite as IAccountServiceHolder).AccountService == null)
                    (invite as IAccountServiceHolder).AccountService = this;
            }
            return invite;
        }
        public async Task RemoveInvitationToOrganization(string organizationName, string email, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.OrganizationUserInvitationEndpoint, organizationName),
                HttpMethod.Delete,
                body: new McUserWithEmail() { Email = email });
            await SendRequest(request, cancellationToken);
        }
        public async Task ResendInvitationToOrganizationAsync(string organizationName, string email, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.OrganizationResendUserInvitationEndpoint, organizationName),
                HttpMethod.Post,
                body: new McUserWithEmail() { Email = email });
            await SendRequest(request, cancellationToken);
        }
        public async Task<List<McApp>> GetAppsOfOrganizationAsync(string organizationName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.OrganizationAppsEndpoint, organizationName),
                HttpMethod.Get);
            return await SendRequest<List<McApp>>(request, cancellationToken);
        }
        public async Task<McApp> CreateAppInOrganizationAsync(string organizationName, McAppSlim app, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.OrganizationAppsEndpoint, organizationName),
                HttpMethod.Post,
                body: app);
            return await SendRequest<McApp>(request, cancellationToken);
        }
        public async Task<McOrganization> GetOrganizationAsync(string organizationName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.OrganizationEndpoint, organizationName),
                HttpMethod.Get);
            return await SendRequest<McOrganization>(request, cancellationToken);
        }
        public async Task DeleteOrganizationAsync(string organizationName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.OrganizationEndpoint, organizationName),
                HttpMethod.Delete);
            await SendRequest(request, cancellationToken);
        }
        public async Task<McOrganization> UpdateOrganizationAsync(string organizationName, BaseOrganization organization, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.OrganizationEndpoint, organizationName),
                ApiSettings.HttpMethodPatch,
                body: organization);
            return await SendRequest<McOrganization>(request, cancellationToken);
        }
        public async Task<List<McOrganization>> GetOrganizationsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                ApiSettings.OrganizationsEndpoint,
                HttpMethod.Get);
            return await SendRequest<List<McOrganization>>(request, cancellationToken);
        }
        public async Task<McOrganization> CreateOrganizationAsync(BaseOrganization organisation, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                ApiSettings.OrganizationsEndpoint,
                HttpMethod.Post,
                body:organisation);
            return await SendRequest<McOrganization>(request, cancellationToken);
        }
        #endregion

        #region Apps
        public async Task<McApp> CreateAppAsync(McAppSlim app, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                ApiSettings.AppsEndpoint,
                HttpMethod.Post,
                body: app);
            return await SendRequest<McApp>(request, cancellationToken);
        }
        public async Task<List<McApp>> GetAppsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                ApiSettings.AppsEndpoint,
                HttpMethod.Get);
            return await SendRequest<List<McApp>>(request, cancellationToken);
        }
        public async Task<McApp> UpdateAppAsync(string ownerName, string appName, McAppBase app, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppEndpoint, ownerName, appName),
                ApiSettings.HttpMethodPatch,
                body:app);
            return await SendRequest<McApp>(request, cancellationToken);
        }
        public async Task<McApp> GetAppAsync(string ownerName, string appName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppEndpoint, ownerName, appName),
                HttpMethod.Get);
            return await SendRequest<McApp>(request, cancellationToken);
        }
        public async Task DeleteAppAsync(string ownerName, string appName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppEndpoint, ownerName, appName),
                HttpMethod.Delete);
            await SendRequest(request, cancellationToken);
        }
        public async Task<List<McDistributionGroup>> GetDistributionGroups(string ownerName, string appName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppDistributionGroupsEndpoint, ownerName, appName),
                HttpMethod.Get);
            var dgroups = await SendRequest<List<McDistributionGroup>>(request, cancellationToken);
            return dgroups.Select(group => AddAppDataToT(group, ownerName, appName)).ToList();
        }
        public async Task<McDistributionGroup> CreateDistributionGroup(string ownerName, string appName, string distributionGroupName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppDistributionGroupsEndpoint, ownerName, appName),
                HttpMethod.Post,
                body: new McDistributionGroupBase() { Name = distributionGroupName });
            return AddAppDataToT(await SendRequest<McDistributionGroup>(request, cancellationToken), ownerName, appName);
        }
        public async Task<McDistributionGroup> UpdateDistributionGroup(string ownerName, string appName, string distributionGroupName, string newName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppDistributionGroupEndpoint, ownerName, appName, distributionGroupName),
                ApiSettings.HttpMethodPatch,
                body: new McDistributionGroupBase() { Name = newName });
            return AddAppDataToT(await SendRequest<McDistributionGroup>(request, cancellationToken), ownerName, appName);
        }
        public async Task<McDistributionGroup> GetDistributionGroup(string ownerName, string appName, string distributionGroupName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppDistributionGroupEndpoint, ownerName, appName, distributionGroupName),
                HttpMethod.Get);
            return AddAppDataToT(await SendRequest<McDistributionGroup>(request, cancellationToken), ownerName, appName);
        }

        public async Task DeleteDistributionGroup(string ownerName, string appName, string distributionGroupName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppDistributionGroupEndpoint, ownerName, appName, distributionGroupName),
                HttpMethod.Delete);
            await SendRequest(request, cancellationToken);
        }
        public async Task<List<McDistributionGroupInvitation>> AddDistributionGroupMembers(string ownerName, string appName, string distributionGroupName, McUsersWithEmailList users, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppDistributionGroupMembersEndpoint, ownerName, appName, distributionGroupName),
                HttpMethod.Post,
                body: users);
            return await SendRequest<List<McDistributionGroupInvitation>>(request, cancellationToken);
        }
        public async Task<List<McDistributionGroupUser>> GetDistributionGroupMembers(string ownerName, string appName, string distributionGroupName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppDistributionGroupMembersEndpoint, ownerName, appName, distributionGroupName),
                HttpMethod.Get);
            return await SendRequest<List<McDistributionGroupUser>>(request, cancellationToken);
        }
        public async Task<List<McDistributionGroupInvitation>> RemoveDistributionGroupMembers(string ownerName, string appName, string distributionGroupName, McUsersWithEmailList users, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppDistributionGroupMembersEndpoint, ownerName, appName, distributionGroupName),
                HttpMethod.Delete,
                body: users);
            return await SendRequest<List<McDistributionGroupInvitation>>(request, cancellationToken);
        }
        public async Task InviteUserToApp(string ownerName, string appName, string email, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppInvitationsEndpoint, ownerName, appName),
                HttpMethod.Post,
                body: new McUserWithEmail() { Email = email });
            await SendRequest(request, cancellationToken);
        }
        public async Task<List<McAppInvitation>> GetAppInvitations(string ownerName, string appName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppInvitationsEndpoint, ownerName, appName),
                HttpMethod.Get);
            var invitations = await SendRequest<List<McAppInvitation>>(request, cancellationToken);
            return invitations.Select(invite => AddAppDataToT(invite, ownerName, appName)).ToList();
        }

        public async Task UpdateAppInvitationPermissions(string ownerName, string appName, string userEmail, McPermissionData permissions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppInvitationEndpoint, ownerName, appName, userEmail),
                ApiSettings.HttpMethodPatch,
                body: permissions);
            await SendRequest(request, cancellationToken);
        }
        public async Task DeleteAppInvitation(string ownerName, string appName, string userEmail, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppInvitationEndpoint, ownerName, appName, userEmail),
                HttpMethod.Delete);
            await SendRequest(request, cancellationToken);
        }
        public async Task<List<McUser>> GetAppTesters(string ownerName, string appName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppTestersEndpoint, ownerName, appName),
                HttpMethod.Get);
            return await SendRequest<List<McUser>>(request, cancellationToken);
        }
        public async Task<List<McAppUser>> GetAppUsers(string ownerName, string appName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppUsersEndpoint, ownerName, appName),
                HttpMethod.Get);
            var users = await SendRequest<List<McAppUser>>(request, cancellationToken);
            return users.Select(user => AddAppDataToT(user, ownerName, appName)).ToList();
        }

        public async Task UpdateAppUser(string ownerName, string appName, string userEmail, McPermissionData permissions, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppUserEndpoint, ownerName, appName, userEmail),
                ApiSettings.HttpMethodPatch,
                body: permissions);
            await SendRequest(request, cancellationToken);
        }
        public async Task DeleteAppUser(string ownerName, string appName, string userEmail, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppUserEndpoint, ownerName, appName, userEmail),
               HttpMethod.Delete);
            await SendRequest(request, cancellationToken);
        }

        public async Task<McApp> TransferAppOwnership(string ownerName, string appName, string destinationOwnerName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.AppTransferEndpoint, ownerName, appName, destinationOwnerName),
               HttpMethod.Post);
            return await SendRequest<McApp>(request, cancellationToken);
        }
        #endregion

        #region Api
        public async Task<List<McApiTokenBase>> GetApiTokensAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(ApiSettings.ApiTokensEndpoint, HttpMethod.Get);
            return await SendRequest<List<McApiTokenBase>>(request, cancellationToken);
        }
        public async Task<McApiToken> CreateApiTokenAsync(McApiTokenInformation tokenInfo, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(ApiSettings.ApiTokensEndpoint, HttpMethod.Post, body: tokenInfo);
            return await SendRequest<McApiToken>(request, cancellationToken);
        }
        public async Task<McApiToken> DeleteApiTokenAsync(string tokenId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(string.Format(ApiSettings.ApiTokenDeleteEndpoint, tokenId), HttpMethod.Delete);
            return await SendRequest<McApiToken>(request, cancellationToken);
        }
        #endregion
        private T AddAppDataToT<T>(T appInvite, string appOwnerName, string appName) where T : IAppDataHolder
        {
            if (appInvite == null)
                return default(T);
            appInvite.AppName = appName;
            appInvite.AppOwnerName = appOwnerName;
            if (appInvite is IAccountServiceHolder)
            {
                if ((appInvite as IAccountServiceHolder).AccountService == null)
                    (appInvite as IAccountServiceHolder).AccountService = this;
            }
            return appInvite;
        }
    }
}

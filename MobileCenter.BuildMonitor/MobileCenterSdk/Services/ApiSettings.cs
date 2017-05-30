using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenterSdk.Services
{
    internal static class ApiSettings
    {
        public static string ApiBaseUrl { get; set; } = "https://api.mobile.azure.com/v0.1";


        //Custom Http Methods
        public static HttpMethod HttpMethodPatch { get; set; } = new HttpMethod("PATCH");

        #region Api Token Endpoints
        public static string ApiTokensEndpoint { get; set; } = "api_tokens";
        public static string ApiTokenDeleteEndpoint { get; set; } = ApiTokensEndpoint + "/{0}";

        #endregion

        #region User Endpoints
        public static string UserEndpoint { get; set; } = "user";

        public static string InvitationsEndpoint { get; set; } = UserEndpoint + "/invitations";
        public static string OrganizationInvitationEndpoint { get; set; } = InvitationsEndpoint + "/orgs";
        public static string AppInvitationEnpoint { get; set; } = InvitationsEndpoint + "/apps";
        public static string OrganizationInvitationAcceptEndpoint { get; set; } = OrganizationInvitationEndpoint + "/{0}/accept";
        public static string OrganizationInvitationRejectEndpoint { get; set; } = OrganizationInvitationEndpoint + "/{0}/reject";
        public static string AppInvitationAcceptEndpoint { get; set; } = AppInvitationEnpoint + "/{0}/accept";
        public static string AppInvitationRejectEndpoint { get; set; } = AppInvitationEnpoint + "/{0}/reject";
        public static string DistributionGroupInvitationAcceptEndpoint { get; set; } = InvitationsEndpoint + "/distribution_groups/accept";
        #endregion

        #region Organization Endpoints 
        public static string OrganizationsEndpoint { get; set; } = "orgs";
        public static string OrganizationEndpoint { get; set; } = OrganizationsEndpoint + "/{0}";
        public static string OrganizationUsersEndpoint { get; set; } = OrganizationEndpoint + "/users";
        public static string OrganizationUserEndpoint { get; set; } = OrganizationUsersEndpoint + "/{1}";
        public static string OrganizationAppsEndpoint { get; set; } = OrganizationEndpoint +"/apps";
        public static string OrganizationUserInvitationEndpoint { get; set; } = OrganizationEndpoint + "/invitations";
        public static string OrganizationResendUserInvitationEndpoint { get; set; } = OrganizationUserInvitationEndpoint + "/resend";
        #endregion

        #region App Endpoints
        public static string AppsEndpoint { get; set; } = "apps";
        public static string AppEndpoint { get; set; } = AppsEndpoint + "/{0}/{1}";
        public static string AppDistributionGroupsEndpoint { get; set; } = AppEndpoint + "/distribution_groups";
        public static string AppDistributionGroupEndpoint { get; set; } = AppDistributionGroupsEndpoint + "/{2}";
        public static string AppDistributionGroupMembersEndpoint { get; set; } = AppDistributionGroupEndpoint + "/members";
        public static string AppInvitationsEndpoint { get; set; } = AppEndpoint + "/invitations";
        public static string AppInvitationEndpoint { get; set; } = AppInvitationsEndpoint + "/{2}";
        public static string AppTestersEndpoint { get; set; } = AppEndpoint + "/testers";
        public static string AppUsersEndpoint { get; set; } = AppEndpoint + "/users";
        public static string AppUserEndpoint { get; set; } = AppUserEndpoint + "/{2}";
        public static string AppTransferEndpoint { get; set; } = AppEndpoint + "/transfer/{2}";

        public static string AppBranchesEndpoint { get; set; } = AppEndpoint + "/branches";
        public static string AppBranchEndpoint { get; set; } = AppBranchesEndpoint + "/{2}";
        public static string AppBranchBuildsEndpoint { get; set; } = AppBranchEndpoint + "/builds";
        public static string AppBranchConfigEndpoint { get; set; } = AppBranchEndpoint + "/config";
        public static string AppBranchToolsetEndpoint { get; set; } = AppBranchEndpoint + "/toolset_projects";
        public static string AppBuildServiceStatusEndpoint { get; set; } = AppEndpoint + "/build_service_status";
        public static string AppBuildEndpoint { get; set; } = AppEndpoint + "/build/{2}";
        public static string AppBuildDistributeEndpoint { get; set; } = AppBuildEndpoint + "/distribute";
        public static string AppBuildDownloadsEndpoint { get; set; } = AppBuildEndpoint + "/downloads/{3}";
        public static string AppBuildLogEndpoint { get; set; } = AppBuildEndpoint + "/logs";
        public static string AppCommitsEndpoint { get; set; } = AppEndpoint + "/commits/batch";
        public static string AppRepoConfigEndpoint { get; set; } = AppEndpoint + "/repo_config";
        public static string AppSourceHostsEndpoint { get; set; } = AppEndpoint + "/{2}/repositories";
        public static string AppXcodeVersionEndpoint { get; set; } = AppEndpoint + "/xcode_versions";
        #endregion

        #region Test Endpoints
        public static string TestRunsEndpoint { get; set; } = AppEndpoint + "/test_runs";
        public static string TestRunEndpoint { get; set; } = TestRunsEndpoint + "/{2}";
        public static string TestRunStateEndpoint { get; set; } = TestRunEndpoint + "/state";
        public static string TestSeriesEndpoint { get; set; } = AppEndpoint + "/test_series";
        public static string SingleTestSeriesEndpoint { get; set; } = TestSeriesEndpoint + "/{2}";
        public static string TestSeriesTestRunsEndpoint { get; set; } = SingleTestSeriesEndpoint + "/test_runs";
        #endregion
    }
}

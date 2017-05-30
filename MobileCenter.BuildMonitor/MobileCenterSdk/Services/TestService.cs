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
    public class TestService : ServiceBase
    {
        public TestService(MobileCenterSdkClient mcsc) : base(mcsc){}
        public async Task<List<McTestRun>> GetTestRunsAsync(string ownerName, string appName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.TestRunsEndpoint, ownerName, appName),
                HttpMethod.Get);
            return await SendRequest<List<McTestRun>>(request, cancellationToken);
        }
        public async Task<McTestRun> GetTestRunAsync(string ownerName, string appName, string testRunId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.TestRunEndpoint, ownerName, appName, testRunId),
                HttpMethod.Get);
            return await SendRequest<McTestRun>(request, cancellationToken);
        }
        public async Task<McTestRun> DeleteTestRunAsync(string ownerName, string appName, string testRunId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.TestRunEndpoint, ownerName, appName, testRunId),
                HttpMethod.Delete);
            return await SendRequest<McTestRun>(request, cancellationToken);
        }
        public async Task<List<McTestRun>> GetTestRunsOfSeriesAsync(string ownerName, string appName, string seriesSlug, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.TestSeriesTestRunsEndpoint, ownerName, appName, seriesSlug),
                HttpMethod.Get);
            return await SendRequest<List<McTestRun>>(request, cancellationToken);
        }
        public async Task<List<McTestSeries>> GetTestSeriesAsync(string ownerName, string appName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.TestSeriesEndpoint, ownerName, appName),
                HttpMethod.Get);
            return await SendRequest<List<McTestSeries>>(request, cancellationToken);
        }
        public async Task<McTestRunState> GetTestRunStateAsync(string ownerName, string appName, string testRunId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var request = PrepareHttpRequest(
                string.Format(ApiSettings.TestRunStateEndpoint, ownerName, appName, testRunId),
                HttpMethod.Get);
            return await SendRequest<McTestRunState>(request, cancellationToken);
        }
    }
}

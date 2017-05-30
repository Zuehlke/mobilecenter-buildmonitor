using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenterSdk.Models
{

    public class McTestRun
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "platform")]
        public string Platform { get; set; }

        [JsonProperty(PropertyName = "stats")]
        public McTestRunStats Stats { get; set; }

        [JsonProperty(PropertyName = "runStatus")]
        public string RunStatus { get; set; }

        [JsonProperty(PropertyName = "resultStatus")]
        public string ResultStatus { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "appVersion")]
        public string AppVersion { get; set; }

        [JsonProperty(PropertyName = "testSeries")]
        public string TestSeries { get; set; }

        [JsonProperty(PropertyName = "testType")]
        public string TestType { get; set; }

        [JsonProperty(PropertyName = "uploadedBy")]
        public string UploadedBy { get; set; }
    }

    public class McTestRunStats
    {
        [JsonProperty(PropertyName = "devices")]
        public int Devices { get; set; }

        [JsonProperty(PropertyName = "devicesFinished")]
        public int DevicesFinished { get; set; }

        [JsonProperty(PropertyName = "devicesFailed")]
        public int DevicesFailed { get; set; }

        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }

        [JsonProperty(PropertyName = "passed")]
        public int Passed { get; set; }

        [JsonProperty(PropertyName = "failed")]
        public int Failed { get; set; }

        [JsonProperty(PropertyName = "skipped")]
        public int Skipped { get; set; }

        [JsonProperty(PropertyName = "peakMemory")]
        public int PeakMemory { get; set; }

        [JsonProperty(PropertyName = "totalDeviceMinutes")]
        public int TotalDeviceMinutes { get; set; }
    }

    public class McTestSeries
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "slug")]
        public string Slug { get; set; }

        [JsonProperty(PropertyName = "mostRecentActivity")]
        public DateTime MostRecentActivity { get; set; }

        [JsonProperty(PropertyName = "testRuns")]
        public List<McTestRunSummary> TestRuns { get; set; }
    }

    public class McTestRunSummary
    {
        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "statusDescription")]
        public string StatusDescription { get; set; }

        [JsonProperty(PropertyName = "failed")]
        public int Failed { get; set; }

        [JsonProperty(PropertyName = "passed")]
        public int Passed { get; set; }

        [JsonProperty(PropertyName = "completed")]
        public bool Completed { get; set; }
    }

    public class McTestRunState
    {
        [JsonProperty(PropertyName = "message")]
        public List<string> Message { get; set; }

        [JsonProperty(PropertyName = "wait_time")]
        public int WaitTime { get; set; }

        [JsonProperty(PropertyName = "exit_code")]
        public int ExitCode { get; set; }
    }

}

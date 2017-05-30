using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenterSdk.Models
{
    public class McBranchConfiguration
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "trigger")]
        public string Trigger { get; set; }

        [JsonProperty(PropertyName = "testsEnabled")]
        public bool IsTestsEnabled { get; set; }

        [JsonProperty(PropertyName = "signed")]
        public bool IsSigned { get; set; }

        [JsonProperty(PropertyName = "toolsets")]
        public McBranchToolsets Toolsets { get; set; }
    }

    public class McBranchToolsets
    {
        [JsonProperty(PropertyName = "xcode")]
        public McXcodeToolset Xcode { get; set; }

        [JsonProperty(PropertyName = "javascript")]
        public McJavascriptToolset Javascript { get; set; }

        [JsonProperty(PropertyName = "xamarin")]
        public McXamarinToolset Xamarin { get; set; }

        [JsonProperty(PropertyName = "android")]
        public McAndroidToolset Android { get; set; }
    }

    public class McXcodeToolset
    {
        [JsonProperty(PropertyName = "projectOrWorkspacePath")]
        public string ProjectOrWorkspacePath { get; set; }

        [JsonProperty(PropertyName = "podfilePath")]
        public string PodfilePath { get; set; }

        [JsonProperty(PropertyName = "provisioningProfileEncoded")]
        public string ProvisioningProfileEncoded { get; set; }

        [JsonProperty(PropertyName = "certificateEncoded")]
        public string CertificateEncoded { get; set; }

        [JsonProperty(PropertyName = "certificatePassword")]
        public string CertificatePassword { get; set; }

        [JsonProperty(PropertyName = "scheme")]
        public string Scheme { get; set; }

        [JsonProperty(PropertyName = "xcodeVersion")]
        public string XcodeVersion { get; set; }

        [JsonProperty(PropertyName = "provisioningProfileFilename")]
        public string ProvisioningProfileFilename { get; set; }

        [JsonProperty(PropertyName = "certificateFilename")]
        public string CertificateFilename { get; set; }

        [JsonProperty(PropertyName = "teamId")]
        public string TeamId { get; set; }

        [JsonProperty(PropertyName = "automaticSigning")]
        public bool AutomaticSigning { get; set; }
    }

    public class McJavascriptToolset
    {
        [JsonProperty(PropertyName = "packageJsonPath")]
        public string PackageJsonPath { get; set; }

        [JsonProperty(PropertyName = "runTests")]
        public bool RunTests { get; set; }
    }

    public class McXamarinToolset
    {
        [JsonProperty(PropertyName = "slnPath")]
        public string SlnPath { get; set; }

        [JsonProperty(PropertyName = "isSimBuild")]
        public bool IsSimBuild { get; set; }

        [JsonProperty(PropertyName = "args")]
        public string Args { get; set; }

        [JsonProperty(PropertyName = "configuration")]
        public string Configuration { get; set; }

        [JsonProperty(PropertyName = "p12File")]
        public string P12File { get; set; }

        [JsonProperty(PropertyName = "p12Pwd")]
        public string P12Pwd { get; set; }

        [JsonProperty(PropertyName = "provProfile")]
        public string ProvProfile { get; set; }
    }

    public class McAndroidToolset
    {
        [JsonProperty(PropertyName = "gradleWrapperPath")]
        public string GradleWrapperPath { get; set; }

        [JsonProperty(PropertyName = "module")]
        public string Module { get; set; }

        [JsonProperty(PropertyName = "variant")]
        public string Variant { get; set; }

        [JsonProperty(PropertyName = "runTests")]
        public bool RunTests { get; set; }

        [JsonProperty(PropertyName = "runLint")]
        public bool RunLint { get; set; }
    }

}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenterSdk.Models
{
    public class McToolsetProjects
    {
        [JsonProperty(PropertyName = "commit")]
        public string Commit { get; set; }

        [JsonProperty(PropertyName = "xcode")]
        public McXcode Xcode { get; set; }

        [JsonProperty(PropertyName = "javascript")]
        public McJavascript Javascript { get; set; }

        [JsonProperty(PropertyName = "xamarin")]
        public McXamarin Xamarin { get; set; }

        [JsonProperty(PropertyName = "android")]
        public McAndroid Android { get; set; }

        [JsonProperty(PropertyName = "uwp")]
        public McUwp Uwp { get; set; }
    }
    public class McJavascript
    {
        [JsonProperty(PropertyName = "packageJsonPaths")]
        public List<string> PackageJsonPaths { get; set; }
    }

    public class McAndroid
    {
        [JsonProperty(PropertyName = "androidModules")]
        public List<McAndroidModule> AndroidModules { get; set; }

        [JsonProperty(PropertyName = "gradleWrapperPath")]
        public string GradleWrapperPath { get; set; }
    }

    public class McUwp
    {
        [JsonProperty(PropertyName = "uwpSolutions")]
        public List<McUwpSolution> UwpSolutions { get; set; }
    }

    public class McUwpSolution
    {
        [JsonProperty(PropertyName = "path")]
        public string Path { get; set; }

        [JsonProperty(PropertyName = "configurations")]
        public List<string> Configurations { get; set; }
    }


    public class McAndroidModule
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "buildTypes")]
        public List<string> BuildTypes { get; set; }

        [JsonProperty(PropertyName = "buildVariants")]
        public List<string> BuildVariants { get; set; }
    }
    public class McXcode
    {
        [JsonProperty(PropertyName = "xcodeSchemeContainers")]
        public List<XcodeSchemeContainer> XcodeSchemeContainers { get; set; }
    }

    public class XcodeSchemeContainer
    {
        [JsonProperty(PropertyName = "path")]
        public string Path { get; set; }

        [JsonProperty(PropertyName = "sharedSchemes")]
        public List<McSharedXcodeScheme> SharedSchemes { get; set; }

        [JsonProperty(PropertyName = "podfilePath")]
        public string PodfilePath { get; set; }
    }

    public class McSharedXcodeScheme
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "hasTestAction")]
        public bool HasTestAction { get; set; }
    }
    public class McXamarin
    {
        [JsonProperty(PropertyName = "xamarinSolutions")]
        public List<McXamarinSolution> XamarinSolutions { get; set; }
    }
    public class McXamarinSolution
    {
        [JsonProperty(PropertyName = "path")]
        public string Path { get; set; }

        [JsonProperty(PropertyName = "configurations")]
        public List<string> Configurations { get; set; }
    }


    public class McToolsetProjectConfiguration
    {
        [JsonProperty(PropertyName = "toolsets")]
        public McToolsets Toolsets { get; set; }

        [JsonProperty(PropertyName = "environmentVariables")]
        public List<string> EnvironmentVariables { get; set; }

        [JsonProperty(PropertyName = "trigger")]
        public string Trigger { get; set; }

        [JsonProperty(PropertyName = "testsEnabled")]
        public bool TestsEnabled { get; set; }
    }

    public class McToolsets
    {
        [JsonProperty(PropertyName = "xcode")]
        public McXcodeToolsetConfig Xcode { get; set; }

        [JsonProperty(PropertyName = "javascript")]
        public McJavascriptToolsetConfig Javascript { get; set; }

        [JsonProperty(PropertyName = "android")]
        public McAndroidToolsetConfig Android { get; set; }

        [JsonProperty(PropertyName = "uwp")]
        public McUwpToolsetConfig Uwp { get; set; }

        [JsonProperty(PropertyName = "xamarin")]
        public McXamarinToolsetConfig Xamarin { get; set; }
    }

    public class McXcodeToolsetConfig
    {
        [JsonProperty(PropertyName = "projectOrWorkspacePath")]
        public string ProjectOrWorkspacePath { get; set; }

        [JsonProperty(PropertyName = "podfilePath")]
        public string PodfilePath { get; set; }

        [JsonProperty(PropertyName = "scheme")]
        public string Scheme { get; set; }

        [JsonProperty(PropertyName = "xcodeVersion")]
        public string XcodeVersion { get; set; }
    }

    public class McJavascriptToolsetConfig
    {
        [JsonProperty(PropertyName = "packageJsonPath")]
        public string PackageJsonPath { get; set; }

        [JsonProperty(PropertyName = "runTests")]
        public bool RunTests { get; set; }
    }

    public class McAndroidToolsetConfig
    {
        [JsonProperty(PropertyName = "gradleWrapperPath")]
        public string GradleWrapperPath { get; set; }

        [JsonProperty(PropertyName = "module")]
        public string Module { get; set; }

        [JsonProperty(PropertyName = "buildVariant")]
        public string BuildVariant { get; set; }

        [JsonProperty(PropertyName = "runTests")]
        public bool RunTests { get; set; }

        [JsonProperty(PropertyName = "runLint")]
        public bool RunLint { get; set; }
    }

    public class McUwpToolsetConfig
    {
        [JsonProperty(PropertyName = "slnPath")]
        public string SolutionPath { get; set; }

        [JsonProperty(PropertyName = "configuration")]
        public string Configuration { get; set; }

        [JsonProperty(PropertyName = "platforms")]
        public List<string> Platforms { get; set; }
    }
    

    public class McXamarinToolsetConfig
    {
        [JsonProperty(PropertyName = "configuration")]
        public string Configuration { get; set; }

        [JsonProperty(PropertyName = "slnPath")]
        public string SolutionPath { get; set; }

        [JsonProperty(PropertyName = "isSimBuild")]
        public bool IsSimBuild { get; set; }
    }

}

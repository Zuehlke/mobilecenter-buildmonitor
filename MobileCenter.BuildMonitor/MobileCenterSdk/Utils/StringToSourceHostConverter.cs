using MobileCenterSdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenterSdk.Utils
{
    public static class StringToSourceHostConverter
    {
        private const string GitHub = "github";
        private const string Bitbucket = "bitbucket";
        private const string Vsts = "vsts";

        public static McSourceHost Convert(string sourceHostString)
        {
            switch(sourceHostString)
            {
                case GitHub:
                    return McSourceHost.GitHub;
                case Bitbucket:
                    return McSourceHost.Bitbucket;
                case Vsts:
                    return McSourceHost.Vsts;
                default:
                    return McSourceHost.Unknown;
            }
        }
        public static string ConvertBack(McSourceHost sourceHost)
        {
            switch(sourceHost)
            {
                case McSourceHost.GitHub:
                    return GitHub;
                case McSourceHost.Bitbucket:
                    return Bitbucket;
                case McSourceHost.Vsts:
                    return Vsts;
                default:
                    return string.Empty;
            }
        }
    }
}

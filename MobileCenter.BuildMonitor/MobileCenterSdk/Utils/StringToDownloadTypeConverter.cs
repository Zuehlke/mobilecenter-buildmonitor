using MobileCenterSdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenterSdk.Utils
{
    public static class StringToDownloadTypeConverter
    {
        private const string Build = "build";
        private const string Symbols = "symbols";
        private const string Logs = "logs";
        private const string TestReportPreview = "test-report-preview";

        public static McDownloadType Convert(string downloadTypeString)
        {
            switch(downloadTypeString)
            {
                case Build:
                    return McDownloadType.Build;
                case Symbols:
                    return McDownloadType.Symbols;
                case Logs:
                    return McDownloadType.Logs;
                case TestReportPreview:
                    return McDownloadType.TestReportPreview;
                default:
                    return McDownloadType.Unknown;
            }
        }
        public static string ConvertBack(McDownloadType downloadType)
        {
            switch(downloadType)
            {
                case McDownloadType.Build:
                    return Build;
                case McDownloadType.Symbols:
                    return Symbols;
                case McDownloadType.Logs:
                    return Logs;
                case McDownloadType.TestReportPreview:
                    return TestReportPreview;
                default:
                    return string.Empty;
            }
        }
    }
}

using MobileCenterSdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenterSdk.Utils
{
    public static class StringToOsTypeConverter
    {
        private const string Android = "Android";
        private const string IOs = "iOS";
        private const string MacOs = "macOS";
        private const string Tizen = "Tizen";
        private const string Windows = "Windows";
        private const string Custom = "Custom";

        public static McAppOs Convert(string osString)
        {
            switch (osString)
            {
                case Android:
                    return McAppOs.Android;
                case IOs:
                    return McAppOs.IOs;
                case MacOs:
                    return McAppOs.MacOs;
                case Tizen:
                    return McAppOs.Tizen;
                case Windows:
                    return McAppOs.Windows;
                case Custom:
                    return McAppOs.Custom;
                default:
                    return McAppOs.Unknown;
            }
        }
        public static string ConvertBack(McAppOs os)
        {
            switch(os)
            {
                case McAppOs.Android:
                    return Android;
                case McAppOs.IOs:
                    return IOs;
                case McAppOs.MacOs:
                    return MacOs;
                case McAppOs.Tizen:
                    return Tizen;
                case McAppOs.Windows:
                    return Windows;
                case McAppOs.Custom:
                    return Custom;
                default:
                    return string.Empty;
            }
        }
    }
}

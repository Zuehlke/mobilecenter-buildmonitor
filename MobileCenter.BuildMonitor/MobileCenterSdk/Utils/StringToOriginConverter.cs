using MobileCenterSdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenterSdk.Utils
{
    public static class StringToOriginConverter
    {
        private const string MobileCenter = "mobile-center";
        private const string HockeyApp = "hockeyapp";
        private const string CodePush = "codepush";
        public static McOrigin Convert(string originString)
        {
            switch (originString.ToLower())
            {
                case MobileCenter:
                    return McOrigin.MobileCenter;
                case HockeyApp:
                    return McOrigin.HockeyApp;
                case CodePush:
                    return McOrigin.CodePush;
                default:
                    return McOrigin.Unknown;
            }
        }
        public static string ConvertBack(McOrigin origin)
        {
            switch(origin)
            {
                case McOrigin.MobileCenter:
                    return MobileCenter;
                case McOrigin.HockeyApp:
                    return HockeyApp;
                case McOrigin.CodePush:
                    return CodePush;
                default:
                    return string.Empty;
            }
        }
    }
}

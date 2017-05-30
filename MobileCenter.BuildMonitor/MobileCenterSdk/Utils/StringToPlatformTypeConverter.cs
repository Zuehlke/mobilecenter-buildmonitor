using MobileCenterSdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenterSdk.Utils
{
    public static class StringToPlatformTypeConverter
    {
        private const string Cordova = "Cordova";
        private const string Java = "Java";
        private const string ObjectiveCSwift = "Objective-C-Swift";
        private const string ReactNative = "React-Native";
        private const string Unity = "Unity";
        private const string UWP = "UWP";
        private const string Xamarin = "Xamarin";
        private const string Unknown = "Unknown";

        public static McAppPlatform Convert(string platformString)
        {
            switch (platformString)
            {
                case Cordova:
                    return McAppPlatform.Cordova;
                case Java:
                    return McAppPlatform.Java;
                case ObjectiveCSwift:
                    return McAppPlatform.ObjectiveCSwift;
                case ReactNative:
                    return McAppPlatform.ReactNative;
                case Unity:
                    return McAppPlatform.Unity;
                case UWP:
                    return McAppPlatform.UWP;
                case Xamarin:
                    return McAppPlatform.Xamarin;
                default:
                    return McAppPlatform.Unknown;
            }
        }
        public static string ConvertBack(McAppPlatform platform)
        {
            switch(platform)
            {
                case McAppPlatform.Cordova:
                    return Cordova;
                case McAppPlatform.Java:
                    return Java;
                case McAppPlatform.ObjectiveCSwift:
                    return ObjectiveCSwift;
                case McAppPlatform.ReactNative:
                    return ReactNative;
                case McAppPlatform.Unity:
                    return Unity;
                case McAppPlatform.UWP:
                    return UWP;
                case McAppPlatform.Xamarin:
                    return Xamarin;
                default:
                    return Unknown;
            }
        }
    }
}

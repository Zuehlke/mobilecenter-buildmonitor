using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileCenter.BuildMonitor.Services
{
    public static class Settings
    {
        private const string TokenKey = "token";
        private static readonly string TokenDefault = string.Empty;

        private static ISettingsService SettingsService
        {
            get
            {
                return DependencyService.Get<ISettingsService>();
            }
        }

        public static string Token {
            get
            {
                return SettingsService.GetValueOrDefault(TokenKey, TokenDefault);
            }
            set
            {
                SettingsService.AddOrUpdateValue(TokenKey, value);
            }
        }
    }
}

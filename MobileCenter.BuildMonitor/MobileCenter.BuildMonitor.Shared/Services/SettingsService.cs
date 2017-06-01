using System;
using System.Collections.Generic;
using System.Text;
using Plugin.Settings;
using MobileCenter.BuildMonitor.Services;

[assembly: Xamarin.Forms.Dependency(typeof(SettingsService))]
namespace MobileCenter.BuildMonitor.Services
{
    public class SettingsService : ISettingsService
    {
        public bool AddOrUpdateValue<T>(string key, T value, string filename = null)
        {
            return CrossSettings.Current.AddOrUpdateValue<T>(key, value, filename);
        }
        public T GetValueOrDefault<T>(string key, T defaultValue, string filename = null)
        {
            return CrossSettings.Current.GetValueOrDefault<T>(key, defaultValue, filename);
        }
    }
}

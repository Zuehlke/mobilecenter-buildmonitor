using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenter.BuildMonitor.Services
{
    public interface ISettingsService
    {
        bool AddOrUpdateValue<T>(string key, T value, string filename = null);
        T GetValueOrDefault<T>(string key, T defaultValue, string filename = null);
    }
}

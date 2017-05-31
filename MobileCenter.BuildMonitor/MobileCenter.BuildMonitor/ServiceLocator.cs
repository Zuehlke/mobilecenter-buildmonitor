using MobileCenter.BuildMonitor.Services;
using MobileCenter.BuildMonitor.ViewModels;
using MobileCenterSdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileCenter.BuildMonitor
{
    public static class ServiceLocator
    {
        private static MobileCenterService _mobileCenterService;
        private static SettingsService _settingsService;
        private static AppListViewModel _appListViewModel;
        

        public static MobileCenterService MobileCenterService
        {
            get
            {
                return _mobileCenterService ?? (_mobileCenterService = new MobileCenterService());
            }
        }

        public static SettingsService SettingsService
        {
            get
            {
                return _settingsService ?? (_settingsService = new SettingsService());
            }
        }

        public static AppListViewModel AppListViewModel
        {
            get
            {
                return _appListViewModel ?? (_appListViewModel = new AppListViewModel(MobileCenterService));
            }
        }
    }
}

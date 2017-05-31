using MobileCenter.BuildMonitor.Mvvm;
using MobileCenter.BuildMonitor.Services;
using MobileCenterSdk;
using MobileCenterSdk.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MobileCenter.BuildMonitor.ViewModels
{
    public class AppListViewModel : ViewModelBase
    {
        // Dependencies
        private readonly MobileCenterService _mobileCenterService;

        // Fields
        private ObservableCollection<McApp> _apps = new ObservableCollection<McApp>();
        private ICommand _refreshCommand;

        // Constructor
        public AppListViewModel(MobileCenterService mobileCenterService)
        {
            _mobileCenterService = mobileCenterService;
        }

        // Properties
        public ObservableCollection<McApp> Apps
        {
            get => _apps;
            set
            {
                SetProperty(ref _apps, value);
            }
        }

        // Commands
        public ICommand RefreshCommand
        {
            get => _refreshCommand ?? (_refreshCommand = new Command(async () => await RefreshAsync()));
        }

        // Interactions
        public async Task RefreshAsync()
        {
            if (IsDataLoading) return;
            IsDataLoading = true;

            try
            {
                var test = await _mobileCenterService.MobileCenterClient.AccountService.GetAppsAsync();

                Apps.Clear();
                test.ForEach(Apps.Add);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Loading apps failed: {e.Message}");
            }

            IsDataLoading = false;
        }
    }
}

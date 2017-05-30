using MobileCenter.BuildMonitor.Mvvm;
using MobileCenterSdk;
using MobileCenterSdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MobileCenter.BuildMonitor.ViewModels
{
    public class AppListViewModel : ViewModelBase
    {
        // Fields
        private List<McApp> _apps;
        private ICommand _refreshCommand;

        // Properties
        public List<McApp> Apps
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
            Console.WriteLine("TEST CL");

            if (IsDataLoading) return;
            IsDataLoading = true;

            try
            {
                
                //var test = await client.AccountService.GetAppsAsync();

                //Apps = test;
            }
            catch (Exception e)
            {
                Console.WriteLine("TEST: " + e.Message);
            }

            IsDataLoading = false;
        }
    }
}

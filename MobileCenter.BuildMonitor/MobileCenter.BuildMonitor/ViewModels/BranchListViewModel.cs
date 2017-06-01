using MobileCenter.BuildMonitor.Mvvm;
using MobileCenterSdk.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MobileCenterSdk.Services;
using System.Threading;

namespace MobileCenter.BuildMonitor.ViewModels
{
    class BranchListViewModel : ViewModelBase
    {
        // Fields
        private ObservableCollection<ItemViewModel> _branchStatuses = new ObservableCollection<ItemViewModel>();
        private ICommand _refreshCommand;
        private McApp _app;
        private BuildService _buildService;

        private CancellationTokenSource _loopCts = new CancellationTokenSource();

        // Properties
        public ObservableCollection<ItemViewModel> BranchStatuses
        {
            get => _branchStatuses;
            set
            {
                SetProperty(ref _branchStatuses, value);
            }
        }

        public BranchListViewModel(McApp app)
        {
            _app = app;
            _buildService = ServiceLocator.MobileCenterService.MobileCenterClient.BuildService;
        }

        // Commands
        public ICommand RefreshCommand
        {
            get => _refreshCommand ?? (_refreshCommand = new Command(async () => await RefreshAsync()));
        }


        // Interactions
        public void StopRefreshingAsync()
        {
            var current = Interlocked.Exchange(ref _loopCts, new CancellationTokenSource());
            current.Cancel();
        }
        public async Task RefreshAsync()
        {
            if (IsDataLoading) return;
            IsDataLoading = true;

            var branches = await _buildService.GetBranchesAsync(_app.Owner.Name, _app.Name);

            BranchStatuses.Clear();
            branches.ForEach(i => BranchStatuses.Add(new ItemViewModel() { BranchStatus = i }));

            while (!_loopCts.IsCancellationRequested)
            {
                try
                {

                    foreach (var b in BranchStatuses)
                    {
                        await UpdateBranchStatusAsync(b);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Loading branches failed: {e.Message}");
                }

                await Task.Delay(TimeSpan.FromSeconds(5));
            }

            IsDataLoading = false;
        }

        private async Task UpdateBranchStatusAsync(ItemViewModel item)
        {
            var builds = await _buildService.GetBranchBuildsAsync(_app.Owner.Name, _app.Name, item.BranchStatus.Branch.Name);

            item.Clear();
            builds.Take(3).ToList().ForEach(item.Add);
        }

        // Item subclass
        public class ItemViewModel : ObservableCollection<McBuild>
        {
            public McBranchStatus BranchStatus { get; set; }
        }
    }
}

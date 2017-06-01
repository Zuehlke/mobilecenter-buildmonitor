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

namespace MobileCenter.BuildMonitor.ViewModels
{
    class BranchListViewModel : ViewModelBase
    {
        // Fields
        private ObservableCollection<ItemViewModel> _branchStatuses = new ObservableCollection<ItemViewModel>();
        private ICommand _refreshCommand;
        private McApp _app;
        private BuildService _buildService;

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
        public async Task RefreshAsync()
        {
            if (IsDataLoading) return;
            IsDataLoading = true;

            try
            {
                var branches = await _buildService.GetBranchesAsync(_app.Owner.Name, _app.Name);

                BranchStatuses.Clear();
                branches.ForEach(i => BranchStatuses.Add(new ItemViewModel() { BranchStatus = i }));

                foreach (var b in BranchStatuses)
                {
                    await UpdateBranchStatusAsync(b);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Loading branches failed: {e.Message}");
            }

            IsDataLoading = false;
        }

        private async Task UpdateBranchStatusAsync(ItemViewModel item)
        {
            var builds = await _buildService.GetBranchBuildsAsync(_app.Owner.Name, _app.Name, item.BranchStatus.Branch.Name);

            item.Builds.Clear();
            builds.ForEach(item.Builds.Add);
        }

        // Item subclass
        public class ItemViewModel : ViewModelBase
        {
            private McBranchStatus _branchStatus;
            private ObservableCollection<McBuild> _builds = new ObservableCollection<McBuild>();

            public McBranchStatus BranchStatus
            {
                get { return _branchStatus; }
                set { SetProperty(ref _branchStatus, value); }
            }

            public ObservableCollection<McBuild> Builds
            {
                get { return _builds; }
                set { SetProperty(ref _builds, value); }
            }
        }
    }
}

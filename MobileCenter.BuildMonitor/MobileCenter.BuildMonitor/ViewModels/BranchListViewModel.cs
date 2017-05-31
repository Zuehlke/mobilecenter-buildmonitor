using MobileCenter.BuildMonitor.Mvvm;
using MobileCenterSdk.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MobileCenter.BuildMonitor.ViewModels
{
    class BranchListViewModel : ViewModelBase
    {
        // Fields
        private ObservableCollection<McBranchStatus> _branches = new ObservableCollection<McBranchStatus>();
        private ICommand _refreshCommand;
        private McApp _app;

        // Properties
        public ObservableCollection<McBranchStatus> Branches
        {
            get => _branches;
            set
            {
                SetProperty(ref _branches, value);
            }
        }

        public BranchListViewModel(McApp app)
        {
            _app = app;
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
                var branches = await _app.GetBranchesAsync();

                Branches.Clear();
                branches.ForEach(Branches.Add);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Loading branches failed: {e.Message}");
            }

            IsDataLoading = false;
        }
    }
}

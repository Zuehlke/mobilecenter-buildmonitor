using MobileCenter.BuildMonitor.ViewModels;
using MobileCenterSdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileCenter.BuildMonitor.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BranchListPage : ContentPage
    {
        private BranchListViewModel ViewModel { get; set; }

        public BranchListPage(McApp app)
        {
            InitializeComponent();
            BindingContext = ViewModel = new BranchListViewModel(app);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var t = ViewModel.RefreshAsync();
        }
    }
}
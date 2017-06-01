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
    public partial class AppListPage : ContentPage
    {
        private AppListViewModel ViewModel { get; set; }

        public AppListPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = ServiceLocator.AppListViewModel;
        }

        private  void MyListview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var app = e.Item as McApp;
             Navigation.PushAsync(new BranchListPage(app));
        }
    }
}
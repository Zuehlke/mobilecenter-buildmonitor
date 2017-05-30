using MobileCenter.BuildMonitor.ViewModels;
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
        public AppListPage()
        {
            InitializeComponent();
            BindingContext = new AppListViewModel();
        }
    }
}
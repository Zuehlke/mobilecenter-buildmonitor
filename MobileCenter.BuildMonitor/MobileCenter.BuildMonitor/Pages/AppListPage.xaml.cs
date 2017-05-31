﻿using MobileCenter.BuildMonitor.ViewModels;
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

        private async void MyListview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new BranchListPage());
        }
    }
}
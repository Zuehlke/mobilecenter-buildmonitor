﻿using MobileCenter.BuildMonitor.Mvvm;
using MobileCenter.BuildMonitor.Pages;
using MobileCenterSdk.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MobileCenter.BuildMonitor.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username;
        private string _password;
        private ICommand _loginCommand;

        public LoginViewModel()
        {
            LoginCommand = new Command(async () =>
            {
                try
                {
                    IsDataLoading = true;
                    await ServiceLocator.MobileCenterService.LoginAsync(Username, Password);
                    await App.Current.MainPage.Navigation.PushAsync(new AppListPage());
                    App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack.First());
                    IsDataLoading = false;
                }catch (MobileCenterException mce)
                {
                    //To-Do: Implement exception handling
                }
            });
        }

        public string Username {
            get => _username;
            set
            {
                SetProperty(ref _username, value);
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
            }
        }
        public ICommand LoginCommand
        {
            get => _loginCommand;
            set
            {
                SetProperty(ref _loginCommand, value);
            }
        }
        public async Task ForwardIfLoggedIn()
        {
            if (ServiceLocator.MobileCenterService.IsUserLoggedIn)
            {
                ServiceLocator.MobileCenterService.SetupTokenFromSettings();
                await App.Current.MainPage.Navigation.PushAsync(new AppListPage());
                App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack.First());
            }
        }
    }
}

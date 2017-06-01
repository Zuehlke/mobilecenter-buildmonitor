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
    public partial class LoginPage : ContentPage
    {
        private LoginViewModel _viewModel;
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new LoginViewModel();
        }
        protected async override void OnAppearing()
        {
            await _viewModel.ForwardIfLoggedIn();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Username.Text = TestCredentials.Username;
            Password.Text = TestCredentials.Password;
        }
    }
}
using ByMe.View.AdminView;
using ByMe.ViewModel.UserViewModel;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ByMe.View.UserView
{
    public partial class UserPenDrives : ContentPage
    {
        private UserPenDrivesViewModel pendriveViewModel;
        public UserPenDrives()
        {
            InitializeComponent();
            Title = "Pen Drives";
            pendriveViewModel = App.Locator.UserPenDrives;
            BindingContext = pendriveViewModel;

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
            await pendriveViewModel.Init();
            pendriveViewModel.AddCartCount();

        }
        public void itemTapped(object sender, ItemTappedEventArgs e)
        {

            this.Navigation.PushAsync(new UserProductDescription(e.Item));
        }
        public void cart_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserCart());
        }
        private void Current_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (!e.IsConnected)
            {
                stkNoConnection.IsVisible = true;
                stk.IsVisible = false;
            }
            else
            {
                stkNoConnection.IsVisible = false;
                stk.IsVisible = true;
            }
        }

    }
}

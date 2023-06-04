using ClientAndStaff.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClientAndStaff
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            if (Global.CurrentUser == null)
            {
                MainPage = new NavigationPage(new SignInUser());
            }
            else
            {
                MainPage = new NavigationPage(new StartPage());
            }
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void Home_Clicked(object sender, EventArgs e)
        {
            MainPage = new NavigationPage(new StartPage());
        }

        private void Cart_Clicked(object sender, EventArgs e)
        {
            MainPage = new NavigationPage(new BasketPage());
        }

        private void Order_Clicked(object sender, EventArgs e)
        {
            MainPage = new NavigationPage(new OrderPage());
        }

        private void Profile_Clicked(object sender, EventArgs e)
        {
            MainPage = new NavigationPage(new ProfilePage());
        }

        private void HomeStaff_Clicked(object sender, EventArgs e)
        {
            MainPage = new NavigationPage(new StartPageStaffTest());
        }

        private void OrderStaff_Clicked(object sender, EventArgs e)
        {

        }

        private void MapsStaff_Clicked(object sender, EventArgs e)
        {
            MainPage = new NavigationPage(new Geo());
        }
    }
}

using ClientAndStaff.Models;
using ClientAndStaff.Pages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ClientAndStaff
{
    public partial class MainPage : ContentPage
    {
        public UserAuth CurrentUser { get; set; }
        public MainPage()
        {
            CurrentUser = new UserAuth();

            InitializeComponent();

            BindingContext = this;
        }

        private async void Btn_SignIn(object sender, EventArgs e)
        {

            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            var result = client.UploadString("http://192.168.0.101:3245/api/Users/SignIn", JsonConvert.SerializeObject(CurrentUser));
            if (result != null)
            {

                await Navigation.PushAsync(new StartPageStaff(), true);

            }
        }

        private async void Btn_Reg(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterUser(), true);
        }
    }
}

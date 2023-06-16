using ClientAndStaff.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClientAndStaff.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckoutPage : ContentPage
    {
        public AddOrder CurrentOrder { get; set; }

        public CheckoutPage()
        {
            
            InitializeComponent();
            OrderListView.ItemsSource = Global.MyBasket;
            

        }
        private async void OnSubmitOrder_Clicked(object sender, EventArgs e)
        {
            
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest()
                    {
                        DesiredAccuracy = GeolocationAccuracy.High,
                        Timeout = TimeSpan.FromSeconds(30)
                    });

                }
                double longitude = 0;
                double latitude = 0;

                if (location == null)
                {
                    //LabelLacation.Text = "No GPS";
                }
                else
                {
                    longitude = location.Longitude;
                    latitude = location.Latitude;
                    //LabelLacation.Text = $"{location.Latitude} and {location.Longitude}";
                }
                CurrentOrder = new AddOrder();
                CurrentOrder.ManuProducts = Global.MyBasket.ToArray();
                CurrentOrder.Description = Description.Text;
                CurrentOrder.LoginUser = Global.CurrentUser.Login;
                CurrentOrder.Longitude = (decimal)longitude;
                CurrentOrder.Latitude = (decimal)latitude;
                var client = new WebClient();
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                var result = client.UploadString(Global.GlobalVar + "api/Orders/AddsProduct", JsonConvert.SerializeObject(CurrentOrder));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", ex.Message , "OK");
                //Debug.WriteLine($"Warning {ex.Message}");
            }
            //Delivery
            /*var clentDelivery = new WebClient();
            clentDelivery.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            var resultDelivery = clentDelivery.UploadString(Global.GlobalVar + "api/Orders/AddsProduct", JsonConvert.SerializeObject(CurrentOrder));*/
            await DisplayAlert("Message", "Order was successful", "OK");
            Global.MyBasket = new List<ManuProduct>();
            await Navigation.PushAsync(new OrderPage());
        }
    }
}
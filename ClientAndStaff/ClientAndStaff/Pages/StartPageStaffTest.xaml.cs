using ClientAndStaff.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    public partial class StartPageStaffTest : ContentPage
    {
        public StartPageStaffTest()
        {
            InitializeComponent();
            var client = new WebClient();
            var response = client.DownloadString(Global.GlobalVar + "api/Orders/GetOrders");
            OrdersListView.ItemsSource = JsonConvert.DeserializeObject<List<Order>>(response);
            var clientStaff = new WebClient();
            var responseStaff = clientStaff.DownloadString(Global.GlobalVar + "api/Orders/GetOrdersFromStaffId?id=" + Global.CurrentUser.Id);
            OngoingOrdersListView.ItemsSource = JsonConvert.DeserializeObject<List<Order>>(responseStaff);
        }

        private async void OnAccept_Clicked(object sender, EventArgs e)
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
                var order = (sender as Button).BindingContext as Order;
                order.IdStatus = 2;
                order.Staff = Global.CurrentUser.Id;
                var geo = new Geol() { Latitude = (decimal)latitude, Longitude = (decimal)longitude };
                var orderData = new { Order = order, Geol = geo };
                var json = JsonConvert.SerializeObject(orderData);
                var client = new WebClient();
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                var url = Global.GlobalVar + "api/Orders/AcceptOrder?id=" + order.Id;
                var result = client.UploadString(url, "PUT", json);

                await DisplayAlert("Message", "Successful", "OK");

            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", ex.Message, "OK");
                //Debug.WriteLine($"Warning {ex.Message}");
            }



            
        }
        //1 Новый 2 Доставка 3 Закрыт
        private async void OnDeliver_Clicked(object sender, EventArgs e)
        {
            try
            {
                var order = (sender as Button).BindingContext as Order;
                order.IdStatus = 3;
               // order.Staff = Global.CurrentUser.Id;
                var json = JsonConvert.SerializeObject(order);
                var client = new WebClient();
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                var url = Global.GlobalVar + "api/Orders/EditOrderForStatus?id=" + order.Id;
                var result = client.UploadString(url, "PUT", json);
                await DisplayAlert("Message", "Successful", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", ex.Message, "OK");
                //Debug.WriteLine($"Warning {ex.Message}");
            }
        }

        private async void Test_Clicked(object sender, EventArgs e)
        {
        //yandexmaps://maps.yandex.ru/?rtext=geo: 54.51404720,36.29488460~geo:54.51404720,36.29488460 
            double originLat = 54.51404870;
            double originLng = 36.29488390;
            double destLat = 54.533312;
            double destLng = 36.313427;
            var url = $"yandexmaps://maps.yandex.ru/?rtext={originLat.ToString().Replace(',', '.')},{originLng.ToString().Replace(',', '.')}~{destLat.ToString().Replace(',', '.')},{destLng.ToString().Replace(',', '.')}&rtt=mt";
            //await Launcher.OpenAsync("http://maps.google.com/?daddr=37.7749,-122.4194&saddr=37.3861,-122.0839&dir=tr");
            await Launcher.OpenAsync($"http://maps.google.com/?daddr={originLat.ToString().Replace(',', '.')},{originLng.ToString().Replace(',', '.')}&saddr={destLat.ToString().Replace(',', '.')},{destLng.ToString().Replace(',', '.')}&dir=tr");
            await DisplayAlert(Title, url, "OK"); 
            //await Launcher.OpenAsync(url);
            //await Launcher.OpenAsync($"yandexmaps://maps.yandex.ru/?rtext={originLat},{originLng}~{destLat},{destLng}&rtt=mt");
            //await Launcher.OpenAsync("yandexmaps://maps.yandex.ru/?rtext=geo:" + originLat + "," + originLng + "~geo:" + destLat + "," + destLng);

        }
    }
}
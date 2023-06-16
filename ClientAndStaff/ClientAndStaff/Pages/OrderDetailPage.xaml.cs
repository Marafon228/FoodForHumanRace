using ClientAndStaff.Helpers;
using ClientAndStaff.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin.Essentials;

namespace ClientAndStaff.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderDetailPage : ContentPage
    {
        public OrderLk CurrentOrder { get; set; }
        public OrderDetailViewModel ViewModel { get; set; }

        public OrderDetailPage(OrderLk order)
        {
            CurrentOrder = order;
            InitializeComponent();
            ViewModel = new OrderDetailViewModel { CurrentOrder = order };
            BindingContext = ViewModel;
            var client = new WebClient();
            var response = client.DownloadString(Global.GlobalVar + "api/OrderAndProducts/GetProductsOrderFromOrderId?id=" + order.Id);
            OrdersList.ItemsSource = JsonConvert.DeserializeObject<List<Product>>(response); 
            if (order.Status == "Доставка")
            {
                TrackBtnGoogle.IsVisible= true;
                TrackBtnYandex.IsVisible= true;
            }
        }

        private async void TrackBtnYandex_Clicked(object sender, EventArgs e)
        {
            var client = new WebClient();
            var response = client.DownloadString(Global.GlobalVar + "api/Orders/GetLocationOrderFromOrderId?id=" + CurrentOrder.Id);
            var orderDelivery = JsonConvert.DeserializeObject<OrderDelivaryReaponse>(response);


            double originLat = 0;
            double originLng = 0;
            double destLat = 0;
            double destLng = 0;
            if (Global.CurrentUser.Role == "Клиент")
            {
                originLat = (double)orderDelivery.LatitudeKl;
                originLng = (double)orderDelivery.LongitudeKl;
                destLat = (double)orderDelivery.LatitudeStaff;
                destLng = (double)orderDelivery.LongitudeStaff;
            }
            else
            {
                originLat = (double)orderDelivery.LatitudeStaff;
                originLng = (double)orderDelivery.LongitudeStaff;
                destLat = (double)orderDelivery.LatitudeKl;
                destLng = (double)orderDelivery.LongitudeKl;
            }

            destLat += 0.0005;
            destLat -= 0.0006;
            await Launcher.OpenAsync($"yandexmaps://maps.yandex.ru/?rtext={originLat.ToString().Replace(',', '.')},{originLng.ToString().Replace(',', '.')}~{destLat.ToString().Replace(',', '.')},{destLng.ToString().Replace(',', '.')}&rtt=mt");
        }

        private async void TrackBtnGoogle_Clicked(object sender, EventArgs e)
        {
            var client = new WebClient();
            var response = client.DownloadString(Global.GlobalVar + "api/Orders/GetLocationOrderFromOrderId?id=" + CurrentOrder.Id);
            var orderDelivery = JsonConvert.DeserializeObject<OrderDelivaryReaponse>(response);


            double originLat = 0;
            double originLng = 0;
            double destLat = 0;
            double destLng = 0;
            if (Global.CurrentUser.Role == "Клиент")
            {
                originLat = (double)orderDelivery.LatitudeKl;
                originLng = (double)orderDelivery.LongitudeKl;
                destLat = (double)orderDelivery.LatitudeStaff;
                destLng = (double)orderDelivery.LongitudeStaff;
            }
            else
            {
                originLat = (double)orderDelivery.LatitudeStaff;
                originLng = (double)orderDelivery.LongitudeStaff;
                destLat = (double)orderDelivery.LatitudeKl;
                destLng = (double)orderDelivery.LongitudeKl;
            }

            //destLat = 54.533312;
            //destLng = 36.313427;
            destLat += 0.0005;
            destLat -= 0.0006;
            await Launcher.OpenAsync($"http://maps.google.com/?daddr={originLat.ToString().Replace(',', '.')},{originLng.ToString().Replace(',', '.')}&saddr={destLat.ToString().Replace(',', '.')},{destLng.ToString().Replace(',', '.')}&dir=tr");
        }
    }
}
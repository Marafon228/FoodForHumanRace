using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClientAndStaff.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPageStaff : ContentPage
    {
        public Order CurrentOrder { get; set; }
        public StartPageStaff()
        {
            InitializeComponent();
            var client = new WebClient();
            var response = client.DownloadString("http://192.168.0.101:3310/api/Orders/GetOrders");
            ListViewOrder.ItemsSource = JsonConvert.DeserializeObject<List<Order>>(response);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Message", "Order was new status successful", "OK");
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Geo());
        }
    }
}
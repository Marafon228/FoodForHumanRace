using ClientAndStaff.Models;
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

        private void OnAccept_Clicked(object sender, EventArgs e)
        {
            var order = (sender as Button).BindingContext as Order;
            order.IdStatus = 2;
            order.Staff = Global.CurrentUser.Id;
            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            var result = client.UploadString(Global.GlobalVar + "api/Orders/AddsProduct", JsonConvert.SerializeObject(order));
        }
        //1 Новый 2 Доставка 3 Закрыт
        private void OnDeliver_Clicked(object sender, EventArgs e)
        {
            var order = (sender as Button).BindingContext as Order;
            order.IdStatus = 3;
            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            var result = client.UploadString(Global.GlobalVar + "api/Orders/AddsProduct", JsonConvert.SerializeObject(order));
        }
    }
}
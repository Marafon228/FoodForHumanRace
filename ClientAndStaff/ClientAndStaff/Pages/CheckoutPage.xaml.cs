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
            CurrentOrder = new AddOrder();
            CurrentOrder.ManuProducts = Global.MyBasket.ToArray();
            CurrentOrder.Description = null;
            CurrentOrder.LoginUser = Global.CurrentUser.Login;
            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            var result = client.UploadString(Global.GlobalVar + "api/Orders/AddsProduct", JsonConvert.SerializeObject(CurrentOrder));
            await DisplayAlert("Message", "Order was successful", "OK");
            Global.MyBasket = new List<ManuProduct>();
            await Navigation.PushAsync(new OrderPage());
        }
    }
}
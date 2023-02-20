using ClientAndStaff.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClientAndStaff.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        public User CurrentUser { get; set; }
        public AddOrder CurrentOrder { get; set; }
        /// <summary>
        /// <behaviors:InvokeCommandAction Command="{Binding ItemTappedCommand}"/>
        /// </summary>
        public StartPage(User currentUser)
        {
            InitializeComponent();
            CurrentUser = currentUser;
            var client = new WebClient();
            var response = client.DownloadString("http://192.168.0.101:3310/api/Products/GetProducts");
            ListViewProducts.ItemsSource = JsonConvert.DeserializeObject<List<Product>>(response);
            


        }

        async void Button_Clicked_Order(object sender, EventArgs e)
        {

            /*Button btn = (Button)sender;
            Product product = (Product)btn.CommandParameter;*/
            /*Product products = ListViewProducts.SelectedItem as Product;*/

            CurrentOrder = new AddOrder();

            Product produc = ListViewProducts.SelectedItem as Product;

            ManuProduct[] manuProducts =
            {
                new ManuProduct(){ Name = produc.Name, Id = produc.Id, Description = produc.Description, Price = produc.Price, Image = produc.Image }
            };
            CurrentOrder.ManuProducts = manuProducts;
            
            CurrentOrder.LoginUser = CurrentUser.Login;
            
            CurrentOrder.Description = null;
            


            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            var result = client.UploadString("http://192.168.0.101:3310/api/Orders/AddsProduct", JsonConvert.SerializeObject(CurrentOrder));




            await DisplayAlert("Message", "Order was successful", "OK");
        }
    }
}
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

namespace ClientAndStaff.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        public AddOrder CurrentOrder { get; set; }
        /// <summary>
        /// <behaviors:InvokeCommandAction Command="{Binding ItemTappedCommand}"/>
        /// </summary>
        public StartPage(User currentUser)
        {
            InitializeComponent();

            var client = new WebClient();
            var response = client.DownloadString("http://192.168.0.101:3245/api/Products/GetProducts");
            ListViewProducts.ItemsSource = JsonConvert.DeserializeObject<List<Product>>(response);
            


        }

        async void Button_Clicked_Order(object sender, EventArgs e)
        {
            /*Button btn = (Button)sender;
            Product product = (Product)btn.CommandParameter;*/
            /*Product products = ListViewProducts.SelectedItem as Product;*/

            ManuProduct[] product = ListViewProducts.SelectedItem as ManuProduct[];



            CurrentOrder.Description = null;
            CurrentOrder.LoginUser = 43.ToString();
            CurrentOrder.ManuProduct = product;

            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            var result = client.UploadString("http://192.168.0.101:3245/api/Orders/AddsProduct", JsonConvert.SerializeObject(CurrentOrder));





        }
    }
}
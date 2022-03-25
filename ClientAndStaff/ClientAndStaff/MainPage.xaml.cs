using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ClientAndStaff
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var client = new WebClient();
            var response = client.DownloadString("http://localhost:55237/api/Products/GetProducts");
            ListViewProducts.ItemsSource = JsonConvert.DeserializeObject<List<Product>>(response);
        }

        private void ListViewProducts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}

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

namespace ClientAndStaff.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderDetailPage : ContentPage
    {
        public OrderDetailViewModel ViewModel { get; set; }

        public OrderDetailPage(OrderLk order)
        {
            InitializeComponent();
            ViewModel = new OrderDetailViewModel { CurrentOrder = order };
            BindingContext = ViewModel;
            var client = new WebClient();
            var response = client.DownloadString(Global.GlobalVar + "api/OrderAndProducts/GetProductsOrderFromOrderId?id=" + order.Id);
            OrdersList.ItemsSource = JsonConvert.DeserializeObject<List<Product>>(response); 
        }
    }
}
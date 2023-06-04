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
        
        public AddOrder CurrentOrder { get; set; }
        /// <summary>
        /// <behaviors:InvokeCommandAction Command="{Binding ItemTappedCommand}"/>
        /// </summary>
        public StartPage()
        {
            InitializeComponent();
            
            var client = new WebClient();
            var response = client.DownloadString(Global.GlobalVar + "api/Products/GetProducts");
            ListViewProducts.ItemsSource = JsonConvert.DeserializeObject<List<Product>>(response);
        }

        void Btn_add_Basket(object sender, EventArgs e)
        {

            Product produc = ListViewProducts.SelectedItem as Product;

            // Check if the product already exists in the basket
            ManuProduct existingProduct = Global.MyBasket.FirstOrDefault(p => p.Id == produc.Id);

            if (existingProduct == null)
            {
                // If the product doesn't exist, add a new item with quantity 1
                ManuProduct newManuProduct = new ManuProduct
                {
                    Name = produc.Name,
                    Id = produc.Id,
                    Description = produc.Description,
                    Price = produc.Price,
                    Image = produc.Image,
                    Quantity = 1
                };
                Global.MyBasket.Add(newManuProduct);
            }
            else
            {
                // If the product already exists, increment the quantity
                existingProduct.Quantity++;
            }

        }

        async void Button_Clicked_Order(object sender, EventArgs e)
        {

            /*Button btn = (Button)sender;
            Product product = (Product)btn.CommandParameter;*/
            /*Product products = ListViewProducts.SelectedItem as Product;*/

            //CurrentOrder = new AddOrder();

            /*Product produc = ListViewProducts.SelectedItem as Product;

            ManuProduct[] manuProducts =
            {
                new ManuProduct(){ Name = produc.Name, Id = produc.Id, Description = produc.Description, Price = produc.Price, Image = produc.Image }
            };*/
            /*CurrentOrder.ManuProducts = Global.MyBasket.ToArray();

            CurrentOrder.LoginUser = CurrentUser.Login;

            CurrentOrder.Description = null;*/



            


            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            var result = client.UploadString(Global.GlobalVar + "api/Orders/AddsProduct", JsonConvert.SerializeObject(CurrentOrder));




            await DisplayAlert("Message", "Order was successful", "OK");
        }
    }
}

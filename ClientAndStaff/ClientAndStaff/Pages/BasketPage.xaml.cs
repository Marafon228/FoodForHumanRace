using ClientAndStaff.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClientAndStaff.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasketPage : ContentPage
    {
        private Dictionary<int, int> _quantities = new Dictionary<int, int>();
        public BasketPage()
        {
            InitializeComponent();
            BindingContext = this;
            BasketList.ItemsSource = Global.MyBasket;
        }
        public Dictionary<int, int> Quantities
        {
            get { return _quantities; }
            set { _quantities = value; }
        }


        private void OnOrder_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CheckoutPage());
        }

        private void OnMinusButton_Clicked(object sender, EventArgs e)
        {
            ManuProduct selectedProduct = (sender as Button)?.CommandParameter as ManuProduct;
            if (selectedProduct.Quantity > 1)
            {
                selectedProduct.Quantity--;
                BasketList.ItemsSource = null;
                BasketList.ItemsSource = Global.MyBasket;
            }
        }

        private void OnPlusButton_Clicked(object sender, EventArgs e)
        {
            ManuProduct selectedProduct = (sender as Button)?.CommandParameter as ManuProduct;
            selectedProduct.Quantity++;
            BasketList.ItemsSource = null;
            BasketList.ItemsSource = Global.MyBasket;
        }

        private void OnRemoveButton_Clicked(object sender, EventArgs e)
        {
            ManuProduct selectedProduct = (sender as Button)?.CommandParameter as ManuProduct;
            Global.MyBasket.Remove(selectedProduct);
            BasketList.ItemsSource = null;
            BasketList.ItemsSource = Global.MyBasket;
        }
    }
}
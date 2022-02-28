using FoodForHumanRaceManagerDesktop.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FoodForHumanRaceManagerDesktop.Pages.Models
{
    /// <summary>
    /// Interaction logic for DetaliesEnterprise.xaml
    /// </summary>
    public partial class DetaliesEnterprise : Page
    {
        private ADO db = new ADO();

        private Enterprise myEnterprise;

        public Enterprise MyEnterprise
        {
            get { return myEnterprise; }
            set { myEnterprise = value; }
        }
        private List<Product> myProduct;

        public List<Product> MyProduct
        {
            get { return myProduct; }
            set { myProduct = value; }
        }


        public DetaliesEnterprise(Enterprise selectEnterprise)
        {
            if (selectEnterprise != null)
            {
                MyEnterprise = selectEnterprise;
            }

            MyEnterprise = db.Enterprise.FirstOrDefault();
            /*.Select(e=> new Enterprise { Name = e.Name, Address = e.Address, Latitude = e.Latitude})*/
            /*.ToList();*/

            /*MyProduct = db.Product*//*.Where(p=> MyEnterprise.TypesOfProducts == p.TypesOfProducts)*//*
                                    .ToList();*/

            InitializeComponent();
            var Product = db.Product
                /*.Where(p => MyEnterprise.TypesOfProducts == p.TypesOfProducts)*/.ToList();
            ListViewProducts.ItemsSource = Product;
        }
    }
}

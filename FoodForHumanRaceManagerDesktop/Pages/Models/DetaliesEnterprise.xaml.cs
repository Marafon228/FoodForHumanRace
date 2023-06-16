using FoodForHumanRaceManagerDesktop.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private Enterprise myEnterprise;

        public Enterprise MyEnterprise
        {
            get { return myEnterprise; }
            set { myEnterprise = value; }
        }
        private ObservableCollection<Product> myProduct;

        public ObservableCollection<Product> MyProduct
        {
            get { return myProduct; }
            set { myProduct = value; }
        }


        public DetaliesEnterprise(Enterprise selectEnterprise)
        {

            MyProduct = new ObservableCollection<Product>(selectEnterprise.TypesOfProducts.Select(t => t.Product));


            if (selectEnterprise != null)
            {
                MyEnterprise = selectEnterprise;
            }
            else
            {
                MyEnterprise = new Enterprise();
                MyEnterprise.UsersAndEnterprise = new List<UsersAndEnterprise>(ADO.Instance.User.ToList().Select(u => new UsersAndEnterprise() { User = u, Enterprise = MyEnterprise }));
            }
            InitializeComponent();

        }

        private void Btn_Add_Product(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddProduct(MyEnterprise));
        }

        private void Btn_Edit_Product(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditProduct((sender as Button).DataContext as Product));
        }

        private void Btn_Delete_Product(object sender, RoutedEventArgs e)
        {            
            var PrdoductRemoving = (sender as Button).DataContext as Product;


            if (MessageBox.Show($"Вы точно хотите удалить {PrdoductRemoving.Name}?", "Внимание", MessageBoxButton.YesNo , MessageBoxImage.Question) == MessageBoxResult.Yes)
            {              
                try
                {
                    ADO.Instance.TypesOfProducts.Remove(PrdoductRemoving.TypesOfProducts.FirstOrDefault());
                    ADO.Instance.SaveChanges();
                    MessageBox.Show("Продукт удалён");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            {

            }
        }
    }
}

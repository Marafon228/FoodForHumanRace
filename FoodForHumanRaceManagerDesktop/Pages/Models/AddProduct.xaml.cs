using FoodForHumanRaceManagerDesktop.Entity;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FoodForHumanRaceManagerDesktop.Pages.Models
{
    /// <summary>
    /// Логика взаимодействия для AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Page
    {
        /*private Product currentProduct;

        public Product CurrentProduct
        {
            get { return currentProduct; }
            set { currentProduct = value; }
        }*/



        public Product CurrentProduct
        {
            get { return (Product)GetValue(CurrentProductProperty); }
            set { SetValue(CurrentProductProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentProduct.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentProductProperty =
            DependencyProperty.Register("CurrentProduct", typeof(Product), typeof(AddProduct));


        public AddProduct()
        {
            CurrentProduct = new Product();
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openImage = new OpenFileDialog();
            openImage.FileName = "";
            openImage.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
            "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
            "Portable Network Graphic (*.png)|*.png";   // Filter files by extension

            if (openImage.ShowDialog() == DialogResult.OK)
            { 
                CurrentProduct.Image = (byte[]) new ImageConverter().ConvertTo(new Bitmap(openImage.FileName), typeof(byte[]));
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                CurrentProduct.TypesOfProducts.Add(new TypesOfProducts() { Enterprise = ADO.Instance.Enterprise.FirstOrDefault(), Product = CurrentProduct });
         
                ADO.Instance.Product.Add(CurrentProduct);
                ADO.Instance.SaveChanges();
                System.Windows.MessageBox.Show("Успешно !");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

        }

        private void Btn_Cancel(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}

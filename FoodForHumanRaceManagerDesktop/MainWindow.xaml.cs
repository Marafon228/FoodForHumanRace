using FoodForHumanRaceManagerDesktop.Entity;
using FoodForHumanRaceManagerDesktop.Pages;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace FoodForHumanRaceManagerDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.NavigationService.Navigate(new Authorization());
            ImportFile();
        }
        private void ImportFile()
        {
            var fileData = File.ReadAllLines(@"D:\КТК\МДК 05.02\Курсовая работа\Продукты.txt");
            var images = Directory.GetFiles(@"D:\КТК\МДК 05.02\Курсовая работа\фото");

            foreach (var line in fileData)
            {
                var data = line.Split('\t');

                var tempProduct = new Product
                {
                    Name = data[0]/*.Replace("\"", "")*/,
                    Description = data[1],
                    Price = decimal.Parse(data[2]),
                };
                try
                {
                    tempProduct.Image = File.ReadAllBytes(images.FirstOrDefault(p => p.Contains(tempProduct.Name)));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                ADO.Instance.Product.Add(tempProduct);
                ADO.Instance.SaveChanges();
            }
        }
    }
}

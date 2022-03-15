using FoodForHumanRaceManagerDesktop.Entity;
using FoodForHumanRaceManagerDesktop.Pages.Models;
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

namespace FoodForHumanRaceManagerDesktop.Pages.View
{
    /// <summary>
    /// Interaction logic for ViewEnterprise.xaml
    /// </summary>
    public partial class ViewEnterprise : Page
    {
        private ADO db = new ADO();

        private ObservableCollection<Enterprise> myEnterprise;

        public ObservableCollection<Enterprise> MyEnterprise
        {
            get { return myEnterprise; }
            set { myEnterprise = value; }
        }

        public ViewEnterprise()
        {


            MyEnterprise = new ObservableCollection<Enterprise>(ADO.Instance.Enterprise.ToList());



            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DetaliesEnterprise((sender as Button).DataContext as Enterprise));
        }

        private void Edit_Enterprise(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new NewEditEnterprice((sender as Button).DataContext as Enterprise));
        }

        private void Btn_Add_Enterprise(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new NewEditEnterprice(null));

        }
    }
}

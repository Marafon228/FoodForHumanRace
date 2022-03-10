using FoodForHumanRaceAdminDesktop.Entity;
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

namespace FoodForHumanRaceAdminDesktop.Pages
{
    /// <summary>
    /// Interaction logic for ViewEnterprise.xaml
    /// </summary>
    public partial class ViewEnterprise : Page
    {

        private Enterprise currentEnterprise;

        public Enterprise CurrentEnterprise
        {
            get { return currentEnterprise; }
            set { currentEnterprise = value; }
        }

        private User currentUser;

        public User CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; }
        }

        public ViewEnterprise(Enterprise enterprise = null, User user = null)
        {
            if (enterprise != null && user != null)
            {
                CurrentUser = user;
                CurrentEnterprise = enterprise;
            }
            else
            {
                CurrentEnterprise = new Enterprise();
                CurrentUser = new User();
            }

            /*CurrentUser = new User();

            CurrentEnterprise = new Enterprise();*/
            
            InitializeComponent();

            //DataGridViewEnterprise.ItemsSource = CurrentUser.Enterprise;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEnterprise());
        }
    }
}

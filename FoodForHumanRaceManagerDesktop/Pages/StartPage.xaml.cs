using FoodForHumanRaceManagerDesktop.Entity;
using FoodForHumanRaceManagerDesktop.Pages.View;
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

namespace FoodForHumanRaceManagerDesktop.Pages
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {


        public User UserEnterprise
        {
            get { return (User)GetValue(UserEnterpriseProperty); }
            set { SetValue(UserEnterpriseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UserEnterprise.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UserEnterpriseProperty =
            DependencyProperty.Register("UserEnterprise", typeof(User), typeof(StartPage));


        
        public StartPage(User userEnterprise)
        {
            if (userEnterprise != null)
            {
                UserEnterprise = userEnterprise;
            }
            InitializeComponent();
            StartFrame.Navigate(new ViewEnterprise(UserEnterprise));
        }

        private void Btn_View_Staff(object sender, RoutedEventArgs e)
        {
            StartFrame.Navigate(new ViewStaff(UserEnterprise));
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Authorization());
        }

        private void Btn_View_Enterprise(object sender, RoutedEventArgs e)
        {
            StartFrame.Navigate(new ViewEnterprise(UserEnterprise));

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StartFrame.Navigate(new ViewReporting());
        }
    }
}

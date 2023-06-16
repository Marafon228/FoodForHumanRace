using FoodForHumanRaceManagerDesktop.Entity;
using FoodForHumanRaceManagerDesktop.Helpers;
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
    /// Interaction logic for Authorization.xaml
    /// </summary>
    
    public partial class Authorization : Page
    {
        
        public User GlobalUser { get; set; }
        private User currentUser;

        public User CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; }
        }


        public Authorization()
        {
            
            CurrentUser = new User();
            InitializeComponent();
        }

        private void LogIn(object sender, RoutedEventArgs e)
        {
          
            if (CurrentUser.Login != null)
            {
                if (CurrentUser.Password != null)
                {
                    GlobalUser = ADO.Instance.User.Where(c => c.Login == CurrentUser.Login && c.Password == CurrentUser.Password).FirstOrDefault();
                    if (GlobalUser != null)
                    {
                        NavigationService.Navigate(new StartPage(GlobalUser));
                    }
                    else
                    {
                        MessageBox.Show("провал");
                    }
                }
                else
                {
                    MessageBox.Show("Введите пароль");
                }
            }
            else
            {
                MessageBox.Show("Введите логин");
            }
        }
    }
}

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
    /// Interaction logic for EditStaff.xaml
    /// </summary>
    public partial class EditStaff : Page
    {




        public User CurrentUser
        {
            get { return (User)GetValue(CurrentUserProperty); }
            set { SetValue(CurrentUserProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentUser.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentUserProperty =
            DependencyProperty.Register("CurrentUser", typeof(User), typeof(EditStaff));




        public EditStaff(User user)
        {
            CurrentUser = user;
            InitializeComponent();
        }

        private void Btn_Save_Staff(object sender, RoutedEventArgs e)
        {
            try
            {
                ADO.Instance.SaveChanges();
                System.Windows.Forms.MessageBox.Show("Save");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void Btn_Back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}

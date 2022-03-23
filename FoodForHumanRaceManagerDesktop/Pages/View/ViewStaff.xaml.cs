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
using FoodForHumanRaceManagerDesktop.Pages;
using FoodForHumanRaceManagerDesktop.Pages.Models;

namespace FoodForHumanRaceManagerDesktop.Pages.View
{
    /// <summary>
    /// Логика взаимодействия для ViewStaff.xaml
    /// </summary>
    public partial class ViewStaff : Page
    {
        private User globalUser;

        public User GlobalUser
        {
            get { return globalUser; }
            set { globalUser = value; }
        }

        private ObservableCollection<User> userStaff;

        public ObservableCollection<User> UserStaff
        {
            get { return userStaff; }
            set { userStaff = value; }
        }

        public ViewStaff(User userEnterprise)
        {
            GlobalUser = userEnterprise;
            
            UserStaff = new ObservableCollection<User>(ADO.Instance.User.Where(u=> u.Role.Name == "Сотрудник" /*&& u.UsersAndEnterprise == GlobalUser.UsersAndEnterprise*/));
            InitializeComponent();
        }

        private void Btn_Add_Staff(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddStaff(GlobalUser));
        }

        private void Btn_Edit_Staff(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditStaff((sender as Button).DataContext as User));
        }
    }
}

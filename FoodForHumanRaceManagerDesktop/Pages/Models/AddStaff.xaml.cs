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
    /// Interaction logic for AddStaff.xaml
    /// </summary>
    public partial class AddStaff : Page
    {

        private User currentUser;

        public User CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; }
        }
        private User globalUser;

        public User GlobalUser
        {
            get { return globalUser; }
            set { globalUser = value; }
        }


        public AddStaff(User userEnterprise)
        {
            GlobalUser = userEnterprise;

            CurrentUser = new User();
            

            
            CurrentUser.Role = ADO.Instance.Role.FirstOrDefault(r => r.Name == "Сотрудник");
            //CurrentUser.UsersAndEnterprise = new UsersAndEnterprise(GlobalUser.UsersAndEnterprise.Select(u=> u.Enterprise));




            /*if (userEnterprise.Role.Name == "Предприниматель")
            {
                GlobalUser = userEnterprise;
            }
            else
            {
                CurrentUser = userEnterprise;
            }*/

            /*if (userEnterprise != null)
            {
                CurrentUser = userEnterprise;
            }
            else
            {
                CurrentUser = new User();
                CurrentUser.Role = ADO.Instance.Role.FirstOrDefault(r => r.Name == "Сотрудник");
            }*/
            InitializeComponent();
        }

        private void Btn_Save_Staff(object sender, RoutedEventArgs e)
        {
            try
            {
                var enterprise = GlobalUser.UsersAndEnterprise.FirstOrDefault().Enterprise;
                //CurrentUser.UsersAndEnterprise.FirstOrDefault().Enterprise = GlobalUser.UsersAndEnterprise.FirstOrDefault().Enterprise;

                //CurrentUser.UsersAndEnterprise = GlobalUser.UsersAndEnterprise;
                ADO.Instance.User.Add(CurrentUser);
                ADO.Instance.SaveChanges();
                ADO.Instance.UsersAndEnterprise.Add(new UsersAndEnterprise() { User = CurrentUser , Enterprise = enterprise });
                ADO.Instance.SaveChanges();
                MessageBox.Show("Сохранено");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_Back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}

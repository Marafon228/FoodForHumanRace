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

        public AddStaff()
        {
            CurrentUser.Role = ADO.Instance.Role.FirstOrDefault(r => r.Name == "Сотрудник");
            InitializeComponent();
        }

        private void Btn_Save_Staff(object sender, RoutedEventArgs e)
        {
            try
            {
                //CurrentUser.Enterprise = 
                ADO.Instance.User.Add(CurrentUser);
                ADO.Instance.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

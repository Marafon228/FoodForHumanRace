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

namespace FoodForHumanRaceManagerDesktop.Pages.View
{
    /// <summary>
    /// Логика взаимодействия для ViewStaff.xaml
    /// </summary>
    public partial class ViewStaff : Page
    {
        private ObservableCollection<User> userStaff;

        public ObservableCollection<User> UserStaff
        {
            get { return userStaff; }
            set { userStaff = value; }
        }

        public ViewStaff()
        {
            
            UserStaff = new ObservableCollection<User>(ADO.Instance.User);
            InitializeComponent();
        }
    }
}

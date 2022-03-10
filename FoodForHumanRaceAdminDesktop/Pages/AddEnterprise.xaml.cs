
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
    /// Interaction logic for AddEnterprise.xaml
    /// </summary>
    public partial class AddEnterprise : Page
    {
        private User currentUser;

        public User CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; CurrentUser.OnPropertyChanged(); }
        }

        private Enterprise currentEnterprise;

        public Enterprise CurrentEnterprise
        {
            get { return currentEnterprise; }
            set { currentEnterprise = value; }
        }

        private ObservableCollection<TypeOfEnterprise> enterprise;

        public ObservableCollection<TypeOfEnterprise> TypeEnterprise
        {
            get { return enterprise; }
            set { enterprise = value;  }
        }

        private TypeOfEnterprise currentTypeEnterprise;

        public TypeOfEnterprise CurrentTypeEnterprise
        {
            get { return currentTypeEnterprise; }
            set { currentTypeEnterprise = value; CurrentTypeEnterprise.OnPropertyChanged(); }
        }




        public AddEnterprise()
        {
            CurrentTypeEnterprise = ADO.Instance.TypeOfEnterprise.FirstOrDefault();

            TypeEnterprise = new ObservableCollection<TypeOfEnterprise>(ADO.Instance.TypeOfEnterprise.ToList());

            CurrentUser = new User() { Role = ADO.Instance.Role.FirstOrDefault(r=> r.Name == "Предприниматель") };
            CurrentEnterprise = new Enterprise();
            CurrentEnterprise.User = CurrentUser;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                ADO.Instance.User.Add(CurrentUser);
                ADO.Instance.SaveChanges();
                ADO.Instance.Enterprise.Add(CurrentEnterprise);
                ADO.Instance.SaveChanges();
                MessageBox.Show("OK");
            }
            catch (Exception)
            {
                MessageBox.Show("Not OK");
            }
        }
    }
}

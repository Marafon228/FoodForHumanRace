
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

            TypeEnterprise = new ObservableCollection<TypeOfEnterprise>(ADO.Instance.TypeOfEnterprise.ToList());

            CurrentUser = new User() { Role = ADO.Instance.Role.FirstOrDefault(r=> r.Name == "Предприниматель") };
            CurrentEnterprise = new Enterprise();
            //CurrentTypeEnterprise = ADO.Instance.TypeOfEnterprise.First();
            /*CurrentUser.UsersAndEnterprise = CurrentEnterprise.UsersAndEnterprise;
            CurrentEnterprise.UsersAndEnterprise = CurrentUser.UsersAndEnterprise;*/
            //CurrentEnterprise.User = CurrentUser;
            
            //CurrentUser.Enterprise = (ICollection<Enterprise>)CurrentEnterprise;
            InitializeComponent();

            

        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                /*CurrentUser.Enterprise.Add(CurrentEnterprise);
                CurrentUser.UsersAndEnterprise.Add(CurrentEnterprise);*/
                ADO.Instance.UsersAndEnterprise.Add(new UsersAndEnterprise { Enterprise = CurrentEnterprise, User = CurrentUser });
                //ADO.Instance.User.Add(CurrentUser);
                ADO.Instance.SaveChanges();
                
                
                MessageBox.Show("OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

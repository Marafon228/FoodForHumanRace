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
        private System.Windows.Threading.DispatcherTimer timer;

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
            set { currentUser = value;CurrentUser.OnPropertyChanged(); }
        }
        private ObservableCollection<Enterprise> enterprises;

        public ObservableCollection<Enterprise> Enterprises
        {
            get { return enterprises; }
            set { enterprises = value; }
        }

        public ViewEnterprise(/*Enterprise enterprise *//*User user = null*/)
        {
            CurrentUser = ADO.Instance.User.Where(u => u.Id == 2).FirstOrDefault();
            //var userss = new ObservableCollection<User>(enterprise.UsersAndEnterprise.Select(e => e.User).Where(e=> e.Role.Name == "Предприниматель"));
            //Enterprises = new ObservableCollection<Enterprise>(/*ADO.Instance.Enterprise.ToList()*/enterprises.FirstOrDefault().UsersAndEnterprise.Select(e=> e.Enterprise).Where(e=>e.UserLogin == userss.FirstOrDefault().Login ));
            Enterprises = new ObservableCollection<Enterprise>(ADO.Instance.Enterprise.ToList());
            
            //CurrentEnterprise = ADO.Instance.Enterprise.FirstOrDefault();
            //CurrentEnterprise = ADO.Instance.Enterprise.ToList().FirstOrDefault();
            //CurrentUser = ADO.Instance.User.FirstOrDefault();
            /*if (*//*enterprise != null &&*//* user != null)
            {
                CurrentUser = user;
                //CurrentEnterprise = enterprise;
            }
            else
            {
                // CurrentEnterprise = new Enterprise();
                //CurrentUser = new List<User>(ADO.Instance.User.ToList());

            }*/

            /*CurrentUser = new User();

            CurrentEnterprise = new Enterprise();*/

            InitializeComponent();

            //DataGridViewEnterprise.ItemsSource = CurrentUser.Enterprise;

            // Initialize the timer
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);

            // Attach the tick event handler
            timer.Tick += Timer_Tick;

            // Start the timer
            timer.Start();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Update the data in the DataGrid
            UpdateDataGrid();
        }

        private void UpdateDataGrid()
        {
            // Fetch the updated data from your data source
            Enterprises = new ObservableCollection<Enterprise>(ADO.Instance.Enterprise.ToList());

            // Assign the data to the DataGrid's ItemsSource property
            DataGridViewEnterprise.ItemsSource = Enterprises;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEnterprise());
        }
    }
}

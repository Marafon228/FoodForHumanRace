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
            if (CurrentUser != null)
            {
                CurrentUser = ADO.Instance.User.Where(u => u.Id == 2).FirstOrDefault();

            }          
            Enterprises = new ObservableCollection<Enterprise>(ADO.Instance.Enterprise.ToList());
            InitializeComponent();
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateDataGrid();
        }

        private void UpdateDataGrid()
        {
            Enterprises = new ObservableCollection<Enterprise>(ADO.Instance.Enterprise.ToList());
            DataGridViewEnterprise.ItemsSource = Enterprises;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEnterprise());
        }

        private void Edit_Enterprise_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditEnterprice((sender as Button).DataContext as Enterprise));
        }
    }
}

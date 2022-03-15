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

namespace FoodForHumanRaceManagerDesktop.Pages.Models
{
    /// <summary>
    /// Логика взаимодействия для NewEditEnterprice.xaml
    /// </summary>
    public partial class NewEditEnterprice : Page
    {
        

        private ObservableCollection<TypeOfEnterprise> typeEnterprise;

        public ObservableCollection<TypeOfEnterprise> TypeEnterprise
        {
            get { return typeEnterprise; }
            set { typeEnterprise = value; }
        }

        public Enterprise CurrentEnterprise
        {
            get { return (Enterprise)GetValue(CurrentEnterpriseProperty); }
            set { SetValue(CurrentEnterpriseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentEnterprise.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentEnterpriseProperty =
            DependencyProperty.Register("CurrentEnterprise", typeof(Enterprise), typeof(NewEditEnterprice));



        public NewEditEnterprice(Enterprise enterprise)
        {
            TypeEnterprise = new ObservableCollection<TypeOfEnterprise>(ADO.Instance.TypeOfEnterprise);
            if (enterprise != null)
            {
                CurrentEnterprise = enterprise;
            }
            InitializeComponent();
        }
    }
}

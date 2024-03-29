﻿using FoodForHumanRaceManagerDesktop.Entity;
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
        public User GlobalUser { get; set; }

        // Using a DependencyProperty as the backing store for CurrentEnterprise.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentEnterpriseProperty =
            DependencyProperty.Register("CurrentEnterprise", typeof(Enterprise), typeof(NewEditEnterprice));



        public NewEditEnterprice(User userEnter)
        {
            TypeEnterprise = new ObservableCollection<TypeOfEnterprise>(ADO.Instance.TypeOfEnterprise);
            
            
            
            GlobalUser = userEnter;

            CurrentEnterprise = new Enterprise();
                
            
            InitializeComponent();
        }

        private void Btn_Save(object sender, RoutedEventArgs e)
        {

            try
            {
                //var globalUser = new List<User>(GlobalEnterprise.UsersAndEnterprise.Select(u => u.User));
                ADO.Instance.Enterprise.Add(CurrentEnterprise);
                ADO.Instance.SaveChanges();
                ADO.Instance.UsersAndEnterprise.Add(new UsersAndEnterprise() { Enterprise = CurrentEnterprise, User = GlobalUser });
                ADO.Instance.SaveChanges();
                MessageBox.Show("OK");
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

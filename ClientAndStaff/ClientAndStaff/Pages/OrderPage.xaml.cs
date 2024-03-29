﻿using ClientAndStaff.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClientAndStaff.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderPage : ContentPage
    {
        public OrderPage()
        {

            InitializeComponent();
            if (Global.CurrentUser.Role == "Клиент")
            {
                ControlTemplate = (ControlTemplate)Application.Current.Resources["NavigationTemplate"];
            }
            else
            {
                ControlTemplate = (ControlTemplate)Application.Current.Resources["NavigationTemplateStaff"];
            }
    
            if (Global.CurrentUser.Role == "Клиент")
            {
                var client = new WebClient();
                var response = client.DownloadString(Global.GlobalVar + "api/Orders/GetOrdersFromUserId?id=" + Global.CurrentUser.Id);
                OrdersList.ItemsSource = JsonConvert.DeserializeObject<List<OrderLk>>(response);
            }
            else
            {
                var client = new WebClient();
                var response = client.DownloadString(Global.GlobalVar + "api/Orders/GetOrdersFromStaffId?id=" + Global.CurrentUser.Id);
                OrdersList.ItemsSource = JsonConvert.DeserializeObject<List<OrderLk>>(response);
            }
            
        }

        private void OnDetailsButton_Clicked(object sender, EventArgs e)
        {
            var order = (sender as Button).BindingContext as OrderLk;
            
            Navigation.PushAsync(new OrderDetailPage(order));
        }
    }
}
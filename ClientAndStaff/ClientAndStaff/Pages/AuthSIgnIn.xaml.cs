using ClientAndStaff.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace ClientAndStaff.Pages
{
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthSIgnIn : ContentPage
    {
        
        public User CurrentUser { get; set; }

        public AuthSIgnIn()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new StartPage());
            
            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            var result = client.UploadString(Global.GlobalVar + "api/Users/SignIn", JsonConvert.SerializeObject(CurrentUser));
            if (result != null)
            {
                Navigation.PushAsync(new MainPage());
            }
        }
    }
}
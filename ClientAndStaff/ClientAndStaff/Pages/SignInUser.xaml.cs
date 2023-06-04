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
	public partial class SignInUser : ContentPage
	{
        public UserAuth CurrentUser { get; set; }
        public SignInUser()
        {
            CurrentUser = new UserAuth();

            InitializeComponent();

            BindingContext = this;
        }

        private async void Btn_SignIn(object sender, EventArgs e)
        {

            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            var result = client.UploadString(Global.GlobalVar + "api/Users/SignIn", JsonConvert.SerializeObject(CurrentUser));
            if (result != null)
            {
                var roleUser = JsonConvert.DeserializeObject<User>(result);
                if (roleUser.Role == "Клиент")
                {
                    Global.CurrentUser = roleUser;
                    await DisplayAlert("Message", "Registration was successful", "OK");
                    await Navigation.PushAsync(new StartPage(), true);
                }
                else if (roleUser.Role == "Сотрудник" || roleUser.Role == "Предприниматель" || roleUser.Role == "Менеджер")
                {
                    Global.CurrentUser = roleUser;
                    await DisplayAlert("Message", "Registration was successful", "OK");
                    await Navigation.PushAsync(new StartPageStaff(), true);
                }
                else
                {
                    await DisplayAlert("Message", "Registration invalid", "OK");

                }
            }

        }

        private async void Btn_Reg(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterUser(), true);
        }
    }
}
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
    public partial class ProfilePage : ContentPage
    {
        private User _user = Global.CurrentUser;

        private UserUpdate _userUpdate;

        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = _user;
            if (Global.CurrentUser.Role == "Клиент")
            {
                ControlTemplate = (ControlTemplate)Application.Current.Resources["NavigationTemplate"];
            }
            else
            {
                ControlTemplate = (ControlTemplate)Application.Current.Resources["NavigationTemplateStaff"];
            }
        }

        private async void Btn_Save_Clicked(object sender, EventArgs e)
        {
            try
            {
                var id_role = 0;
                if(_user.Role == "Сотрудник")
                {
                    id_role = 1;
                }
                else if(_user.Role == "Предприниматель")
                {
                    id_role = 2;
                }
                else if (_user.Role == "Менеджер")
                {
                    id_role = 3;
                }
                else if (_user.Role == "Клиент")
                {
                    id_role = 4;
                }
                _userUpdate = new UserUpdate(){ Id = _user.Id, FirsName = _user.FirsName, LastName = _user.LastName,
                    MidleName = _user.LastName, Adress = _user.Adress, Email = _user.Email, Login = _user.Login,
                    Password = _user.Password, Phone = _user.Phone, IdRole = id_role};
                var client = new WebClient();
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                var url = Global.GlobalVar + "api/Users/UpdateUser?id=" + _user.Id;
                var result = client.UploadString(url, "PUT", JsonConvert.SerializeObject(_userUpdate));
                if(result != null)
                {
                    await DisplayAlert("Message", "The profile update was successful", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", ex.Message, "OK");
                throw;
            }
            
        }

        private async void Btn_Log_Out_Clicked(object sender, EventArgs e)
        {
            Global.CurrentUser = null;
            await Navigation.PushAsync(new SignInUser(), true);
        }
    }
}
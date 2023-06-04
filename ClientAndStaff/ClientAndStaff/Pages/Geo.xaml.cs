using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClientAndStaff.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Geo : ContentPage
    {
        public Geo()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest()
                    {
                        DesiredAccuracy = GeolocationAccuracy.High,
                        Timeout = TimeSpan.FromSeconds(30)
                    });

                }

                if (location == null)
                {
                    LabelLacation.Text = "No GPS";
                }
                else
                {
                    LabelLacation.Text = $"{location.Latitude} and {location.Longitude}";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Warning {ex.Message}");
            }
        }
    }
}
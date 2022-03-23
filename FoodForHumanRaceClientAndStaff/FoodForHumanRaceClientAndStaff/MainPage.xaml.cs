using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FoodForHumanRaceClientAndStaff
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                string srvrdbname = "FoodForHumanRace";
                string srvrname = "192.168.0.101";
                string svrusername = "marafon";
                string svriserpassword = "12345cth";


                string sqlcon = $"Data Source={srvrname}; Initial Catalog={srvrdbname}; User Id={svrusername}; Password={svriserpassword};Trusted_Connection=true ";

                SqlConnection sqlConnection = new SqlConnection(sqlcon);
                sqlConnection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        
        }
    }
}

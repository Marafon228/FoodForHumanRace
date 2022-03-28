using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationFoodForHumanRace.Models.Json
{
    public class RegisterUser
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirsName { get; set; }
        public string MidleName { get; set; }
        public string LastName { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

    }
}